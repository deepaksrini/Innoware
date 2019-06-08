using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Innowave.FreedomAdmin.Api.Filters;
using Innowave.FreedomAdmin.Api.MediatR;
using Innowave.FreedomAdmin.Api.Swagger;
using Innowave.FreedomAdmin.Business;
using Innowave.FreedomAdmin.Business.Addresses.UpdateAddress;
using Innowave.FreedomAdmin.Business.Behaviours;
using Innowave.FreedomAdmin.Business.Businesses.ActivateBusiness;
using Innowave.FreedomAdmin.Business.Businesses.CreateNewBusiness;
using Innowave.FreedomAdmin.Business.Businesses.DeActivateBusiness;
using Innowave.FreedomAdmin.Business.Businesses.GetAllBusiness;
using Innowave.FreedomAdmin.Business.Businesses.GetBusinessById;
using Innowave.FreedomAdmin.Business.Businesses.ProductByBusiness;
using Innowave.FreedomAdmin.Business.Connector;
using Innowave.FreedomAdmin.Business.Helpers;
using Innowave.FreedomAdmin.Business.Menus.GetMenusByUsers;
using Innowave.FreedomAdmin.Business.Products.GetAllProducts;
using Innowave.FreedomAdmin.Business.Security;
using Innowave.FreedomAdmin.DataContext;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PartialResponse.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Innowave.FreedomAdmin.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services
               .AddMvc(options =>
               {
                   options.Filters.Add(typeof(ApiClientValidationExceptionAttribute));
                   options.Filters.Add(typeof(FluentValidationExceptionAttribute));
                   options.Filters.Add(typeof(ValidateModelAttribute));
                   options.Filters.Add(typeof(GlobalExceptionFilter));
                   options.OutputFormatters.RemoveType<JsonOutputFormatter>();
               })
               .AddPartialJsonFormatters();

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMediatR(typeof(AssemblyMaker));
            services.AddScoped<IMediator, SynchronousMediator>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            //services.AddTransient<IValidatorFactory, FluentValidatorFactory>();

            services.Configure<ConfigurationManager>(Configuration);

            // configure jwt authentication
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("JwtSettings:JwtPublicKey"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer")
                };
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(sw =>
            {
            //     sw.DescribeAllEnumsAsStrings();
            //     sw.DescribeStringEnumsInCamelCase();
            //     sw.DescribeAllParametersInCamelCase();

            //    // sw.SchemaFilter<FluentValidationSchemaFilter>();
            //     sw.SchemaFilter<DataTypeSchemaFilter>();

            //     sw.MapType<Guid>(() => new Schema
            //     {
            //         Type = "string",
            //         Format = "uuid",
            //         Example = Guid.NewGuid().ToString(),
            //         Pattern = "([0-9][a-f][A-F]){8}-(([0-9][a-f][A-F]){4}-){3}([0-9][a-f][A-F]){12}"
            //     });

                sw.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Innowave FreedomAdmin API",
                    Description = "FreedomAdmin",
                    Contact = new Contact { Name = "Innowave Healthcare Pvt Ltd,.", Email = "support@innowave.in" }
                });

            });

            services.AddScoped<ICryptographyHelper, CryptographyHelper>(cryptographyHelper => new CryptographyHelper
              (() => new AesManaged(),
               Configuration["EncryptionKey"],
               Configuration["VectorKey"]
               ));

            services.AddScoped<IValidator<ConnectorCommand>, ConnectorValidation>();
            services.AddScoped<IValidator<GetMenusByUsersCommand>, GetMenusByUsersValidation>();
            services.AddScoped<IValidator<GetAllProductsCommand>, GetAllProductsValidation>();
            services.AddScoped<IValidator<CreateNewBusinessCommand>, CreateNewBusinessValidation>();
            services.AddScoped<IValidator<GetAllBusinessQuery>, GetAllBusinessValidation>();
            services.AddScoped<IValidator<GetBusinessByIdQuery>, GetBusinessByIdValidation>();
            services.AddScoped<IValidator<ActivateBusinessCommand>, ActivateBusinessValidation>();
            services.AddScoped<IValidator<DeActivateBusinessCommand>, DeActivateBusinessValidation>();
            services.AddScoped<IValidator<ProductByBusinessCommand>, ProductByBusinessValidation>();
            services.AddScoped<IValidator<UpdateAddressCommand>, UpdateAddressValidation>();


            services.AddScoped<IUnitOfWork, UnitOfWork>(ctx => new UnitOfWork(Configuration["ConnectionStrings:ConnectionString"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStatusCodePages();

            app.UseCors(builder => builder.WithOrigins(Configuration.GetSection("AllowedOrigins").Get<string[]>()).AllowAnyHeader().AllowAnyMethod());

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddLog4Net(Configuration.GetValue<string>("Log4NetConfigFile:Name"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Innowave.FreedomAdmin.Api - V1");
            });
        }
    }
}
