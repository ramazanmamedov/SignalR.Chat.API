﻿using System;
using System.Threading.Tasks;
using SignalR.Chat.API.Entities.Core;
using SignalR.Chat.API.Web.Mediator.LogsReadonly;
using Calabonga.Microservices.Core.QueryParams;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SignalR.Chat.API.Web.Controllers
{
    /// <summary>
    /// ReadOnlyController Demo
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class LogsReadonlyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogsReadonlyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        [Authorize(Policy = "Logs:UserRoles:View", Roles = AppData.SystemAdministratorRoleName)]
        public async Task<IActionResult> GetRoles()
        {
            //Get Roles for current user
            return Ok(await _mediator.Send(new GetRolesRequest(), HttpContext.RequestAborted));
        }

        [HttpGet("[action]/{id:guid}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new LogGetByIdRequest(id), HttpContext.RequestAborted));
        }


        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetPaged([FromQuery] PagedListQueryParams queryParams)
        {
            return Ok(await _mediator.Send(new LogGetPagedRequest(queryParams), HttpContext.RequestAborted));
        }
    }
}