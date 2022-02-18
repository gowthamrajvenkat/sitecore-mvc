using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Project.Cts1.Models
{
    public class Article
    {
        public HtmlString ArticleTitle { get; set; }
        public HtmlString ArticleBrief { get; set; }
        public string ArticleUrl { get; set; }
    }
}