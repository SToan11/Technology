namespace ASM2.Config
{
    public class SystemConfig
    {
        private IConfiguration _configuration;
        public static string ConnectionString;

        public SystemConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Init()
        {
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
        }
    }
}
