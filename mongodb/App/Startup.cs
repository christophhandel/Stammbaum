using LeoMongo;
using FamilyTreeMongoApp.Core.Util;
using FamilyTreeMongoApp.Core.Workloads.Person;
using Neo4j.Driver;
using Neo4jClient;

namespace FamilyTreeMongoApp;

public class Startup
{
    private const string Origin = "_allowSpecificOrigins";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<AppSettings>(Configuration.GetSection(AppSettings.Key));

        var settings = new AppSettings();
        Configuration.GetSection("AppSettings").Bind(settings);

        services.AddAutoMapper(typeof(MapperProfile));

        switch (settings.DatabaseType.ToLower())
        {
            case "neo4j":
                // This is to register your Neo4j Driver Object as a singleton
                services.AddSingleton(GraphDatabase.Driver(settings.Neo4jConnection, AuthTokens.Basic(settings.Neo4jUser, settings.Neo4jPassword)));
               
                //TODO: REMOVE
                services.AddLeoMongo<MongoConfig>();

                // This is the registration for your domain repository class
                services.AddScoped<IPersonRepository, PersonRepositoryNeo>();
                break;
            case "mongodb":
                // configure fwk
                services.AddLeoMongo<MongoConfig>();

                // for bigger assemblies it would be alright to register those via reflection by naming convention!
                services.AddScoped<IPersonRepository, PersonRepository>();
                break;
        }

        services.AddScoped<IPersonService, PersonService>();

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddControllers();

        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            options.AddPolicy(Origin,
                builder =>
                {
                    builder.WithOrigins("*") // Angular CLI
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); });
            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<ErrorLoggingMiddleware>();

        //app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(Origin);

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}

public sealed class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}