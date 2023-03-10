using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using StudentAdminPortal.API.Data;
using StudentAdminPortal.API.Repositories;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("angularApplication",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // note the port is included 
                .AllowAnyHeader()
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .WithExposedHeaders("*");
        });
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();


builder.Services.AddDbContext<StudentAdminContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentAdminPortalDb"));
});

builder.Services.AddScoped<IStudentRepository, SqlStudentRepository>();
builder.Services.AddScoped<IImageRepository, LocalStorageImageRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("angularApplication");

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions 
{ 
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Resources")),
    RequestPath = "/Resources"
});

app.UseAuthorization();

app.MapControllers();

app.Run();
