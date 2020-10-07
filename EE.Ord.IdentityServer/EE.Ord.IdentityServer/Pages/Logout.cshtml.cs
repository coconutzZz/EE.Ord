using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EE.Ord.IdentityServer.Models;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EE.Ord.IdentityServer.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEventService _events;

        public LogoutModel(IIdentityServerInteractionService interaction, SignInManager<ApplicationUser> signInManager, IEventService events)
        {
            _interaction = interaction;
            _signInManager = signInManager;
            _events = events;
        }

        public bool ShowLogoutPrompt { get; set; }
        public string LogoutId { get; set; }


        public async Task<PageResult> OnGet(string logoutId)
        {
            LogoutId = logoutId;

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                ShowLogoutPrompt = false;
                return Page();
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                ShowLogoutPrompt = false;
                return Page();
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return Page();
        }

        public async Task<RedirectToPageResult> OnPost()
        {
            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await _signInManager.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            return RedirectToPage("LoggedOut", new { logoutId = LogoutId } );
        }

    }
}
