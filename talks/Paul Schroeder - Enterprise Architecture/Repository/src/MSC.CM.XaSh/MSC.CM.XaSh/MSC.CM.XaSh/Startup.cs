using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using MSC.CM.Xam.ModelObj.CM;
using MSC.CM.XaSh.Helpers;
using MSC.CM.XaSh.Services;
using MSC.CM.XaSh.ViewModels;
using Polly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;

namespace MSC.CM.XaSh
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static string ExtractAppSettings(string filename)
        {
            var location = FileSystem.CacheDirectory;
            string full = null;
            var a = Assembly.GetExecutingAssembly();
            using (var resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream != null)
                {
                    full = Path.Combine(location, filename);

                    using (var stream = File.Create(full))
                    {
                        resFilestream.CopyTo(stream);
                    }
                }
            }

            return full;
        }

        public static void Init()
        {
            var configLocation = ExtractAppSettings("MSC.CM.XaSh.appsettings.json");

            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    c.AddCommandLine(new String[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    c.AddJsonFile(configLocation);
                })
                .ConfigureServices((c, x) => ConfigureServices(c, x))
                .ConfigureLogging(l => l.AddConsole(abc =>
                {
                    abc.DisableColors = true;
                }))
                .Build();

            ServiceProvider = host.Services;
        }

        public static void InitForSwitchToAPIData()
        {
            App.UseSampleDataStore = false;
            ServiceProvider = null;
            Startup.Init();
        }

        public static void InitForSwitchToSampleData()
        {
            App.UseSampleDataStore = true;
            ServiceProvider = null;
            Startup.Init();
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddHttpClient(Consts.UNAUTHORIZED, client =>
            {
                client.BaseAddress = new Uri(App.AzureBackendUrl);
            }).ConfigureHttpClient(client =>
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("api-version", "1"); // Not needed for token auth calls, but needed for heartbeat check.
            })
            .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            }));

            services.AddHttpClient(Consts.AUTHORIZED, client =>
            {
                client.BaseAddress = new Uri(App.AzureBackendUrl);
            }).ConfigureHttpClient(client =>
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("api-version", "1");

                string jwt = AuthenticationHelper.GetToken(); // This gets configured after user login.
                System.Net.Http.Headers.AuthenticationHeaderValue authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);
                client.DefaultRequestHeaders.Authorization = authorization; // client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
            })
            .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            }));

            if (ctx.HostingEnvironment.IsDevelopment() && App.UseSampleDataStore)
            {
                //load viewmodels directly from sample data
                //services.AddSingleton<IDataStore, SampleDataStore>();

                //load SQLite from Sample Data
                services.AddSingleton<IDataStore, SQLiteDataStore>();
                services.AddSingleton<IDataLoader, SampleDataLoader>();
                services.AddSingleton<IDataUploader, SampleDataUploader>();
            }
            else
            {
                //load SQLite from cloud based API
                services.AddSingleton<IDataStore, SQLiteDataStore>();
                services.AddSingleton<IDataLoader, AzureDataLoader>();
                services.AddSingleton<IDataUploader, AzureDataUploader>();
            }

            services.AddTransient<AboutViewModel>(); //viewmodel are created new, everytime
            services.AddTransient<AnnouncementsViewModel>();
            services.AddTransient<FeedbackViewModel>();
            services.AddTransient<GeneralInfoViewModel>();
            services.AddTransient<LocalWeatherViewModel>();
            services.AddTransient<MyFavoritesViewModel>();
            services.AddTransient<MyProfileViewModel>();
            services.AddTransient<SessionsByRoomViewModel>();
            services.AddTransient<SessionsByTimeViewModel>();
            services.AddTransient<SpeakerViewModel>();
            services.AddTransient<WorkshopsViewModel>();

            services.AddTransient<MyProfileEditViewModel>();

            //services.AddTransient<MainPage>();

            services.AddSingleton<App>(); //App is a singleton
        }
    }
}