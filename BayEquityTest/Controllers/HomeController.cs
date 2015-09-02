using BayEquityTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayEquityTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("ajax/officers")]
        public JsonResult Officers()
        {
            var db = new Entities();
            return Json(db.LoanOfficers.ToList(), JsonRequestBehavior.AllowGet);
        }

        [Route("ajax/pipeline/{key}")]
        public JsonResult Pipeline(string key)
        {
            var db = new Entities();
            if (!db.LoanOfficers.Any(x => x.Key == key))
                throw new Exception("No officer with key: " + key);
            var pipeline = db.Pipelines.Where(x => x.LoanOfficerKey == key)                
                .Select(x => new
                {
                    x.LoanNumber,
                    x.Amount,
                    x.BorrowerFirstName,
                    x.BorrowerLastName,
                    Purpose = x.Purpose.Name,                    
                })
                .OrderBy(x=>x.LoanNumber);
            var foo = new List<string>();
            db.Database.Log = foo.Add;
            var data = db.Database.SqlQuery<PipelineResult>("PipelineData {0}", key).Single();
            return Json(new { Summary = data, Pipeline = pipeline }, JsonRequestBehavior.AllowGet);
        }
    }
}