using Microsoft.AspNetCore.Identity;

namespace Web_Shop.Models
{
    public class AppUseModel : IdentityUser
    {
        public string Occupation {  get; set; }
    }
}
