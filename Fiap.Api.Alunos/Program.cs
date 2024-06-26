using AutoMapper;
using Fiap.Api.Alunos.Services;
using Fiap.Api.Alunos.Data.Context;
using Fiap.Api.Alunos.Data.Repository;
using Fiap.Api.Alunos.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Fiap.Web.Alunos.Services;
using Asp.Versioning;
using Fiap.Api.Alunos;
using Fiap.Api.Alunos.ViewModel;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        #region INICIALIZANDO O BANCO DE DADOS
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
        builder.Services.AddDbContext<DatabaseContext>(
            opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
        );
        #endregion

        #region Repositorios
        builder.Services.AddScoped<IAlertaRepository, AlertaRepository>();
        #endregion

        #region Services
        builder.Services.AddScoped<IAlertaService, AlertaService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        #endregion

        #region AutoMapper
        // Configuração do AutoMapper
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AllowNullCollections = true;
            c.AllowNullDestinationValues = true;
            c.CreateMap<AlertaModel, AlertaViewModel>();
            c.CreateMap<AlertaViewModel, AlertaModel>();
        });

        IMapper mapper = mapperConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);
        #endregion

        #region Autenticacao
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi")),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        #endregion

        #region Versionamento
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version")
            );
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
        #endregion

        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in app.DescribeApiVersions())
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName);
                }
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
