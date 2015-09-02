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

        [Route("ajax/totals/{key}")]
        public JsonResult Totals(string key)
        {
            var db = new Entities();
            var data = db.Database.SqlQuery<PipelineResult>("PipelineData {0}", key).Single();
            return Json(new object[] { data }, JsonRequestBehavior.AllowGet);
        }

        [Route("ajax/pipeline/{key}")]
        public JsonResult Pipeline(string key)
        {
            var db = new Entities();
            var pipeline = db.Pipelines.Where(x => x.LoanOfficerKey == key)
                .Select(x => new
                {
                    x.LoanNumber,
                    x.Amount,
                    x.BorrowerFirstName,
                    x.BorrowerLastName,
                    Purpose = x.Purpose.Name,
                })
                .OrderBy(x => x.LoanNumber);
            return Json(pipeline, JsonRequestBehavior.AllowGet);
        }

    }
}