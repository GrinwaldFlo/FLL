using FLL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FLL.Services
{
    public class DataService
    {
        private IServiceProvider ServiceProvider { get; }

        public ApplicationDbContext Db
        {
            get
            {
                try
                {
                    return ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext();
                }
                catch (Exception ex)   // If there is an issue, takes the last available
                {
                    Serilog.Log.Error(ex, "Get ApplicationDbContext");
                    throw;
                }                
            }
        }

        public DataService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task AddContest(Contest contest)
        {
            var db = Db;
            if (db == null)
                throw new Exception("Failed to connect to the database");
            await db.Contests.AddAsync(contest);
            await db.SaveChangesAsync();
        }

        internal (Contest?, Data.Types.AccessLevels) FindContest(string guid, string contestName)
        {
           if(!Guid.TryParse(guid, out Guid guidNeedle))
                return (null, Data.Types.AccessLevels.None);

            var foundContest = Db?.Contests.AsNoTracking().FirstOrDefault(x => x.ShortName == contestName && (x.AdminGuid == guidNeedle || x.UserGuid == guidNeedle || x.ViewGuid == guidNeedle));

            if(foundContest == null)
                return (null, Data.Types.AccessLevels.None);

            return (foundContest, foundContest.GetAccessLevel(guidNeedle));
        }

        internal async Task<bool> ContestExists(string shortName)
        {
            return await Db.Contests.AnyAsync(x => x.ShortName == shortName);
        }
    }
}
