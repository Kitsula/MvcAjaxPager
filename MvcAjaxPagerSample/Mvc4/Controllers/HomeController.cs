using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

using MvcAjaxPagerSample.Models;

using MvcAjaxPager;


namespace MvcAjaxPagerSample.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

		public ActionResult Index(int page = 1)
        {
			var topicsDt = new DataTable("Topic");
			topicsDt.ReadXml(Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "Topics.xml"));
			var topics = topicsDt.AsEnumerable();
	        var topicsList = new List<Topic>();
	        topics.ToList().ForEach(t => topicsList.Add(new Topic {Title = t["title"].ToString(), Text = t["text"].ToString()})); 

	
			
			const int itemsPerPage = 4;
			var items = topicsList.AsQueryable().ToPagedList(page, itemsPerPage);

			if (!Request.IsAjaxRequest()) {
				return View(items);
			}

			return PartialView("Modules/TopicList", items);			
        }

    }
}
