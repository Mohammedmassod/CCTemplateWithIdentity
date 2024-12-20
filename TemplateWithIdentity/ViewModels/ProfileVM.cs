using TemplateWithIdentity.Models;

namespace TemplateWithIdentity.ViewModels
{
    public class ProfileVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string? OldPass { get; set; }
        public string NewPass { get; set; }
        public string ConfirmPass { get; set; }

        public ProfileVM()
        {
            
        }
        //public ProfileVM(Users user)
        //{
        //    Id = user.Id;
        //    Name = user.Name;
        //    UserName = user.UserName;

        //}
    }
}
