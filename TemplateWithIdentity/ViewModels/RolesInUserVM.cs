namespace TemplateWithIdentity.ViewModels
{
    public class RolesInUserVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        
       

        public List<UpdateUserRolesVM> RolesList { get; set; }
    }
}
