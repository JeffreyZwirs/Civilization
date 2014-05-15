using Civilization.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Civilization.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Overview()
        {
            var sessions = db.Questions;
            return View(sessions);
        }

        public ActionResult Sessions(int code)
        {
            var session = from s in db.Questions
                        where s.code == code
                        select s;

            return View(session.First());
        }

        [HttpPost]
        public ActionResult Sessions(Answers Answer)
        {
            if (ModelState.IsValid)
            {
                db.Answers.Add(Answer);
                db.SaveChanges();
                return RedirectToAction("Overview");
            }
            return View(Answer);
        }

        public ActionResult SessionCreate()
        {
            return View();
        }

        NumberGenerator numGen = new NumberGenerator();

        [HttpPost]
        public ActionResult SessionCreate(Questions Sessions)
        {
            int gen;            
            
            while (true)
            {
                gen = numGen.Gen(4);

                var result = (from s in db.Questions where s.code == gen select s).Any();

                if (!result)                
                    break;
            }
            Sessions.code = gen;

            db.Questions.Add(Sessions);
            db.SaveChanges();

            return RedirectToAction("Overview", new { code = Sessions.code });            
        }

        public ActionResult Edit(int id = 0)
        {
            Session["ID"] = id;
            Questions Sessions = db.Questions.Find(id);
            if (Sessions == null)
            {
                return HttpNotFound();
            }
            return View(Sessions);
        }

        [HttpPost]
        public ActionResult Edit(Questions Sessions)
        {
            Sessions.QuestionID = (int)Session["ID"];
            if (ModelState.IsValid)
            {
                db.Entry(Sessions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Overview");
            }
            return View(Sessions);
        }

        public ActionResult Delete(int id = 0)
        {
            Questions Sessions = db.Questions.Find(id);
            if (Sessions == null)
            {
                return HttpNotFound();
            }
            return View(Sessions);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Questions Sessions = db.Questions.Find(id);
            db.Questions.Remove(Sessions);
            db.SaveChanges();
            return RedirectToAction("Overview");
        }

        private DatabaseContext db = new DatabaseContext();
    }
}
