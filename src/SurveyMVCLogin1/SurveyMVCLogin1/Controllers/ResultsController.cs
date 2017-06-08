using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SurveyMVCLogin1.DAL;
using SurveyMVCLogin1.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Drawing;
using System.IO;
using SurveyMVCLogin1.Utility;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using Org.BouncyCastle.Utilities.Collections;
using Microsoft.AspNet.Identity;

namespace SurveyMVCLogin1.Controllers
{
    [Authorize]
    public class ResultsController : Controller
    {
        private SurveyContext db = new SurveyContext();


        // GET: Results
        public ActionResult Index()
        {
            return View(db.Surveys.ToList());
        }


        // GET: Results/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }


        // GET: Results/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Results/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SurveyID,StartTime,S1Q1Answer,S1Q2Answer,S1Q3Answer,S1Q3Score,S1Q4Answer,S1Q5Answer,S1Q6Answer,S1Q6Score,S1Q7Answer,S1Q8Answer,S1Q9Answer,S1Q10Answer,S1Q10Score,S1Q11Answer,S1Q11Score,S1Q12Answer,S1Q13Answer,S1Q13Score,S1Q14Answer,S1Q15Answer,S1Q16Answer,S2Q1Answer,S2Q1Score,S2Q2Answer,S2Q2Score,S2Q3Answer,S2Q3Score,S2Q4Answer,S2Q4Score,S2Q5Answer,S2Q5Score,S2Q6Answer,S2Q6Score,S2Q7Answer,S2Q7Score,S2Q8Answer,S2Q8Score,S2Q9Answer,S2Q9Score,S2Q10Answer,S2Q10Score,S3Q1Answer,S3Q1Score,S3Q2Answer,S3Q2Score,S3Q3Answer,S3Q3Score,S3Q4Answer,S3Q4Score,S3Q5Answer,S3Q5Score,S3Q6Answer,S3Q6Score,S3Q7Answer,S3Q7Score,S3Q8Answer,S3Q8Score,S3Q9Answer,S3Q9Score,S3Q10Answer,S3Q10Score,S4aQ1Answer,S4aQ2Answer,S4aQ3Answer,S4aQ4Answer,S4aQ5Answer,S4aQ6Answer,S4aQ7Answer,S4aQ8Answer,S4aQ9Answer,S4aQ10Answer,S4aQ11Answer,S4aQ12Answer,S4aQ13Answer,S4aQ14Answer,S4aQ15Answer,S4aQ16Answer,S4aQ17Answer,S4aQ18Answer,S4aQ19Answer,S4aQ20Answer,S4aQ21Answer,S4aQ22Answer,S4aQ23Answer,S4aQ24Answer,S4aQ25Answer,S4aQ26Answer,S4aQ27Answer,S4aQ28Answer,S4aQ29Answer,S4aQ30Answer,S4aQ31Answer,S4aQ32Answer,S4aQ33Answer,S4aQ34Answer,S4aQ35Answer,S4aQ36Answer,S4aQ37Answer,S4aQ38Answer,S4aQ39Answer,S4aQ40Answer,S4aQ41Answer,S4aQ42Answer,S4aQ43Answer,S4aQ44Answer,S4aQ45Answer,S4aQ46Answer,S4aQ47Answer,S4aQ48Answer,S4aQ49Answer,S4aQ50Answer,S4aQ51Answer,S4aQ52Answer,S4aQ53Answer,S4aQ54Answer,S4aQ55Answer,S4aQ56Answer,S4aQ57Answer,S4bQ1Answer,S4bQ2Answer,S4bQ3Answer,S4bQ4Answer,S4bQ5Answer,S4bQ6Answer,S4bQ7Answer,S4bQ8Answer,S4bQ9Answer,S4bQ10Answer,S4bQ11Answer,S4bQ12Answer,S4bQ13Answer,S4bQ14Answer,S4bQ15Answer,S4bQ16Answer,S4bQ17Answer,S4bQ18Answer,S4bQ19Answer,S4bQ20Answer,S4bQ21Answer,S4bQ22Answer,S4bQ23Answer,S4bQ24Answer,S4bQ25Answer,S4bQ26Answer,S4bQ27Answer,S4bQ28Answer,S4bQ29Answer,S4bQ30Answer,S4bQ31Answer,S4bQ32Answer,S4bQ33Answer,S4bQ34Answer,S4bQ35Answer,S4bQ36Answer,S4bQ37Answer,S4bQ38Answer,S4bQ39Answer,S4bQ40Answer,S4bQ41Answer,S4bQ42Answer,S4bQ43Answer,S4bQ44Answer,S4bQ45Answer,S4bQ46Answer,S4bQ47Answer,S4bQ48Answer,S4bQ49Answer,S4bQ50Answer,S4bQ51Answer,S4bQ52Answer,S4bQ53Answer,S4bQ54Answer,S4bQ55Answer,S4bQ56Answer,S4bQ57Answer,S4bQ58Answer,S4bQ59Answer,S4bQ60Answer,S4bQ61Answer")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Surveys.Add(survey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(survey);
        }

