using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Core.Workloads.Person;
using LeoMongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyTreeMongoApp.Test
{
    public class Startup 
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection(AppSettings.Key));
            services.AddAutoMapper(typeof(MapperProfile));
            
            // configure fwk
            services.AddLeoMongo<MongoConfig>();

            // for bigger assemblies it would be alright to register those via reflection by naming convention!
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        }
    }
}