namespace Mma.SqlStudio.SqlServer.Models
{
    public class SqlStudioOptions
    {
        public string Database { get; set; } = "";
        public string ConnectionString { get; set; } = "";
        public string Route { get; set; } = "/sql-studio";
        public string AppName { get; set; } = "Mma SQL Studio";
    }
}
