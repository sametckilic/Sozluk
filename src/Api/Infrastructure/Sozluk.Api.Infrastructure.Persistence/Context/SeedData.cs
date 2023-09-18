using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Infrastructure.Persistence.Context
{
    public class SeedData
    {
        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.FirstName, i => i.Person.FirstName)
                .RuleFor(i => i.LastName, i => i.Person.LastName)
                .RuleFor(i => i.Email, i => i.Internet.Email())
                .RuleFor(i => i.UserName, i => i.Internet.UserName())
                .RuleFor(i => i.Password, i => PasswordEncrypter.Encrypt(i.Internet.Password()))
                .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
                .Generate(500);

            return result;
        }

        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();

            dbContextBuilder.UseSqlServer(configuration["SozlukDbConnectionString"]);

            var context = new SozlukDboContext(dbContextBuilder.Options);

            var users = GetUsers();

            var userIds = users.Select(i => i.Id);

            await context.Users.AddRangeAsync(users);

            var guid = Enumerable.Range(0, 150).Select(i => Guid.NewGuid()).ToList();
            int counter = 0;

            var entries = new Faker<Entry>("tr")
                .RuleFor(i => i.Id, i => guid[counter++])
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Subject, i => i.Lorem.Sentence(3, 5))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .Generate(150);

            await context.AddRangeAsync(entries);

            var comments = new Faker<EntryComment>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(1))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .RuleFor(i => i.EntryId, i => i.PickRandom(guid))
                .Generate(1000);

            await context.AddRangeAsync(comments);

            await context.SaveChangesAsync();
        }
    }
}
