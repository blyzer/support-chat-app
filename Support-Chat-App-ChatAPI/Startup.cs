using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using upport_Chat_App.Managers.ChatSessionReceiver;
using upport_Chat_App.Managers.ChatSessionSender;
using Support_Chat_App.Data;
using Support_Chat_App.Data.Helpers;
using Support_Chat_App.Managers.IManagers;
using Support_Chat_App.Managers.Managers;
using Support_Chat_App.Repositories.Authorization;
using Support_Chat_App.Repositories.IRepositories;
using Support_Chat_App.Repositories.Repositories;
using System;
using System.IO;
using System.Reflection;

namespace Support_Chat_App_ChatAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SupportChatContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // configure DI for application services
            services.AddTransient<ITokenController, TokenController>();
            services.AddSingleton<IChatRepository, ChatRepository>();
            services.AddTransient<IChatManager, ChatManager>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IChatSessionCreationSender, ChatSessionCreationSender>();

            //Customerize the swagger ui
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Support Chat App - Chat",
                    Description = "An API to create the Chat sessions",
                    Contact = new OpenApiContact
                    {
                        Name = "Darshana Edirisinghe",
                        Email = "dharshanaedirisinghe@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/darshana-edirisinghe-08a0a110b/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //RabitMq configurations
            var rabbitMqSection = Configuration.GetSection("RabbitMq");
            var rabbitMqConfigurations = rabbitMqSection.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(rabbitMqSection);

            if (rabbitMqConfigurations.ConsumerEnabled)
            {
                services.AddHostedService<CreateChatSessionReceiver>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<TokenMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Register swagger ui
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
