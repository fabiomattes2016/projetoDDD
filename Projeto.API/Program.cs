using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Projeto.API.Models.Authentication;
using Projeto.API.Models.Messages;
using Projeto.Domain.Interfaces.Generics;
using Projeto.Domain.Interfaces.Messages;
using Projeto.Domain.Interfaces.Services.Messages;
using Projeto.Domain.Services.Messages;
using Projeto.Entities.Entities.Authentication;
using Projeto.Entities.Entities.Messages;
using Projeto.Infrastructure.Configuration;
using Projeto.Infrastructure.Repository.Generics;
using Projeto.Infrastructure.Repository.Repositories.Messages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config Services
builder.Services.AddDbContext<ContextBase>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ContextBase>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Config Interface e Repo
builder.Services.AddSingleton(typeof(IGeneric<>), typeof(GenericRepository<>));
builder.Services.AddSingleton<IMessage, MessageRepository>();

// Config Domain
builder.Services.AddSingleton<IMessageService, MessageService>();

// Config JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(option =>
      {
          option.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = false,
              ValidateAudience = false,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,

              ValidIssuer = "Projeto.Securiry.Bearer",
              ValidAudience = "Projeto.Securiry.Bearer",
              IssuerSigningKey = JwtSecurityKey.Create("634746795957356E59584A705933563061584A7062576C79636E5668636D383D")
          };

          option.Events = new JwtBearerEvents
          {
              OnAuthenticationFailed = context =>
              {
                  Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                  return Task.CompletedTask;
              },
              OnTokenValidated = context =>
              {
                  Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                  return Task.CompletedTask;
              }
          };
      });

// Config AutoMapper
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<MessageViewModel, Message>();
    cfg.CreateMap<Message, MessageViewModel>();
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//var devClient = "http://localhost:3000";
//var urlHML = "https://dominiodocliente2.com.br";
//var urlPROD = "https://dominiodocliente3.com.br";

app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.UseSwaggerUI();

app.Run();
