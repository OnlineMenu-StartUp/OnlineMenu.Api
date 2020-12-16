using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Application.Services;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Api.Controllers
{
    public class StatusController: AppBaseController
    {
        private readonly IMapper mapper;
        private readonly StatusService statusService;

        public StatusController(StatusService statusService, IMapper mapper)
        {
            this.statusService = statusService;
            this.mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatusList()
        {
            return Ok(await statusService.GetStatuses());
        }
        
        [HttpPost]
        public async Task<ActionResult<Status>> CreateStatus(string statusName)
        {   
            var createdStatus = await statusService.CreateStatus(statusName);
            var createdStatusUrl = $"{Request.GetDisplayUrl()}/{createdStatus.Id}";
            return Created(createdStatusUrl ,mapper.Map<Status>(createdStatus));
        }
    }
}