using System.Collections.Generic;
using System.Linq;
using OnlineMenu.Domain.Exceptions;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application.Services
{
    public class StatusService
    {
        private readonly IOnlineMenuContext _ctx;

        public StatusService(IOnlineMenuContext ctx)
        {
            _ctx = ctx;
        }
        
        public IEnumerable<Status> GetStatuses()
        {
            return _ctx.Statuses;
        }

        public Status CreateStatus(Status status)
        {
            var alreadyExists = _ctx.Statuses.Any(s => s.Name == status.Name);
            if (alreadyExists)
            {
                throw new BadValueException($"The value [{status.Name}] already exists");
            }
            
            _ctx.Statuses.Add(status); 
            _ctx.SaveChanges();
            
            return status;
        }
    }
}