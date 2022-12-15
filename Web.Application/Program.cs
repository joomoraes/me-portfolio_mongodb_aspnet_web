using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Web.Application.Domain.Security;

var builder = WebApplication.CreateBuilder(args);
var signingConfigurations = new SigningConfigurations();


builder.Services.AddSingleton<Web.Application.Data.MongoDB>();
builder.Services.AddScoped<Web.Application.Data.Repositories.UsersRepository>();
builder.Services.AddScoped<Web.Application.Data.Repositories.PostsRepository>();
builder.Services.AddSingleton(signingConfigurations);
Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
Environment.SetEnvironmentVariable("Issuer", "ExemploIssue");
Environment.SetEnvironmentVariable("Seconds", "28800");
builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( bearerOptions =>
{
    bearerOptions.SaveToken = true;
    var paramsValidation = bearerOptions.TokenValidationParameters;
    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
    paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
    paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");

    paramsValidation.ValidateIssuerSigningKey = true;
    paramsValidation.ValidateLifetime = true;
    paramsValidation.ClockSkew = TimeSpan.Zero;
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
