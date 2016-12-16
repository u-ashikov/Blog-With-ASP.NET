using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebStepBlog.Models;
using PagedList;
using PagedList.Mvc;
using WebStepBlog.Extensions;

namespace WebStepBlog.Controllers
{
    [ValidateInput(false)]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index(int? page)
        {
            var posts = db.Posts.Include(p => p.Author).Include(p => p.Tags).ToList().ToPagedList(page ?? 1, 5);
            return View(posts);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,Date,Tag")] Post post)
        {
            if (ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(post.Tag))
                {
                    post.Tags = null;
                }
                else
                {
                    var tags = post.Tag.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    List<Tag> postTags = new List<Tag>();
                    foreach (var tag in tags)
                    {
                        Tag newTag = new Tag();
                        newTag.Title = tag;
                        List<Tag> search = new List<Tag>();
                        search.AddRange(db.Tags.Where(t => t.Title == newTag.Title));
                        if (search.Count() > 0)
                        {
                            var existingTag = db.Tags.Where(t => t.Title == newTag.Title);
                            postTags.AddRange(existingTag);

                        }
                        else
                        {
                            db.Tags.Add(newTag);
                            postTags.Add(newTag);
                        }
                    }
                    post.Tags = postTags;
                }
                post.Author = db.Users.FirstOrDefault(u=>u.UserName==User.Identity.Name);
                post.Date = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                this.AddNotification("Post created!",NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles ="Administrators")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Tag,Tags")] Post post)
        {
            if (ModelState.IsValid)
            {
                var currentPost = db.Posts.Include(p => p.Tags).Single(x => x.Id == post.Id);
                currentPost.Tags.Clear();
                currentPost.Tag = post.Tag;
                if (!String.IsNullOrEmpty(post.Tag))
                {
                    string[] tags = post.Tag.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    List<Tag> postTags = new List<Tag>();
                    foreach (var tag in tags)
                    {
                        Tag newTag = new Tag();
                        newTag.Title = tag;
                        List<Tag> search = new List<Tag>();
                        search.AddRange(db.Tags.Where(t => t.Title == newTag.Title));
                        if (search.Count() > 0)
                        {
                            var existingTag = db.Tags.Where(t => t.Title == newTag.Title);
                            postTags.AddRange(existingTag);

                        }
                        else
                        {
                            db.Tags.Add(newTag);
                            postTags.Add(newTag);
                        }
                        currentPost.Tags = postTags;
                    }
                }
                currentPost.Title = post.Title;
                currentPost.Body = post.Body;
                db.Entry(currentPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize(Roles = "Administrators")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Comments.RemoveRange(post.Comments);
            post.Tags.Clear();  
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Posts/SinglePost/5
        public ActionResult SinglePost(int? id)
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


    }
}
