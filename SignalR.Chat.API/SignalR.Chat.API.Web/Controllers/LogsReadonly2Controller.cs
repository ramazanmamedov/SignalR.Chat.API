﻿using System.Security.Claims;
using AutoMapper;
using SignalR.Chat.API.Data;
using SignalR.Chat.API.Entities;
using SignalR.Chat.API.Web.Infrastructure.Settings;
using SignalR.Chat.API.Web.ViewModels.LogViewModels;
using Calabonga.Microservices.Core;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SignalR.Chat.API.Web.Controllers
{
    /// <summary>
    /// ReadOnlyController Demo
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class LogsReadonly2Controller : ReadOnlyController<Log, LogViewModel, PagedListQueryParams>
    {
        private readonly CurrentAppSettings _appSettings;

        /// <inheritdoc />
        public LogsReadonly2Controller(
            IOptions<CurrentAppSettings> appSettings,
            IUnitOfWork<ApplicationDbContext> unitOfWork,
            IMapper mapper)
            : base(unitOfWork, mapper)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet("user-roles")]
        [Authorize(Policy = "Logs:UserRoles:View")]
        public IActionResult Get()
        {
            //Get Roles for current user
            var roles = ClaimsHelper.GetValues<string>((ClaimsIdentity) User.Identity, "role");
            return Ok($"Current user ({User.Identity.Name}) have following roles: {string.Join("|", roles)}");
        }

        /// <inheritdoc />
        protected override PermissionValidationResult ValidateQueryParams(PagedListQueryParams queryParams)
        {
            if (queryParams.PageSize <= 0)
            {
                queryParams.PageSize = _appSettings.PageSize;
            }

            return new PermissionValidationResult();
        }
    }
}