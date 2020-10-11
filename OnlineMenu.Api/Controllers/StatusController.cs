using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Application;
using OnlineMenu.Application.Dto;
using OnlineMenu.Domain;

namespace OnlineMenu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController: ControllerBase
    {
        private readonly IMapper mapper;
        private readonly StatusService statusService;

        public StatusController(StatusService statusService, IMapper mapper)
        {
            this.statusService = statusService;
            this.mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<StatusDto>> GetStatusList()
        {
            var statuses = statusService.GetStatuses();
            var statusResultList = statuses.Select(status => mapper.Map<StatusDto>(status)).ToList();

            return Ok(statusResultList);
        }
        
        [HttpPost]
        public ActionResult<StatusDto> CreateStatus(StatusDto status)
        {
            // The client shouldn't be able to provide an id. 
            // It is not the case to create a new Request DTO for Status entity
            status.Id = null;
            
            var createdStatus = statusService.CreateStatus(mapper.Map<Status>(status));
            var createdStatusUrl = $"{Request.GetDisplayUrl()}/{createdStatus.Id}";
            return Created(createdStatusUrl ,mapper.Map<StatusDto>(createdStatus));
        }
    }
}