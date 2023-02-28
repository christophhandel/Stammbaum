using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Core.Workloads.AccomplishmentWorkload;
using FamilyTreeMongoApp.Core.Workloads.Person;
using FamilyTreeMongoApp.Core.Workloads.PersonWorkload;
using LeoMongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Driver;

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
            var settings = new AppSettings();
            Configuration.GetSection("AppSettings").Bind(settings);
            
            // configure fwk
            services.AddLeoMongo<MongoConfig>();
            
            services.AddSingleton(GraphDatabase.Driver(settings.Neo4jConnection, AuthTokens.Basic(settings.Neo4jUser, settings.Neo4jPassword)));


            // for bigger assemblies it would be alright to register those via reflection by naming convention!
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IAccomplishmentRepository, AccomplishmentRepository>();

            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        }
    }
}