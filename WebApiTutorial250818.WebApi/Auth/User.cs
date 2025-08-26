using Microsoft.AspNetCore.Identity;

namespace WebApiTutorial250818.WebApi.Auth
{
    public class User : IdentityUser<Guid>
    {
        public string? Horse { get; set; }
    }
}
