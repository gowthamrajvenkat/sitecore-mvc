using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Project.Cts1.Models
{
    public class LeadershipProfile
    {
        public HtmlString LeaderName { get; set; }
        public HtmlString ProfileBrief { get; set; }

        public HtmlString LeaderImage { get; set; }

        public string DetailPageUrl { get; set; }
    }
}