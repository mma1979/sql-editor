using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;
using sql_editor.Models;

namespace sql_editor.Services
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
            if (_options.ConnectionMode == "string") return _options.ConnectionString;
            
            var d = _options.Direct;
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = d.Server,
                InitialCatalog = d.Database,
                TrustServerCertificate = true,
                MultipleActiveResultSets = true
            };

            if (d.AuthMode == "sql")
            {
                builder.UserID = d.Username;
                builder.Password = d.Password;
            }
            else
            {
                builder.IntegratedSecurity = true;
            }

            return builder.ConnectionString;
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
                // Return empty list on failure for mock-up safety, 
                // but in real app we'd handle/log the error.
                return new List<SchemaNode>();
            }
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
}
