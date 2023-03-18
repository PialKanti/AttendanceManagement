using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using AttendanceManagement.Api.Mappers.Formatters;
using AttendanceManagement.Api.Options;
using AttendanceManagement.Api.Repositories;
using AttendanceManagement.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AttendanceManagement.Api.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiVersioningServices(this IServiceCollection services)
        {
            services.AddApiVersioning(option =>
            {
                option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = true;
                option.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version"));
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.Cookie.Name = "X-Access-Token";
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtToken:ValidIssuer"],
                        ValidAudience = configuration["JwtToken:ValidAudience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    configuration[
                                        "JwtToken:Key"])) //Todo keep IssuerSigningKey in .env file or user secrets
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                            {
                                context.Token = context.Request.Cookies["X-Access-Token"];
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();

            return services;
        }

        public static IServiceCollection AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            services.Configure<IdentityOptions>(options =>
            {
                //User Settings
                options.User.RequireUniqueEmail = true;

                //Password Settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });


            services.Configure<JwtTokenOptions>(configuration.GetSection(JwtTokenOptions.JwtToken));

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository<ApplicationUser>, UsersRepository>();
            services.AddScoped<JwtTokenService, JwtTokenService>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<RegisterUserDto, ApplicationUser>()
                    .ForMember(destination => destination.BirthDate,
                        options => options.MapFrom(source =>
                            string.IsNullOrEmpty(source.BirthDate)
                                ? (DateTime?)null
                                : DateTime.Parse(source.BirthDate)));

                configuration.CreateMap<Attendance, AttendanceDto>()
                    .ForMember(destination => destination.Username,
                        options => options.MapFrom(source => (source.User != null) ? source.User.UserName : string.Empty))
                    .ForMember(destination => destination.EntryDateTime, options => options.ConvertUsing<TimestampToDateTimeFormatter, long?>(source => source.EntryTimestamp))
                    .ForMember(destination => destination.ExitDateTime, options => options.ConvertUsing<TimestampToDateTimeFormatter, long?>(source => source.ExitTimestamp));

                configuration.CreateMap<AttendanceCreateDto, Attendance>()
                    .ForMember(destination => destination.EntryTimestamp, options => options.ConvertUsing<DateTimeToTimestampFormatter, DateTime>(source => source.EntryDateTime))
                    .ForMember(destination => destination.EntryDate, options => options.MapFrom(source => source.EntryDateTime.Date))
                    .ForMember(destination => destination.Month, options => options.MapFrom(source => source.EntryDateTime.Month))
                    .ForMember(destination => destination.Year, options => options.MapFrom(source => source.EntryDateTime.Year));
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddSwaggerGenerator(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName, configurePolicy =>
                {
                    configurePolicy.WithOrigins("http://localhost:8080", "https://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            return services;
        }
    }
}
