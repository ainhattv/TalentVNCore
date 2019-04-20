using ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentVN.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext appDbContext, ILoggerFactory loggerFactory)
        {

            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!appDbContext.Accounts.Any())
                {
                    var accounts = new List<Account>()
                    {
                        new Account(){
                            AccountID = Guid.NewGuid().ToString(),
                            FirstName = "Nhat",
                            LastName = "Nhat",
                        },
                         new Account(){
                            AccountID = Guid.NewGuid().ToString(),
                            FirstName = "Nhi",
                            LastName = "Nhi",
                        },
                          new Account(){
                            AccountID = Guid.NewGuid().ToString(),
                            FirstName = "Tuan",
                            LastName = "Tuan",
                        }
                    };

                    await appDbContext.Accounts.AddRangeAsync(accounts);

                    await appDbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
