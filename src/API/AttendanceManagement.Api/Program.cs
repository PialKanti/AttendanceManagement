using AttendanceManagement.Api.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string corsPolicyName = "CorsPolicy";
builder.Services.AddCorsPolicy(corsPolicyName);

builder.Services.AddControllers();
builder.Services.AddApiVersioningServices();

builder.Services.AddCookiePolicy(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

AttendanceManagement.Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddConfigurationOptions(builder.Configuration);

builder.Services.AddAutoMapper();

builder.Services.AddAuthServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenerator();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
