using Cts.Project.Cts1.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cts.Project.Cts1.Controllers
{
    public class FilePathController : Controller
    {
        // GET: FilePath
        public ActionResult GetBreadCrumb()
        {
            var contextItem = Sitecore.Context.Item;
            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            var currentItemNav = new NavigationItem
            {
                NavTitle = contextItem.DisplayName,
                NavUrl = LinkManager.GetItemUrl(contextItem)
            };
            var navItemList = contextItem.Axes.GetAncestors()
                                //.Where(x => x.Fields["IsNavigable"] != null && x.Fields["IsNavigable"].Value == "1")
                                .Where(x => x.Axes.IsDescendantOf(homeItem))
                                .Where(x => CheckNavigableOption(x))
                                .Select(x => new NavigationItem
                                {
                                    NavTitle = x.DisplayName,
                                    NavUrl = LinkManager.GetItemUrl(x)
                                })
                                .ToList()
                                .Concat(new List<NavigationItem> { currentItemNav });
            return View("/Views/Cts/Common/Breadcrumb.cshtml", navItemList);
        }

        private bool CheckNavigableOption(Item item)
        {
            CheckboxField checkboxField = item.Fields["IsNavigable"];
            return checkboxField.Checked;
        }
    }
}