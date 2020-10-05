using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace EE.Ord.Infrastructure.Web
{
    public class WebUser : IInfrastructureUser
    {
        private IHttpContextAccessor _contextAccessor;

        public WebUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;

            var identity = (ClaimsIdentity)contextAccessor.HttpContext?.User?.Identity;

            var claim = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (claim != null && Guid.TryParse(claim.Value, out Guid userId))
            {
                Name = "ApiUser";
                ID = userId;
            }
            else
            {
                Name = "DefaultUser";
                ID = Guid.Parse("9DB5F12C-8428-405B-928C-7644F624C081");
            }
        }

        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
        public string Name { get; }
        public Guid ID { get; }
        public int? UtcOffset { get; set; }
    }
}
