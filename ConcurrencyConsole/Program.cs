using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ConcurrencyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = null;
            var host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configuration = configHost.Build();
                }).Build();

            var conn = configuration.GetValue<string>("ConnectString");

            Console.WriteLine(conn);


            //using (var context = new PersonContext())
            //{
            //    context.Database.EnsureDeleted();
            //    context.Database.EnsureCreated();

            //    context.Persons.Add(new Person
            //    {
            //        FirstName = "John",
            //        LastName = "Doe"
            //    });

            //    context.SaveChanges();
            //}

            using (var context = new PersonContext())
            {
                var person = context.Persons.Single(p => p.PersonId == 1);
                person.PhoneNumber = "555-555-5555";

                context.Database.ExecuteSqlCommand(@"
                    UPDATE Persons SET FirstName = 'Jane'                   
                    WHERE PersonId =1
                    ");

                var saved = false;
                while (!saved)
                {
                    try
                    {
                        context.SaveChanges();
                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        foreach (var entry in e.Entries)
                        {
                            if (entry.Entity is Person)
                            {
                                var proposedValues = entry.CurrentValues;
                                var databaseValues = entry.GetDatabaseValues();

                                foreach (var property in proposedValues.Properties)
                                {
                                    var proposedValue = proposedValues[property];
                                    var databaseValue = databaseValues[property];


                                }

                                entry.OriginalValues.SetValues(databaseValues);
                            }
                            else
                            {
                                throw new NotSupportedException(
                                    "Don't know to handle concurrency conflicts for" + entry.Metadata.Name);
                            }
                        }
                    }
                }
            }

            host.Run();

            
        }
    }

    public class PersonContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\projects;Database=EFGetStarted.NewDb;Integrated Security=true;");
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class Person
    {
        public int PersonId { get; set; }

        [ConcurrencyCheck]
        public string FirstName { get; set; }

        [ConcurrencyCheck]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
