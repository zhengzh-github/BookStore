using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BookStore.EntityFrameworkCore;
using BookStore.MultiTenancy;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.Security.Claims;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;

namespace BookStore
{
    [DependsOn(
        typeof(BookStoreApplicationModule),
        typeof(BookStoreEntityFrameworkCoreModule),
        typeof(BookStoreHttpApiModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule)
        )]
    public class BookStoreHttpApiHostModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<BookStoreDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}BookStore.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<BookStoreDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}BookStore.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<BookStoreApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}BookStore.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<BookStoreApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}BookStore.Application", Path.DirectorySeparatorChar)));
                });
            }

            //context.Services.AddSwaggerGen(
            //    options =>
            //    {
            //        options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore API", Version = "v1" });
            //        options.DocInclusionPredicate((docName, description) => true);
            //        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //        {
            //            Description = "JWT??????(????????????????????????????????????) ????????????????????????Bearer {token}???????????????????????????????????????\"",
            //            Name = "Authorization",//jwt?????????????????????
            //            In = ParameterLocation.Header,//jwt????????????Authorization???????????????(????????????)
            //            Type = SecuritySchemeType.ApiKey
            //        });
            //        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            //        {
            //            { new OpenApiSecurityScheme
            //                {
            //                    Reference = new OpenApiReference()
            //                    {
            //                        Id = "Bearer",
            //                        Type = ReferenceType.SecurityScheme
            //                    }
            //                }, Array.Empty<string>()
            //            }
            //        });
            //        var xmlapipath = Path.Combine(AppContext.BaseDirectory, "BookStore.HttpApi.xml");
            //        if (File.Exists(xmlapipath))
            //        {
            //            options.IncludeXmlComments(xmlapipath, true);
            //        }
            //        var xmlapppath = Path.Combine(AppContext.BaseDirectory, "BookStore.Application.Contracts.xml");
            //        if (File.Exists(xmlapipath))
            //        {
            //            options.IncludeXmlComments(xmlapppath, true);
            //        }

            //        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "BookStore.Application.xml"),true);
            //    });


            context.Services.AddAbpSwaggerGenWithOAuth(
                configuration["AuthServer:Authority"],
                new Dictionary<string, string>
                {
                    {"BookStore", "BookStore API"}
                },
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                    //var basePath = AppContext.BaseDirectory;

                    //var xmlPath = Path.Combine(basePath, "BookStore.Application.xml");
                    //options.IncludeXmlComments(xmlPath, true);
                });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "????????????"));
            });

            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "BookStore";
                });

            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "BookStore:";
            });

            //if (!hostingEnvironment.IsDevelopment())
            //{
            //    var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            //    context.Services
            //        .AddDataProtection()
            //        .PersistKeysToStackExchangeRedis(redis, "BookStore-Protection-Keys");
            //}

            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }
            app.UseAbpRequestLocalization();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");

                //var configuration = context.GetConfiguration();
                //options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                //options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
                //options.OAuthScopes("BookStore");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
