using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TemplateWithIdentity.Models;

namespace TemplateWithIdentity.ViewModels
{
    public class UserVM
    {
        
        public string? Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool isClient { get; set; }
        
        public string? PassWord { get; set; }
        
        public string? ConfirmPassWord { get; set; }

        public UserVM()
        {
                
        }

        public UserVM(Users users)
        {
            Id= users.Id;
            UserName = users.UserName;
            Name=users.Name;
            PhoneNumber = users.PhoneNumber;
            isClient = users.IsClient;
            
        }
    }
}
