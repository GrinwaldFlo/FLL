using FLL.Data.Models;
using FLL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

namespace FLL.Server
{
    public class MainService : BackgroundService
    {
        private IServiceProvider ServiceProvider { get; }
        //private readonly EmailSender emailSender;
        //private readonly string path;
        //private bool NotificationSent = false;

        private ApplicationDbContext? db;

        public MainService(IServiceProvider serviceProvider)//, IConfiguration configuration, IOptions<SmtpSenderOptions> smtpOptions)
        {
            ServiceProvider = serviceProvider;
            //emailSender = new EmailSender(smtpOptions, serviceProvider);
            //path = configuration["MailLog"];

        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            db = ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext();
            if (db == null)
            {
                Log.Error("Failed to link to DB");
                return;
            }

            db.Database.Migrate();

            while (!stoppingToken.IsCancellationRequested)
            {
              
                await Task.Delay(10000, stoppingToken);
            }

        }
    }
}
