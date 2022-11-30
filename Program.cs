using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MikesCars.Interfaces;
using MikesCars.Repositories;

var builder = WebApplication.CreateBuilder(args);
var MikesCars = "_mikescars";

var firebaseProjectId = builder.Configuration.GetValue<string>("Authentication:Firebase:ProjectId");
var googleTokenUrl = $"https://securetoken.google.com/{firebaseProjectId}";
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = googleTokenUrl;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = googleTokenUrl,
            ValidateAudience = true,
            ValidAudience = firebaseProjectId,
            ValidateLifetime = true
        };
    });

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MikesCars,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7057",
                                             "http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                          .WithMethods("GEt", "POST", "PUT", "DELETE")
                          .WithExposedHeaders("*");
                      });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<IFactRepository, FactRepository>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();
builder.Services.AddTransient<IListingRepository, ListingRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IFavoriteRepository, FavoriteRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MikesCars);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
