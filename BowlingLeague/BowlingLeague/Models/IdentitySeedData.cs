﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models
{
    public static class IdentitySeedData
    {
        //Create a Admin User
        private const string adminUser = "Admin";
        private const string adminPassword = "413ExtraYeetPeriod(t)!";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            //Build an instance
            AppIdentityDBContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<AppIdentityDBContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(adminUser);

            if (user == null)
            {
                //Add additional Admin Settings
                user = new IdentityUser(adminUser);

                user.Email = "admin@yeet.com";
                user.PhoneNumber = "555-1234";

                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
