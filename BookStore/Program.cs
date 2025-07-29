using BookStore;
using BookStore.Abstraction;
using BookStore.MongDb;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using BookStore.Domain;
using BookStore.Services;

public class Program
{
  private static void Main(string[] args)
  {
    var assemblies = LoadApplicationDependecies();
    var builder = WebApplication.CreateBuilder(args);


    builder.Services.AddMediatR(options =>
    {
      options.RegisterServicesFromAssemblies(assemblies);
    });

    builder.Services.AddAutoMapper(assemblies);

    builder.Services.Scan(scan => scan.FromAssemblies(assemblies)
    .AddClasses(type => type.AssignableTo(typeof(IDatabase)))
    .AsImplementedInterfaces().WithSingletonLifetime()
    .AddClasses(type => type.AssignableTo(typeof(IRepository<>)))
    .AsImplementedInterfaces().WithScopedLifetime());

    builder.Services.AddControllers();
    builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblies(assemblies));

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var section = builder.Configuration.GetSection("DatabaseConfiguration");
    var connectioStringSection = section.GetSection("ConnectionString").Value;
    var databaseName = section.GetSection("DatabaseName").Value;

    builder.Services.AddSingleton<IDatabaseConfiguration>(options =>
    {
      return new DatabaseConfiguration
      {
        ConnectionString = connectioStringSection,
        DatabaseName = databaseName
      };
    });
    var authSecretKey = builder.Configuration["AuthSecuredKey"];

    builder.Services.AddSingleton<IAuthSecuredKey>(sp =>
    {
      return new AuthSecuredKey
      {
        Key = authSecretKey
      };
    });
    builder.Services.AddSingleton<IAuth, Auth>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    app.UseMiddleware<GlobalLoggerMiddleware>();

    app.Run();
  }

  private static Assembly[] LoadApplicationDependecies()
  {
    var context = DependencyContext.Default;
    return context.RuntimeLibraries.SelectMany(library =>
    library.GetDefaultAssemblyNames(context))
      .Where(assembly => assembly.FullName.Contains("BookStore")).Select(Assembly.Load).ToArray();
  }
}