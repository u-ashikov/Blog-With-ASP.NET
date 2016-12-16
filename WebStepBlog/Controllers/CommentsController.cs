using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebStepBlog.Models;

namespace WebStepBlog.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments/CreateComment/5

        public ActionResult CreateComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = GetPost(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        private Post GetPost(int? id)
        {
            return id.HasValue ? db.Posts.Include(p => p.Author).Include(t=>t.Tags).Where(x => x.Id == id).First() : new Post { Id = -1 };
        }

        // POST: Comments/Create/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult AddComment([Bind(Include = "Id,Name,Email,Body")] Comment comment, int postId)
        {
            if (ModelState.IsValid)
            {
                var post = db.Posts.Find(postId);
                Comment inputComment = new Comment();
                inputComment.Body = comment.Body;
                if (User.Identity.IsAuthenticated)
                {
                    string user = User.Identity.GetUserId();
                    ApplicationUser currentUser = db.Users.FirstOrDefault(u => u.Id == user);
                    inputComment.Name = currentUser.UserName;
                    inputComment.Email = currentUser.Email;
                    inputComment.User = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                }
                else
                {
                    inputComment.Name = comment.Name;
                    inputComment.Email = comment.Email;
                }
                post.Comments.Add(inputComment);
                db.SaveChanges();
                return RedirectToAction("CreateComment", new { id = postId });
            }
            return View("CreateComment", comment);
        }

        // GET: Comments/EditComment/5
        [Authorize(Roles = "Administrators")]
        public ActionResult EditComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/EditComment/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment([Bind(Include = "Id,Name,Email,Body")] Comment comment, int id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(comment);
        }

        // GET: Comments/DeleteComment/5
        [Authorize(Roles = "Administrators")]
        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comment/DeleteComment/5
        [Authorize(Roles = "Administrators")]
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}