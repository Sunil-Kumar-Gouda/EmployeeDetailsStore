using EmployeeDetailStore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetailStore.Api.StartupConfiguration
{
    public static class AddData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EmployeeDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<EmployeeDbContext>>()))
            {
                //context.Database.Migrate();
                context.Database.EnsureCreated();
                // Check if there are any records in the database
                if (context.Employees.Any())
                {
                    // The database has already been seeded
                    return;
                }

                // Adding some employee
                context.Employees.Add(new Model.DatabaseModels.Employee { FirstName = "Sunil", LastName = "Kumar", Email = "sunilabcdefggouda@domain.com", Age = 28, Gender = "Male" });
                context.Employees.Add(new Model.DatabaseModels.Employee { FirstName = "John", LastName = "Smith", Email = "johnsmithabcef1234@domain.com", Age = 31, Gender = "male" });
                context.Employees.Add(new Model.DatabaseModels.Employee { FirstName = "Monica", LastName = "Mercer", Email = "monicamerceruiasfh1354@domain.com", Age = 39, Gender = "Female" });
                context.Employees.Add(new Model.DatabaseModels.Employee { FirstName = "Rachel", LastName = "Green", Email = "rachelgreen@domain.com", Age = 27, Gender = "female" });
                context.SaveChanges();
            }
        }
    }
}
