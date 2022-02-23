using Cts.Project.Cts1.Models;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cts.Project.Cts1.Controllers
{
    public class FeaturedArticlesController : Controller
    {
        // GET: FeaturedArticles
        public ActionResult GetFeaturedArticle()
        {
            var datasourceItem = RenderingContext.Current.Rendering.Item;

            Article article = new Article
            {
                ArticleTitle = new HtmlString(FieldRenderer.Render(datasourceItem, "Title")),
                ArticleBrief = new HtmlString(FieldRenderer.Render(datasourceItem, "Brief")),
                ArticleUrl = LinkManager.GetItemUrl(datasourceItem)
            };
            return View("/Views/Cts/Common/FeaturedArticle.cshtml", article);
        }
    }
}