using Microsoft.AspNetCore.Identity;
using TemplateWithIdentity.ViewModels;

namespace TemplateWithIdentity.Models
{
    public class Users:IdentityUser
    {
        public string Name { get; set; }
        public bool IsClient { get; set; }
        public string? Pass { get; set; }

        public Users(UserVM users)
        {

            UserName = users.UserName;
            Name = users.Name;
            PhoneNumber = users.PhoneNumber;
            IsClient = users.isClient;

        }
        public Users()
        {

        }
    }
}
