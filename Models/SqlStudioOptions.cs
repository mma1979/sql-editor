namespace Mma.SqlStudio.SqlServer.Models
{
    public class SqlStudioOptions
    {
        public string ConnectionMode { get; set; } = "builder";
        public DirectOptions Direct { get; set; } = new();
        public string ConnectionString { get; set; } = "";
        public ApiProxyOptions ApiProxy { get; set; } = new();
    }

    public class DirectOptions
    {
        public string Server { get; set; } = "";
        public string Database { get; set; } = "";
        public string AuthMode { get; set; } = "windows";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class ApiProxyOptions
    {
        public string Url { get; set; } = "";
        public string Token { get; set; } = "";
        public string DatabaseId { get; set; } = "";
    }
}
