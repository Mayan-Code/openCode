using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Infrastructure.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static Domain.ActionContext GetActionContext(this ControllerBase controller)
        {
            Domain.ActionContext context = new Domain.ActionContext();

            context.ActionSource = controller.HttpContext.Connection.RemoteIpAddress.ToString() + " " +
                controller.HttpContext.Request.Headers[HeaderNames.UserAgent];

            if(controller.HttpContext.User != null && controller.HttpContext.User.Identity != null)
                context.Username = controller.HttpContext.User.Identity.Name;
            
            context.IpAddress = controller.GetIpAddress();
            
            return context;
        }

        public static string GetIpAddress(this ControllerBase controller)
        {
            if (controller.Request.Headers.ContainsKey("X-Forwarded-For"))
                return controller.Request.Headers["X-Forwarded-For"];
            else
                return controller.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
