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
using Microsoft.EntityFrameworkCore;
using QuizService.Data;
using QuizService.Models;

namespace QuizService {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuizService", Version = "v1" });
            });

            services.AddDbContext<QuizContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("QuizContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuizService v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope()) {
                serviceScope.ServiceProvider
                    .GetService<QuizContext>()
                    .Database
                    .Migrate();
                serviceScope.ServiceProvider
                    .GetService<QuizContext>().Add(new Quiz() {
                        Questions = new List<Question>() {
                            new Question() {
                                Text = "How many programmers does it take to write a test?",
                                Answers = new List<Answer>() {
                                    new Answer() {
                                        Text = "1",
                                        IsCorrect = false
                                    },
                                    new Answer() {
                                        Text = "2",
                                        IsCorrect = false
                                    },
                                    new Answer() {
                                        Text = "3",
                                        IsCorrect = false
                                    },
                                    new Answer() {
                                        Text = "Out of range exception",
                                        IsCorrect = true
                                    }
                                }
                            },
                            new Question() {
                                Text = "How many programmers does it take to write a test?",
                                Answers = new List<Answer>() {
                                    new Answer() {
                                        Text = "1",
                                        IsCorrect = false
                                    },
                                    new Answer() {
                                        Text = "2",
                                        IsCorrect = false
                                    },
                                    new Answer() {
                                        Text = "3",
                                        IsCorrect = false
                                    },
                                    new Answer() {
                                        Text = "Out of range exception",
                                        IsCorrect = true
                                    }
                                }
                            },
                            new Question() {
                                Text = "How many programmers does it take to write a test?",
                                Answers = new List<Answer>() {
                                    new Answer() {
                                        Text = "1",
                                        IsCorrect = false
                                    },
                                    new Answer() {
                                        Text = "2",
                                        IsCorrect = false
                                    },
                                    new Answer() {
                                        Text = "3",
                                        IsCorrect = false
                                    },
                                    new Answer() {
                                        Text = "Out of range exception",
                                        IsCorrect = true
                                    }
                                }
                            }
                        }
                    });
                serviceScope.ServiceProvider
                    .GetService<QuizContext>().SaveChanges();
            }
        }
    }
}
