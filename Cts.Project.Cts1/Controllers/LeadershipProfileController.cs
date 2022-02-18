using Cts.Project.Cts1.Models;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cts.Project.Cts1.Controllers
{
    public class LeadershipProfileController : Controller
    {
        // GET: LeadershipProfile
        public ActionResult GetLeadershipProfile()
        {
            var contextItem = Sitecore.Context.Item;
            LeadershipProfile leadershipProfile = new LeadershipProfile();
            leadershipProfile.LeaderName = new HtmlString(FieldRenderer.Render(contextItem, "LeaderName"));
            leadershipProfile.ProfileBrief = new HtmlString(FieldRenderer.Render(contextItem, "ProfileBrief"));
            //leadershipProfile.Brief = contextItem.Fields["ProfileBrief"].Value;
            leadershipProfile.LeaderImage = new HtmlString(FieldRenderer.Render(contextItem, "Image"));
            //leadershipProfile.DetailPageUrl = contextItem.Fields["ProfileDetail"].Value; //this gives the raw value
            LinkField linkField = contextItem.Fields["ProfileDetail"];
            var targetItem = linkField.TargetItem;
            leadershipProfile.DetailPageUrl = LinkManager.GetItemUrl(targetItem);
            return View("/Views/Cts/LeadershipProfile/LeadershipProfile.cshtml", leadershipProfile);
        }

        public ActionResult GetCTSProfile()
        {
            var contextItem = Sitecore.Context.Item;
            CTSProfile ctsProfile = new CTSProfile();
            ctsProfile.FirstName = new HtmlString(FieldRenderer.Render(contextItem, "FirstName"));
            ctsProfile.LastName = new HtmlString(FieldRenderer.Render(contextItem, "LastName"));
            ctsProfile.EmailID = new HtmlString(FieldRenderer.Render(contextItem, "EmailID"));
            LinkField linkField = contextItem.Fields["LeadershipProfile"];
            var targetItem = linkField.TargetItem;
            ctsProfile.ProfilePageUrl = LinkManager.GetItemUrl(targetItem);

            DateField dateField = contextItem.Fields["DateOfJoining"];
            ctsProfile.DateOfJoining = new HtmlString(FieldRenderer.Render(contextItem, "DateOfJoining"));
            ctsProfile.JoiningDate = dateField.DateTime;
            return View("/Views/Cts/LeadershipProfile/CTSProfile.cshtml", ctsProfile);
        }

        public ActionResult GetLeaderArticles()
        {
            List<Article> articleList = new List<Article>();
            var contextItem = Sitecore.Context.Item;
            MultilistField multilistField = contextItem.Fields["Articles"];
            articleList = multilistField.GetItems()
                            .Select(x => new Article
                            {
                                ArticleTitle = new HtmlString(FieldRenderer.Render(x, "Title")),
                                ArticleBrief = new HtmlString(FieldRenderer.Render(x, "Brief")),
                                ArticleUrl = LinkManager.GetItemUrl(x)
                            }).ToList();
            return View("/Views/Cts/LeadershipProfile/Articles.cshtml", articleList);
        }
    }
}