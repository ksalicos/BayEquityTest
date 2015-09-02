using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using BayEquityTest.Models;
using System.Web.Hosting;

namespace BayEquityTest.Code
{
    public static class PopulateTables
    {
        //Recreates tables, should I need to clear them.
        public static void Go()
        {
            var db = new Entities();
            if (!db.LoanOfficers.Any())
                LoadOfficers();
            if (!db.Pipelines.Any())
                LoadPipelines();
        }

        public static void LoadOfficers()
        {
            var db = new Entities();
            var path = HostingEnvironment.MapPath(@"~/loan-officers.csv");
            using (var officers = new StreamReader(path))
            {
                officers.ReadLine(); //skip header
                while (officers.Peek() >= 0)
                {
                    var line = officers.ReadLine().Split(',');
                    db.LoanOfficers.Add(new LoanOfficer
                    {
                        Key = line[0],
                        FirstName = line[1],
                        LastName = line[2],
                        Email = line[3],
                        Title = line[4],
                        City = line[5],
                        State = line[6]
                    });
                }
            }

            db.SaveChanges();
        }

        public static void LoadPipelines()
        {
            var db = new Entities();
            var path = HostingEnvironment.MapPath(@"~/pipeline.csv");
            using (var pipeline = new StreamReader(path))
            {
                pipeline.ReadLine(); //skip header
                while (pipeline.Peek() >= 0)
                {
                    var line = pipeline.ReadLine().Split(',');
                    var purpose = line[5];
                    db.Pipelines.Add(new Pipeline
                    {
                        LoanOfficerKey = line[0],
                        LoanNumber = line[1],
                        BorrowerFirstName = line[2],
                        BorrowerLastName = line[3],
                        Amount = decimal.Parse(line[4]),
                        Purpose = db.Purposes.First(x=>x.Name == purpose),
                        Originated = DateTime.Parse(line[6]),
                        SubmittedToUW = DateTime.Parse(line[7]),
                        Funded = DateTime.Parse(line[8])
                    });
                }
            }

            db.SaveChanges();
        }
    }
}