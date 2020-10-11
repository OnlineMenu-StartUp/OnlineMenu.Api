using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Application;
using OnlineMenu.Application.Dto;
using OnlineMenu.Application.Services;
using OnlineMenu.Domain;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly StatusService _statusService;

        public StatusController(StatusService statusService, IMapper mapper)
        {
            _statusService = statusService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<StatusDto>> GetStatusList()
        {
            var statuses = _statusService.GetStatuses();
            var statusResList = statuses.Select(status => _mapper.Map<StatusDto>(status)).ToList();

            return Ok(statusResList);
        }
        
        [HttpPost]
        public ActionResult<Status> CreateStatus(StatusDto status)
        {
            var createdStatus = _statusService.CreateStatus(_mapper.Map<Status>(status));
            return Ok(_mapper.Map<StatusDto>(createdStatus));
        }
    }
}