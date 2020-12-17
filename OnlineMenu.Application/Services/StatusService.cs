using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Domain.Exceptions;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application.Services
{
    public class StatusService
    {
        private readonly IOnlineMenuContext context;

        public StatusService(IOnlineMenuContext context)
        {
            this.context = context;
        }
        
        public Task<List<Status>> GetStatuses()
        {
            return context.Statuses.ToListAsync();
        }

        public async Task<Status> CreateStatus(string statusName)
        {
            if (await context.Statuses.AnyAsync(s => s.Name == statusName))
            {
                throw new BadValueException($"The value [{statusName}] already exists");
            }
            
            var savedStatus = context.Statuses.Add(new Status{Name = statusName}); 
            await context.SaveChangesAsync();
            
            return savedStatus.Entity;
        }
    }
}