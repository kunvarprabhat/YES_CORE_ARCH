using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace YES.Domain.Auth
{

#nullable disable
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public bool RememberMe { get; set; }
        //public List<RefreshToken> RefreshTokens { get; set; }
        //public bool OwnsToken(string token)
        //{
        //    return this.RefreshTokens?.Find(x => x.Token == token) != null;
        //}


    }
}