        // Cookie-Aware WebClient
        public class CookieAwareWebClient : WebClient
        {
            public CookieAwareWebClient()
            {
                CookieContainer = new CookieContainer();
            }
            public CookieContainer CookieContainer { get; private set; }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = (HttpWebRequest)base.GetWebRequest(address);
                request.CookieContainer = CookieContainer;
                return request;
            }
        }

        public ActionResult Pdf(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }

            //Uri baseUri = new Uri(Request.Url.OriginalString);

            string htmlCode = "";
            using (var client = new CookieAwareWebClient())
            {
                
                // Authenticate (username and password can be either
                //  hard-coded or pulled from a settings area)
                var values = new NameValueCollection{
                            { "UserName", "test@cd.com" },
                            { "Password", "2017@Admin"},
                    };

                client.UploadValues(new Uri("http://survey.mvc.login1/Account/Login"), "POST", values);

                htmlCode = client.DownloadString(Request.Url.OriginalString);
            }

            //MyWebClient WebClient = new MyWebClient();

            //string htmlCode = WebClient.DownloadString(new Uri(URL_status));

            byte[] pdfFile = this.ConvertHtmlTextToPDF(htmlCode);

            return File(pdfFile, "application/pdf", "result.pdf");
        }

        public byte[] ConvertHtmlTextToPDF(string htmlText)
        {
            if (string.IsNullOrEmpty(htmlText))
            {
                return null;
            }
            //避免當htmlText無任何html tag標籤的純文字時，轉PDF時會掛掉，所以一律加上<p>標籤
            htmlText = "<p>" + htmlText + "</p>";

            MemoryStream outputStream = new MemoryStream();//要把PDF寫到哪個串流
            byte[] data = Encoding.UTF8.GetBytes(htmlText);//字串轉成byte[]
            MemoryStream msInput = new MemoryStream(data);
            Document doc = new Document();//要寫PDF的文件，建構子沒填的話預設直式A4
            PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);
            //指定文件預設開檔時的縮放為100%
            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            //開啟Document文件 
            doc.Open();
            //使用XMLWorkerHelper把Html parse到PDF檔裡
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8, new UnicodeFontFactory());
            //將pdfDest設定的資料寫到PDF檔
            PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);
            doc.Close();
            msInput.Close();
            outputStream.Close();
            //回傳PDF檔案 
            return outputStream.ToArray();
        }

        // GET: Results/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }


        // POST: Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyID,StartTime,S1Q1Answer,S1Q2Answer,S1Q3Answer,S1Q3Score,S1Q4Answer,S1Q5Answer,S1Q6Answer,S1Q6Score,S1Q7Answer,S1Q8Answer,S1Q9Answer,S1Q10Answer,S1Q10Score,S1Q11Answer,S1Q11Score,S1Q12Answer,S1Q13Answer,S1Q13Score,S1Q14Answer,S1Q15Answer,S1Q16Answer,S2Q1Answer,S2Q1Score,S2Q2Answer,S2Q2Score,S2Q3Answer,S2Q3Score,S2Q4Answer,S2Q4Score,S2Q5Answer,S2Q5Score,S2Q6Answer,S2Q6Score,S2Q7Answer,S2Q7Score,S2Q8Answer,S2Q8Score,S2Q9Answer,S2Q9Score,S2Q10Answer,S2Q10Score,S3Q1Answer,S3Q1Score,S3Q2Answer,S3Q2Score,S3Q3Answer,S3Q3Score,S3Q4Answer,S3Q4Score,S3Q5Answer,S3Q5Score,S3Q6Answer,S3Q6Score,S3Q7Answer,S3Q7Score,S3Q8Answer,S3Q8Score,S3Q9Answer,S3Q9Score,S3Q10Answer,S3Q10Score,S4aQ1Answer,S4aQ2Answer,S4aQ3Answer,S4aQ4Answer,S4aQ5Answer,S4aQ6Answer,S4aQ7Answer,S4aQ8Answer,S4aQ9Answer,S4aQ10Answer,S4aQ11Answer,S4aQ12Answer,S4aQ13Answer,S4aQ14Answer,S4aQ15Answer,S4aQ16Answer,S4aQ17Answer,S4aQ18Answer,S4aQ19Answer,S4aQ20Answer,S4aQ21Answer,S4aQ22Answer,S4aQ23Answer,S4aQ24Answer,S4aQ25Answer,S4aQ26Answer,S4aQ27Answer,S4aQ28Answer,S4aQ29Answer,S4aQ30Answer,S4aQ31Answer,S4aQ32Answer,S4aQ33Answer,S4aQ34Answer,S4aQ35Answer,S4aQ36Answer,S4aQ37Answer,S4aQ38Answer,S4aQ39Answer,S4aQ40Answer,S4aQ41Answer,S4aQ42Answer,S4aQ43Answer,S4aQ44Answer,S4aQ45Answer,S4aQ46Answer,S4aQ47Answer,S4aQ48Answer,S4aQ49Answer,S4aQ50Answer,S4aQ51Answer,S4aQ52Answer,S4aQ53Answer,S4aQ54Answer,S4aQ55Answer,S4aQ56Answer,S4aQ57Answer,S4bQ1Answer,S4bQ2Answer,S4bQ3Answer,S4bQ4Answer,S4bQ5Answer,S4bQ6Answer,S4bQ7Answer,S4bQ8Answer,S4bQ9Answer,S4bQ10Answer,S4bQ11Answer,S4bQ12Answer,S4bQ13Answer,S4bQ14Answer,S4bQ15Answer,S4bQ16Answer,S4bQ17Answer,S4bQ18Answer,S4bQ19Answer,S4bQ20Answer,S4bQ21Answer,S4bQ22Answer,S4bQ23Answer,S4bQ24Answer,S4bQ25Answer,S4bQ26Answer,S4bQ27Answer,S4bQ28Answer,S4bQ29Answer,S4bQ30Answer,S4bQ31Answer,S4bQ32Answer,S4bQ33Answer,S4bQ34Answer,S4bQ35Answer,S4bQ36Answer,S4bQ37Answer,S4bQ38Answer,S4bQ39Answer,S4bQ40Answer,S4bQ41Answer,S4bQ42Answer,S4bQ43Answer,S4bQ44Answer,S4bQ45Answer,S4bQ46Answer,S4bQ47Answer,S4bQ48Answer,S4bQ49Answer,S4bQ50Answer,S4bQ51Answer,S4bQ52Answer,S4bQ53Answer,S4bQ54Answer,S4bQ55Answer,S4bQ56Answer,S4bQ57Answer,S4bQ58Answer,S4bQ59Answer,S4bQ60Answer,S4bQ61Answer")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(survey);
        }


        // GET: Results/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }


        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
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
