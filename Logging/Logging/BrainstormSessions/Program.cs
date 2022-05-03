using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;

namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var smtpInfo = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("smtp-info.json"));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File("Logs\\logs.log")
                .WriteTo.Email(new EmailConnectionInfo
                {
                    FromEmail = smtpInfo["username"],
                    ToEmail = smtpInfo["to"],
                    MailServer = smtpInfo["server"],
                    NetworkCredentials = new NetworkCredential
                    {
                        UserName = smtpInfo["username"],
                        Password = smtpInfo["password"]
                    },
                    EnableSsl = true,
                    Port = Int32.Parse(smtpInfo["port"]),
                    EmailSubject = smtpInfo["subject"]
                })
                .CreateLogger();

            Log.Information("Starting web host");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
