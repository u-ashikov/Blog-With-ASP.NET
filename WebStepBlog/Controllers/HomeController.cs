using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStepBlog.Models;
using PagedList;
using PagedList.Mvc;

namespace WebStepBlog.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page)
        {
            List<object> sumModel = new List<object>();
            sumModel.Add(db.Posts.Include(p => p.Author).Include(p=>p.Tags).OrderByDescending(p => p.Date).ToList().ToPagedList(page ?? 1, 5));
            sumModel.Add(db.Posts.OrderByDescending(d => d.Date).ToList());
            sumModel.Add(db.Tags.OrderByDescending(d => d.Posts.Count).ToList());
            return View(sumModel);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}