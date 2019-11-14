using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestApi.Data.Models;
using RestApi.DatabaseAccess.Connectors;
using RestApi.DatabaseAccess.Context;
using RestApi.Interfaces;
using RestApi.Services;

namespace RestApi
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
            services.AddCors(setup =>
            {
                setup.AddDefaultPolicy(policy => policy.AllowAnyOrigin());
            });
            services.AddTransient<IVocabularyService, VocabularyService>();
            services.AddTransient<IConnector, EFConnector>();
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<VocabularyContext>();
            services.AddMvc();
            services.AddControllers();
            services.AddDbContext<VocabularyContext>(x =>
            {
                //x.UseSqlServer(@"Data Source=DUCH003\TOLEARNINSTANCE;Initial Catalog=TestDb;Integrated Security=True");
                x.UseInMemoryDatabase("TestDb");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            Seed();
        }

        private void Seed()
        {
            var entries = new List<Sentence>();
            entries.Add(new Sentence
            {
                ID = 0,
                Foreign = "House",
                Primary = "Dom",
                Description = "The place where person lives daily.",
                Examples = "My home is where's my bed is.",
                LevelOfRecognition = 0.7,
                Subject = "Daily",
                Source = "Dictinary",
                AttemptsLeft = 0
            });
            entries.Add(new Sentence
            {
                ID = 0,
                Foreign = "Spoon",
                Primary = "Łyżka",
                Description = "A thing often used to eat soups and liquid dishes.",
                Examples = "Sorry! I have spilled some soup from my spoon!;Spoons are Cadabra's main attribute.",
                LevelOfRecognition = 0.23,
                Subject = "Kitchen",
                Source = "Dictinary",
                AttemptsLeft = 0
            });
            entries.Add(new Sentence
            {
                ID = 0,
                Foreign = "Fork",
                Primary = "Widelec",
                Description = "A thing often used to eat solid dishes.",
                Examples = "I have bought a bunch of new forks.",
                LevelOfRecognition = 1,
                Subject = "Kitchen",
                Source = "Dictinary",
                AttemptsLeft = 0
            });
            entries.Add(new Sentence
            {
                ID = 0,
                Foreign = "Oven",
                Primary = "Piekarnik",
                Description = "Baking machine.",
                Examples = "I have buns in the oven.",
                LevelOfRecognition = 0.34,
                Subject = "Kitchen",
                Source = "Dictinary",
                AttemptsLeft = 0
            });
            entries.Add(new Sentence
            {
                ID = 0,
                Foreign = "Fridge",
                Primary = "Lodówka",
                Description = null,
                Examples = null,
                LevelOfRecognition = 0.92,
                Subject = "Kitchen",
                Source = "Dictinary",
                AttemptsLeft = 0
            });
            entries.Add(new Sentence
            {
                ID = 0,
                Foreign = "Table",
                Primary = "Stół",
                Description = "During sunday's dinners the whole family met sitting in front of that.",
                Examples = "Can you set the table, please?;This table is built of mahogany.",
                LevelOfRecognition = 0,
                Subject = "Kitchen",
                Source = "Your house in a nutshell.",
                AttemptsLeft = 0
            });
            entries.Add(new Sentence
            {
                ID = 0,
                Foreign = "Table",
                Primary = "Biurko",
                Description = "A furniture designed to work on it.",
                Examples = "He is doing he's homework on the table.",
                LevelOfRecognition = 0.1,
                Subject = "Office",
                Source = "Your house in a nutshell.",
                AttemptsLeft = 0
            });
            entries.Add(new Sentence
            {
                ID = 0,
                Foreign = "Computer",
                Primary = "Komputer",
                Description = null,
                Examples = "I am software engineer so I am working on computers.",
                LevelOfRecognition = 0.12,
                Subject = "Office",
                Source = "Daily work",
                AttemptsLeft = 0
            });
            entries.Add(new Sentence
            {
                ID = 0,
                Foreign = "Chair",
                Primary = "Krzesło",
                Description = "Furtniture on which people are sitting.",
                Examples = "Look, how I restored this chair. Is not it beautiful?;Oh man, I was really. I needed to sit for a while.",
                LevelOfRecognition = 0.87,
                Subject = "Office",
                Source = "Daily work",
                AttemptsLeft = 0
            });
            entries.Add(new Sentence
            {
                ID = 0,
                Foreign = "Dinning room",
                Primary = "Jadalnia",
                Description = "Part of house where people eat's their food.",
                Examples = null,
                LevelOfRecognition = 0.5,
                Subject = "Kitchen",
                Source = "Your house in a nutshell.",
                AttemptsLeft = 0
            });
            var context = new VocabularyContext();
            context.AddRange(entries);
            context.SaveChanges();
        }
    }
}
