using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApp_BackOffice.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the BackOfficeUser class
    public class BackOfficeUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Nome { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Apelido { get; set; }

    }
}
