using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EE.Ord.IdentityServer.Pages
{
    public class LoggedOutModel : PageModel
    {
        private readonly IIdentityServerInteractionService _interaction;

        public LoggedOutModel(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        public string PostLogoutRedirectUri { get; set; }
        public string ClientName { get; set; }
        public string LogoutId { get; set; } 

        public async Task OnGet(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            PostLogoutRedirectUri = logout?.PostLogoutRedirectUri;
            ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName;
            LogoutId = logoutId;
        }
    }
}
