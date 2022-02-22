using Cts.Project.Cts1.Models;
using Sitecore.Data;
using Sitecore.Data.Templates;
using Sitecore.Publishing;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cts.Project.Cts1.Controllers
{
    public class CommentsFormController : Controller
    {
        [HttpGet]
        public ActionResult CommentsFormAction()
        {
            Comment comment = new Comment();
            return View("/Views/Cts/LeadershipProfile/CommentsForm.cshtml", comment);
        }

        [HttpPost]
        public ActionResult CommentsFormAction(Comment comment)
        {
            //Create a new comment item as child item for the article item
            //template
            TemplateID templateID = new TemplateID(new ID("{FDB313F2-A6CC-46FA-A2EE-3B735765CDB6}"));
            //parent item
            var parentItem = Sitecore.Context.Item;
            var masterDB = Sitecore.Configuration.Factory.GetDatabase("master");
            var webDb = Sitecore.Configuration.Factory.GetDatabase("web");
            var parentItemFromMasterDB = masterDB.GetItem(parentItem.ID);
            
            using(new SecurityDisabler())
            {
                //create item
                var commentsItem = parentItemFromMasterDB.Add(comment.Name, templateID);
                //update the fields 
                commentsItem.Editing.BeginEdit();
                commentsItem["Comments"] = comment.Comments;
                commentsItem["Name"] = comment.Name;
                commentsItem["EmailId"] = comment.EmailId;
                commentsItem.Editing.EndEdit();

                PublishOptions publishOptions = new PublishOptions(masterDB, webDb, PublishMode.SingleItem, Sitecore.Context.Language, DateTime.Now);
                Publisher publisher = new Publisher(publishOptions);
                publisher.Options.RootItem = commentsItem;
                publisher.Options.Deep = true;
                publisher.Options.Mode = PublishMode.SingleItem;
                publisher.Publish();
            }
            

            return View("/Views/Cts/LeadershipProfile/ThankYou.cshtml");
        }

        public ActionResult GetComments()
        {
            List<Comment> comments = new List<Comment>();
            var contextItem = Sitecore.Context.Item;

            comments = contextItem.GetChildren()
                                        .Where(x => x.TemplateName == "Comment")
                                        .Select(x => new Comment
                                        {
                                            Comments = x.Fields["Comments"].Value,
                                            EmailId = x.Fields["EmailId"].Value,
                                            Name = x.DisplayName
                                        }).ToList();
            return View("/Views/Cts/LeadershipProfile/DisplayComments.cshtml", comments);
        }
    }
}