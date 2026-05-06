using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Data
{
    public static class TaskLookupSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!await context.Statuses.AnyAsync())
            {
                context.Statuses.AddRange(
                    new Status { Name = "Pending" },
                    new Status { Name = "In Progress" },
                    new Status { Name = "Completed" },
                    new Status { Name = "Cancelled" }
                );
            }

            if (!await context.Priorities.AnyAsync())
            {
                context.Priorities.AddRange(
                    new Priority { Name = "Low" },
                    new Priority { Name = "Medium" },
                    new Priority { Name = "High" },
                    new Priority { Name = "Urgent" }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}