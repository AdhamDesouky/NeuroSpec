using Microsoft.EntityFrameworkCore;
using NeuroSpecBackend.Model;
using NeuroSpec.Shared.Models.HUB;
using Microsoft.SemanticKernel;
using NeuroSpecBackend.Services;


namespace NeuroSpecBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.Configure<NeuroDbContext>(builder.Configuration.GetSection("DatabaseSettings"));

            builder.Services.AddScoped<NeuroDbContext>();

            builder.Services.AddScoped<ChatbotService>();
            
            builder.Services.AddSignalR(); //chat

            var app = builder.Build();

            


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
