
using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.ClassProject;
 using Microcredit.ClassProject.CustomersSVC;
 using Microcredit.ClassProject.EmployeeSVC;
 using Microcredit.ClassProject.ProductsSVC;
 using Microcredit.GETErr;
using Microcredit.Reports.ExecuteSP;
 using Microcredit.Services.AddNewLonaSVC;
using Microcredit.Services.InterestRateSVC;
using Microcredit.Services.PaymentOfistallmentsSVC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Build.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ModelService;
using System.Reflection;
using System.Text;

namespace Microcredit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public enum ServiceType
        {
            ExecutepaymentOfistallmentsbetweenDateReport,
            AllIssuanceLoansReport,
             
        }
        public delegate IExecuteReport ServiceResolver(ServiceType serviceType);

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            #region CONNECTION MIGRATION
            //services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));

            services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("MicrocreditTOCon"), x => x.MigrationsAssembly("Microcredit")));

            services.AddIdentity<Appuser, IdentityRole>(opt => { }).AddEntityFrameworkStores<ApplicationDbContext>();


            //     services.AddDbContext<DataProtectionKeysContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DataProtectionKeysContextCon"), x => x.MigrationsAssembly("Microcredit")));
            #endregion

            #region Services
             services.AddTransient<IProducts, ProductsSVC>();
            services.AddTransient<ICustomers, CustomersSVC>();
             services.AddTransient<IEmployee, EmployeeSVC>();
            services.AddTransient<IAddNewLona, AddNewLonaSVC>();
            services.AddTransient<IInterestRate, InterestRateSVC>();
            services.AddTransient<IPaymentOfistallments, PaymentOfistallmentsSVC>();
            services.AddTransient<IExecuteReport, ExecutAllInfoAboutcustomerReport>();
           
            services.AddTransient<IExecuteReportWithmultipleParam, ExecutepaymentOfistallmentsbetweenDateReport>();
             
            services.AddTransient<IExecuteReportDuelmentsbetweenDate, ExecuteReportDuelmentsbetweenDate>();

            services.AddTransient<  IExecuteReportIssuanceLoansbetweenDate, ExecuteReportIssuanceLoansbetweenDate> ();

            //services.AddTransient<IExecuteReportDuelmentsbetweenDate, Exec>();

            services.AddTransient<IAllIStatusLoansReport, ExecuteAllIStatusLoansReport>();

            //services.AddScoped<IAllIssuanceLoansReport>();

            //services.AddTransient<ServiceResolver>(serviceProvider => serviceTypeName =>
            //{
            //    switch (serviceTypeName)
            //    {
            //        //case ServiceType.FileLogger:
            //        //    return serviceProvider.GetService<FileLogger>();
            //        //case ServiceType.DbLogger:
            //        //    return serviceProvider.GetService<DbLogger>();
            //        case ServiceType.AllIssuanceLoansReport:
            //            return serviceProvider.GetService<IExecuteReport>();
            //        default:
            //            return null;
            //    }
            //});


            services.AddTransient<IExecutCountCustomersReport, ExecutCountCustomersReport>();

             services.TryAddSingleton<IClientErrorFactory, ProblemDetailsErrorFactory>();
             services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
             services.AddTransient<IExecuteProducts, ExecuteProducts>();
            services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));

            #endregion

            /*                              DEFAULT IDENTITY OPTIONS                                             */
            /*---------------------------------------------------------------------------------------------------*/
            //var identityDefaultOptionsConfiguration = Configuration.GetSection("IdentityDefaultOptions");
            //services.Configure<IdentityDefaultOptions>(identityDefaultOptionsConfiguration);
            //var identityDefaultOptions = identityDefaultOptionsConfiguration.Get<IdentityDefaultOptions>();

            //services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            //{
            //    // Password settings
            //    options.Password.RequireDigit = identityDefaultOptions.PasswordRequireDigit;
            //    options.Password.RequiredLength = identityDefaultOptions.PasswordRequiredLength;
            //    options.Password.RequireNonAlphanumeric = identityDefaultOptions.PasswordRequireNonAlphanumeric;
            //    options.Password.RequireUppercase = identityDefaultOptions.PasswordRequireUppercase;
            //    options.Password.RequireLowercase = identityDefaultOptions.PasswordRequireLowercase;
            //    options.Password.RequiredUniqueChars = identityDefaultOptions.PasswordRequiredUniqueChars;

            //    // Lockout settings
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityDefaultOptions.LockoutDefaultLockoutTimeSpanInMinutes);
            //    options.Lockout.MaxFailedAccessAttempts = identityDefaultOptions.LockoutMaxFailedAccessAttempts;
            //    options.Lockout.AllowedForNewUsers = identityDefaultOptions.LockoutAllowedForNewUsers;

            //    // User settings
            //    options.User.RequireUniqueEmail = identityDefaultOptions.UserRequireUniqueEmail;

            //    // email confirmation require
            //    options.SignIn.RequireConfirmedEmail = identityDefaultOptions.SignInRequireConfirmedEmail;

            //}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            /*---------------------------------------------------------------------------------------------------*/


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {

                var key = Encoding.ASCII.GetBytes(Configuration["JWTConfig:Key"]);
                var issuer = Configuration["JWTConfig:Issuer"];
                var audience = Configuration["JWTConfig:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience

                };
            });


            services.AddApiVersioning(
         options =>
         {
             options.ReportApiVersions = true;
             options.AssumeDefaultVersionWhenUnspecified = true;
             options.DefaultApiVersion = new ApiVersion(1, 0);
         });
            /*---------------------------------------------------------------------------------------------------*/
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {


                builder.WithOrigins("http://localhost:8010").AllowAnyMethod().AllowAnyHeader();
            }));



            services.AddCors(); // Make sure you call this previous to AddMvc

            services.AddMvc();
            services.AddSession();
            services.AddMvc().AddControllersAsServices().AddRazorRuntimeCompilation().SetCompatibilityVersion(CompatibilityVersion.Latest);


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microcredit", Version = "v1" });
            });
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = ".NET Core 5 ", Version = "v1", Description = "This test description" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                     {
                         new OpenApiSecurityScheme{
                             Reference=new OpenApiReference  {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                             }
                         },
                         new string[]{}

                     }

              });

                
            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //c.IncludeXmlComments(xmlPath);
        });
    
        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microcredit v1"));
            }
    
            // Make sure you call this before calling app.UseMvc()
            //app.UseCors(
            //    options => options.WithOrigins("http://localhost:4200/").AllowAnyMethod()
            //);
            app.UseCors(builder => builder
          .AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
            //app.UseMvc();
            app.UseSession();  // Before UseMvc()

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
