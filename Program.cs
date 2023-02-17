using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Art_Gallery.Models;
using Art_Gallery.DbContextClasses;
using Art_Gallery.Interface;
using Art_Gallery.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GallaryDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddAuthentication(Options=>{
    Options.DefaultScheme=CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options=>{
    options.LoginPath="/account/facebook-login";
}).AddFacebook(options=>{
    options.AppId="AppId";
    options.AppSecret="AppSecrete";
});
builder.Services.AddAuthentication(authenoption =>
{
    authenoption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authenoption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwtoptions =>
    {

        var Key = builder.Configuration.GetValue<string>("JwtConfig:Key");
        var KeyBytes = Encoding.ASCII.GetBytes(Key);
        jwtoptions.SaveToken = true;
        jwtoptions.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(KeyBytes),
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            ClockSkew=TimeSpan.Zero
        };
    });
   
   builder.Services.AddDbContext<GallaryDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
    builder.Services.AddScoped(typeof(Ilogin), typeof(LoginRepository));
    builder.Services.AddScoped<Iregistration,RegistrationRepository>();
    builder.Services.AddScoped<Iexhibation,ExhibationRepository>();
    builder.Services.AddScoped<Iartist,ArtRepository>();
    builder.Services.AddScoped<Iadmin,AdminRepository>();
    builder.Services.AddScoped<Ibuyer,BuyerRepository>();
   // builder.Services.AddScoped<Ilogin,LoginRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
