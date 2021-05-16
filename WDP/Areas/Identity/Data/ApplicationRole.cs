using Microsoft.AspNetCore.Identity;
using System;

namespace WDP.Areas.Identity.Data
{

    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
