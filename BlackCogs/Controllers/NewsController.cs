using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlackCogs.Data;
using BlackCogs.Data.Models;
using BlackCogs.Data.ViewModels;

namespace BlackCogs.Controllers
{
    [Export("News", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NewsController : Controller
    {
        private Context db = new Context();

        // GET: News
        public ActionResult Index()
        {


            try
            {

                List<ViewNews> vlist = new List<ViewNews>();
                List<News> news = db.News.ToList();
                foreach(var v in news)
                {
                    ViewNews vn = new ViewNews();
                    vn.ImportFromModel(v);
                    vlist.Add(vn);
                }


                return View(vlist);
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewNews vn = new ViewNews();
            vn.ImportFromModel(news);
            return View(vn);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Published,content,RowVersion")] ViewNews news)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(news.ExportToModel());
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewNews vn = new ViewNews();
            vn.ImportFromModel(news);

            return View(vn);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Published,content,RowVersion")] ViewNews news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news.ExportToModel()).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewNews vn = new ViewNews();
            vn.ImportFromModel(news);
            return View(vn);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
    }
}
