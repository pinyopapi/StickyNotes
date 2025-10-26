using Microsoft.EntityFrameworkCore;
using StickyNotes.Application.Interfaces;
using StickyNotes.Application.Services;
using StickyNotes.Infrastructure.Persistence;
using StickyNotes.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace StickyNotes.Api;
[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp",
                policy => policy.WithOrigins("http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod());
        });

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<INoteRepository, NoteRepository>();
        builder.Services.AddScoped<INoteService, NoteService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowReactApp");
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}