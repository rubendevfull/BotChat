using BotBasedChatInfrastructure.ModelSecurity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotBasedChatInfrastructure
{
    public class ApplicationDbContext : IdentityDbContext<UserIdentity>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public static async Task SeedAccounts(UserManager<UserIdentity> userManager,
            IConfiguration configuration)
        {
            //string userName = configuration["SeedDataIdentity:superAdmin:Name"];
            //string email = configuration["SeedDataIdentity:superAdmin:Email"];
            //string password = configuration["SeedDataIdentity:superAdmin:Password"];

            //if (await userManager.FindByNameAsync(userName) == null)
            //{
            //    var superUser = new UserIdentity();
            //    superUser.UserName = userName;
            //    superUser.Email = email;

            //    var identityResult = await userManager.CreateAsync(superUser, password);
            //    if (!identityResult.Succeeded)
            //        throw new Exception("Super user was not created");
            //}

        }
    }
}
