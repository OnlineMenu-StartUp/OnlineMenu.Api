using System.Collections.Generic;
using System.Linq;
using OnlineMenu.Domain;
using OnlineMenu.Domain.Exceptions;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application
{
    public class StatusService
    {
        private readonly IOnlineMenuContext context;

        public StatusService(IOnlineMenuContext context)
        {
            this.context = context;
        }
        
        public IEnumerable<Status> GetStatuses()
        {
            return context.Statuses;
        }

        public Status CreateStatus(Status status)
        {
            var alreadyExists = context.Statuses.Any(s => s.Name == status.Name);
            
            if (alreadyExists)
            {
                throw new BadValueException($"The value [{status.Name}] already exists");
            }
            
            var savedStatus = context.Statuses.Add(status); 
            context.SaveChanges();
            
            return savedStatus.Entity;
        }
    }
}