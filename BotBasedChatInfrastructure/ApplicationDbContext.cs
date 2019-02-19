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
            string user1Email = configuration["SeedDataIdentity:user1:Email"];
            string user1Password = configuration["SeedDataIdentity:user1:Password"];
            string user1Profile = configuration["SeedDataIdentity:user1:Profile"];

            if (await userManager.FindByNameAsync(user1Email) == null)
            {
                var user1 = new UserIdentity();
                user1.UserName = user1Email;
                user1.Email = user1Email;

                var identityResult = await userManager.CreateAsync(user1, user1Password);
                if (!identityResult.Succeeded)
                    throw new Exception("first user was not created");

                
                await userManager.AddClaimAsync(user1, new System.Security.Claims.Claim("profile", user1Profile));
                                        
                

            }

            string user2Email = configuration["SeedDataIdentity:user2:Email"];
            string user2Password = configuration["SeedDataIdentity:user2:Password"];
            string user2Profile = configuration["SeedDataIdentity:user2:Profile"];

            if (await userManager.FindByNameAsync(user2Email) == null)
            {
                var user2 = new UserIdentity();
                user2.UserName = user2Email;
                user2.Email = user2Email;

                var identityResult = await userManager.CreateAsync(user2, user2Password);
                if (!identityResult.Succeeded)
                    throw new Exception("first user was not created");

                await userManager.AddClaimAsync(user2, new System.Security.Claims.Claim("profile", user2Profile));
            }

        }
    }
}
