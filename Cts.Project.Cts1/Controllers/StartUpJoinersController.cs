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
    public class StartUpJoinersController : Controller
    {
        // GET: StartUpJoiners
        public ActionResult GetListOfStartUpJoiners()
        {
            var contextItem = Sitecore.Context.Item;
            
            var startUpJoinersList = contextItem.GetChildren()
                                        .Where(x => x.TemplateName == "LeadershipProfile")
                                        .Where(x => CheckJoinerForStartUp(x))
                                        .Select(x => new LeadershipCard
                                        {
                                            LeaderName = x.Fields["LeaderName"].Value,
                                            LeaderProfile = x.Fields["ProfileBrief"].Value,
                                            LeaderProfileUrl = LinkManager.GetItemUrl(x),
                                        }).ToList();
            return View("/Views/Cts/Listing/StartUpJoiners.cshtml", startUpJoinersList);
        }

        private bool CheckJoinerForStartUp(Item joinerItem)
        {
            LinkField profileField = joinerItem.Fields["ProfileDetail"];
            var startUpJoinerSettingItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID("{825F1216-AF43-4589-A158-0BD91AA0EC16}"));
            DateField startDate = startUpJoinerSettingItem.Fields["StartDate"];
            DateField endDate = startUpJoinerSettingItem.Fields["EndDate"];
           
            if (profileField.IsInternal)
            {
                var profileItem = profileField.TargetItem;
                if (profileItem.TemplateName == "CTSProfile")
                {
                    DateField profileJoiningDate = profileItem.Fields["DateOfJoining"];
                    if ((profileJoiningDate.DateTime > startDate.DateTime)
                        && (profileJoiningDate.DateTime < endDate.DateTime))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;

        }
    }
}