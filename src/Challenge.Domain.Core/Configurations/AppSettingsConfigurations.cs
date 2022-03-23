namespace Challenge.Domain.Core.Configurations
{
    public class AppSettingsConfigurations
    {
        public DataBaseConfig DataBaseConfiguration { get; set; }
    }

    public class DataBaseConfig
    {
        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }
    }
}
