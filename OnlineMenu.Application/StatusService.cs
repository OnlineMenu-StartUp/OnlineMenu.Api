using System.Collections.Generic;
using OnlineMenu.Domain;

namespace OnlineMenu.Application
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
            _ctx.Statuses.Add(status);
            _ctx.SaveChanges();
            return status;
        }
    }
}