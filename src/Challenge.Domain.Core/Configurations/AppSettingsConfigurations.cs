namespace Challenge.Domain.Core.Configurations
{
    public class AppSettingsConfigurations
    {
        public DataBaseConfig DataBaseConfiguration { get; set; }
    }

    public class DataBaseConfig
    {
        public string Server { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Cluster { get; set; }
        public string DataBaseName { get; set; }
    }
}
