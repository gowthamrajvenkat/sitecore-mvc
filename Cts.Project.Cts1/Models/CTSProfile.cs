using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Project.Cts1.Models
{
    public class CTSProfile
    {
        public HtmlString FirstName { get; set; }
        public HtmlString LastName { get; set; }
        public HtmlString EmailID { get; set; }
        public string ProfilePageUrl { get; set; }
        public HtmlString DateOfJoining { get; set; }
        public DateTime JoiningDate { get; set; }

    }
}