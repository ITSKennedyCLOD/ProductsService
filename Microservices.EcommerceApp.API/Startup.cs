using Microservices.EcommerceApp.ApplicationCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using Microservices.EcommerceApp.ApplicationCore.Repositories;
using Microservices.EcommerceApp.API.Consumer.ClientConsumers;
using Microservices.EcommerceApp.API.Consumer.Order;

namespace Microservices.EcommerceApp.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.EcommerceApp.API", Version = "v1" });
            });

            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IReviewRepository, ReviewRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IClientRepository, ClientRepository>();

            services.AddMassTransit(
                x =>
                {

                    x.AddConsumer<CreateClientConsumer>();
                    x.AddConsumer<DeleteClientConsumer>();
                    x.AddConsumer<UpdateClientConsumer>();
                    x.AddConsumer<NewOrderConsumer>();
                    x.AddConsumer<DeleteOrderConsumer>();
                    x.AddConsumer<UpdateOrderConsumer>();

                    x.UsingRabbitMq((context, config) =>
                    {
                        

                        config.Host(
                            "roedeer.rmq.cloudamqp.com",
                            "vpeeygzh",
                            credential =>
                            {
                                credential.Username("vpeeygzh");
                                credential.Password("t0mDd3KRsJkXRV3DXzmCUfRWmDFbFu42");
                            }
                        );

                        config.ConfigureEndpoints(context);

                        config.ReceiveEndpoint("CreateClientCommands", e =>
                        {
                            e.Consumer<CreateClientConsumer>(context);

                        });


                        config.ReceiveEndpoint("DeleteClientCommands", e =>
                        {
                            e.Consumer<DeleteClientConsumer>(context);

                        });


                    

                        config.ReceiveEndpoint("UpdateClientCommands", e =>
                        {
                            e.Consumer<UpdateClientConsumer>(context);

                        });

                        config.ReceiveEndpoint("CreateOrderCommands", e =>
                        {
                            e.Consumer<NewOrderConsumer>(context);

                        });

                        config.ReceiveEndpoint("DeleteOrderCommands", e =>
                        {
                            
                            e.Consumer<DeleteOrderConsumer>(context);

                        });

                        config.ReceiveEndpoint("UpdateOrderCommands", e =>
                        {
                            e.Consumer<UpdateOrderConsumer>(context);

                        });

                    });
                }
            );

            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.EcommerceApp.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
