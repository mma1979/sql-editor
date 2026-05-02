using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;
using Mma.SqlStudio.SqlServer.Models;

namespace Mma.SqlStudio.SqlServer.Services
{
    public class SchemaService
    {
        private readonly SqlStudioOptions _options;

        public SchemaService(IOptions<SqlStudioOptions> options)
        {
            _options = options.Value;
        }

        private string GetConnectionString()
        {
           return _options.ConnectionString;
        }

        public async Task<List<SchemaNode>> GetSchemaAsync()
        {
            string connString = GetConnectionString();
            using IDbConnection db = new SqlConnection(connString);

            var sql = @"
                SELECT 
                    TABLE_SCHEMA as SchemaName, 
                    TABLE_NAME as ObjectName, 
                    TABLE_TYPE as ObjectType 
                FROM INFORMATION_SCHEMA.TABLES
                UNION ALL
                SELECT 
                    ROUTINE_SCHEMA as SchemaName,
                    ROUTINE_NAME as ObjectName,
                    'PROCEDURE' as ObjectType
                FROM INFORMATION_SCHEMA.ROUTINES
                WHERE ROUTINE_TYPE = 'PROCEDURE'
                ORDER BY SchemaName, ObjectName";

            try
            {
                var results = await db.QueryAsync<SchemaItem>(sql);

                if (_options.ExcludedSchemas != null && _options.ExcludedSchemas.Any())
                {
                    results = results.Where(r => !_options.ExcludedSchemas.Contains(r.SchemaName, StringComparer.OrdinalIgnoreCase));
                }

                if (_options.ExcludedObjects != null && _options.ExcludedObjects.Any())
                {
                    results = results.Where(r => !_options.ExcludedObjects.Contains(r.ObjectName, StringComparer.OrdinalIgnoreCase));
                }
                
                return results.GroupBy(r => r.SchemaName)
                    .Select(g => new SchemaNode
                    {
                        Name = g.Key,
                        Children = new List<CategoryNode>
                        {
                            new CategoryNode("Tables", g.Where(x => x.ObjectType == "BASE TABLE").Select(x => x.ObjectName).ToList()),
                            new CategoryNode("Views", g.Where(x => x.ObjectType == "VIEW").Select(x => x.ObjectName).ToList()),
                            new CategoryNode("Procedures", g.Where(x => x.ObjectType == "PROCEDURE").Select(x => x.ObjectName).ToList())
                        }
                    }).ToList();
            }
            catch
            {
                return new List<SchemaNode>();
            }
        }

        public async Task<QueryResult> ExecuteQueryAsync(string sql)
        {
            var result = new QueryResult();
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                
                using var cmd = new SqlCommand(sql, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                
                // Check if we have a result set (query)
                if (reader.FieldCount > 0)
                {
                    var dt = new DataTable();
                    dt.Load(reader);

                    result.Columns = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
                    foreach (DataRow row in dt.Rows)
                    {
                        result.Rows.Add(row.ItemArray.Select(i => i?.ToString() ?? "NULL").ToList());
                    }
                    result.Message = $"Success: {dt.Rows.Count} rows returned.";
                    result.IsQuery = true;
                }
                else
                {
                    // Non-query (INSERT, UPDATE, DELETE, etc.)
                    int affected = reader.RecordsAffected;
                    result.Message = $"Success: Command executed. {affected} rows affected.";
                    result.IsQuery = false;
                }
                
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error: " + ex.Message;
            }
            return result;
        }

        private class SchemaItem
        {
            public string SchemaName { get; set; } = "";
            public string ObjectName { get; set; } = "";
            public string ObjectType { get; set; } = "";
        }
    }

    public class SchemaNode
    {
        public string Name { get; set; } = "";
        public List<CategoryNode> Children { get; set; } = new();
    }

    public class CategoryNode
    {
        public string Name { get; set; }
        public List<string> Objects { get; set; }
        public CategoryNode(string name, List<string> objects)
        {
            Name = name;
            Objects = objects;
        }
    }

    public class QueryResult
    {
        public bool Success { get; set; }
        public bool IsQuery { get; set; }
        public string Message { get; set; } = "";
        public List<string> Columns { get; set; } = new();
        public List<List<string>> Rows { get; set; } = new();
    }
}
