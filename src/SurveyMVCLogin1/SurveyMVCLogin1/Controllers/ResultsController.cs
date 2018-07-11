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

            /*int S1ScoreTest = 0;
            string S1ScoreTestMsg = "";
            int S2ScoreTest = 0;
            string S2ScoreTestMsg = "";
            int S3ScoreTest = 0;
            string S3ScoreTestMsg = "";*/

            int S1Score = 0;
            int S2Score = 0;
            int S3Score = 0;
            string S3ScoreMsg = "";


            if (survey != null)
            {
                /*S1ScoreTest = survey.S1Q3Score + survey.S1Q6Score + survey.S1Q10Score + survey.S1Q11Score + survey.S1Q13Score;
                if (S1ScoreTest > 60)
                {
                    S1ScoreTestMsg = "第一部得分大于60分：您对英国企业家移民签证政策充分了解，可以进入下一步评测。Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempus pulvinar dictum. Praesent commodo augue vitae massa vestibulum pulvinar id nec mauris. Nam id tellus tincidunt, fringilla massa eu, finibus lectus. Quisque quis elementum ante. Etiam varius aliquam lacus. Pellentesque vitae luctus eros. Nunc maximus sapien sit amet ipsum scelerisque lobortis. Aenean ac vestibulum lacus. Integer bibendum nunc in aliquet commodo. Nunc mollis laoreet felis in faucibus. Integer faucibus ornare libero sed suscipit. Nullam in euismod nunc, eu tempus velit.";
                }
                else if (S1ScoreTest >= 30 && S1ScoreTest < 60)
                {
                    S1ScoreTestMsg = "第一部分得分在30-60分之间：您对英国企业家移民签证政策基本了解，可以进入下一步评测。Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempus pulvinar dictum. Praesent commodo augue vitae massa vestibulum pulvinar id nec mauris. Nam id tellus tincidunt, fringilla massa eu, finibus lectus. Quisque quis elementum ante. Etiam varius aliquam lacus. Pellentesque vitae luctus eros. Nunc maximus sapien sit amet ipsum scelerisque lobortis. Aenean ac vestibulum lacus. Integer bibendum nunc in aliquet commodo. Nunc mollis laoreet felis in faucibus. Integer faucibus ornare libero sed suscipit. Nullam in euismod nunc, eu tempus velit.";
                }
                else if (S1ScoreTest < 30)
                {
                    S1ScoreTestMsg = "第一部分得分在30分以下：您对英国企业家移民签证政策不够了解，建议您充分了解政策后再进入下一步评测。Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempus pulvinar dictum. Praesent commodo augue vitae massa vestibulum pulvinar id nec mauris. Nam id tellus tincidunt, fringilla massa eu, finibus lectus. Quisque quis elementum ante. Etiam varius aliquam lacus. Pellentesque vitae luctus eros. Nunc maximus sapien sit amet ipsum scelerisque lobortis. Aenean ac vestibulum lacus. Integer bibendum nunc in aliquet commodo. Nunc mollis laoreet felis in faucibus. Integer faucibus ornare libero sed suscipit. Nullam in euismod nunc, eu tempus velit.";
                }

                ViewBag.Section1ScoreTest = S1ScoreTest;
                ViewBag.Section1Message = S1ScoreTestMsg;

                S2ScoreTest = survey.S2Q1Score + survey.S2Q2Score + survey.S2Q3Score + survey.S2Q4Score + survey.S2Q5Score + survey.S2Q6Score + survey.S2Q7Score + survey.S2Q8Score + survey.S2Q9Score + survey.S2Q10Score;
                if (S2ScoreTest == 100)
                {
                    S2ScoreTestMsg = "第二部得分等于100分：您对英国企业家移民签证政策充分了解，可以进入下一步评测。Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempus pulvinar dictum. Praesent commodo augue vitae massa vestibulum pulvinar id nec mauris. Nam id tellus tincidunt, fringilla massa eu, finibus lectus. Quisque quis elementum ante. Etiam varius aliquam lacus. Pellentesque vitae luctus eros. Nunc maximus sapien sit amet ipsum scelerisque lobortis. Aenean ac vestibulum lacus. Integer bibendum nunc in aliquet commodo. Nunc mollis laoreet felis in faucibus. Integer faucibus ornare libero sed suscipit. Nullam in euismod nunc, eu tempus velit.";
                }
                else if (S2ScoreTest >= 60 && S2ScoreTest < 100)
                {
                    S2ScoreTestMsg = "第二部分得分在60-90分之间：您对英国企业家移民签证政策基本了解，可以进入下一步评测。Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempus pulvinar dictum. Praesent commodo augue vitae massa vestibulum pulvinar id nec mauris. Nam id tellus tincidunt, fringilla massa eu, finibus lectus. Quisque quis elementum ante. Etiam varius aliquam lacus. Pellentesque vitae luctus eros. Nunc maximus sapien sit amet ipsum scelerisque lobortis. Aenean ac vestibulum lacus. Integer bibendum nunc in aliquet commodo. Nunc mollis laoreet felis in faucibus. Integer faucibus ornare libero sed suscipit. Nullam in euismod nunc, eu tempus velit.";
                }
                else if (S2ScoreTest < 60)
                {
                    S2ScoreTestMsg = "第二部分得分在60分以下：您对英国企业家移民签证政策不够了解，建议您充分了解政策后再进入下一步评测。Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempus pulvinar dictum. Praesent commodo augue vitae massa vestibulum pulvinar id nec mauris. Nam id tellus tincidunt, fringilla massa eu, finibus lectus. Quisque quis elementum ante. Etiam varius aliquam lacus. Pellentesque vitae luctus eros. Nunc maximus sapien sit amet ipsum scelerisque lobortis. Aenean ac vestibulum lacus. Integer bibendum nunc in aliquet commodo. Nunc mollis laoreet felis in faucibus. Integer faucibus ornare libero sed suscipit. Nullam in euismod nunc, eu tempus velit.";
                }

                ViewBag.Section2ScoreTest = S2ScoreTest;
                ViewBag.Section2Message = S2ScoreTestMsg;

                S3ScoreTest = survey.S3Q1Score + survey.S3Q2Score + survey.S3Q3Score + survey.S3Q4Score + survey.S3Q5Score + survey.S3Q6Score + survey.S3Q7Score + survey.S3Q8Score + survey.S3Q9Score + survey.S3Q10Score;
                if (S3ScoreTest == 100)
                {
                    S3ScoreTestMsg = "第三部得分等于100分：您对英国企业家移民签证政策充分了解，可以进入下一步评测。Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempus pulvinar dictum. Praesent commodo augue vitae massa vestibulum pulvinar id nec mauris. Nam id tellus tincidunt, fringilla massa eu, finibus lectus. Quisque quis elementum ante. Etiam varius aliquam lacus. Pellentesque vitae luctus eros. Nunc maximus sapien sit amet ipsum scelerisque lobortis. Aenean ac vestibulum lacus. Integer bibendum nunc in aliquet commodo. Nunc mollis laoreet felis in faucibus. Integer faucibus ornare libero sed suscipit. Nullam in euismod nunc, eu tempus velit.";
                }
                else if (S3ScoreTest >= 60 && S3ScoreTest < 100)
                {
                    S3ScoreTestMsg = "第三部分得分在60-90分之间：您对英国企业家移民签证政策基本了解，可以进入下一步评测。Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempus pulvinar dictum. Praesent commodo augue vitae massa vestibulum pulvinar id nec mauris. Nam id tellus tincidunt, fringilla massa eu, finibus lectus. Quisque quis elementum ante. Etiam varius aliquam lacus. Pellentesque vitae luctus eros. Nunc maximus sapien sit amet ipsum scelerisque lobortis. Aenean ac vestibulum lacus. Integer bibendum nunc in aliquet commodo. Nunc mollis laoreet felis in faucibus. Integer faucibus ornare libero sed suscipit. Nullam in euismod nunc, eu tempus velit.";
                }
                else if (S3ScoreTest < 60)
                {
                    S3ScoreTestMsg = "第三部分得分在60分以下：您对英国企业家移民签证政策不够了解，建议您充分了解政策后再进入下一步评测。Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempus pulvinar dictum. Praesent commodo augue vitae massa vestibulum pulvinar id nec mauris. Nam id tellus tincidunt, fringilla massa eu, finibus lectus. Quisque quis elementum ante. Etiam varius aliquam lacus. Pellentesque vitae luctus eros. Nunc maximus sapien sit amet ipsum scelerisque lobortis. Aenean ac vestibulum lacus. Integer bibendum nunc in aliquet commodo. Nunc mollis laoreet felis in faucibus. Integer faucibus ornare libero sed suscipit. Nullam in euismod nunc, eu tempus velit.";
                }

                ViewBag.Section3ScoreTest = S3ScoreTest;
                ViewBag.Section3Message = S3ScoreTestMsg;*/

                S1Score = survey.S1Q3Score + survey.S1Q6Score + survey.S1Q9Score + survey.S1Q13Score + survey.S1Q14Score + survey.S1Q16Score;
                S2Score = survey.S2Q1Score + survey.S2Q2Score + survey.S2Q3Score + survey.S2Q4Score + survey.S2Q5Score + survey.S2Q6Score + survey.S2Q7Score + survey.S2Q8Score + survey.S2Q9Score + survey.S2Q10Score;
                S3Score = survey.S3Q1Score + survey.S3Q2Score + survey.S3Q3Score + survey.S3Q4Score + survey.S3Q5Score + survey.S3Q6Score + survey.S3Q7Score + survey.S3Q8Score + survey.S3Q9Score + survey.S3Q10Score;
                // case - 01 : 英国商业知识得分 100, 签证知识得分 100, 基本信息得分 60及以上
                if (S3Score == 100 && S2Score == 100 && S1Score >= 60)
                {
                    S3ScoreMsg = "您的个人背景，企业家签证知识和商业知识都非常完美，您非常适合申请英国的企业家签证。";
                }
                // case - 02 : 英国商业知识得分 100, 签证知识得分 60-90, 基本信息得分 60及以上
                else if (S3Score == 100 && S2Score >= 60 && S2Score <= 90 && S1Score >= 60)
                {
                    S3ScoreMsg = "您的个人背景和商业知识都非常好，对英国的企业家签证也有一定的了解，请阅读《商业创业指南》，深入了解企业家签证后可申请英国的企业家签证。";
                }
                // case - 03 : 英国商业知识得分 100, 签证知识得分 100, 基本信息得分 30-50
                else if (S3Score == 100 && S2Score == 100 && S1Score >= 30 && S1Score <= 50)
                {
                    S3ScoreMsg = "您对商业知识和企业家签证非常了解，在个人背景方面要有针对性的加强，可以咨询专业律师，并做好充分的准备工作后可尝试申请英国的企业家签证。";
                }
                // case - 04 : 英国商业知识得分 100, 签证知识得分 60-90, 基本信息得分 30-50
                else if (S3Score == 100 && S2Score >= 60 && S2Score <= 90 && S1Score >= 30 && S1Score <= 50)
                {
                    S3ScoreMsg = "您对商业知识非常了解，个人背景有一些基础，对企业家签证也有一定的了解，可阅读《商业创业指南》加深了解英国企业家签证知识后，可尝试申请企业家签证。";
                }
                // case - 05 : 英国商业知识得分 60-90, 签证知识得分 100, 基本信息得分 60及以上
                else if (S3Score >= 60 && S3Score <= 90 && S2Score == 100 && S1Score >= 60)
                {
                    S3ScoreMsg = "您的个人背景和签证知识都非常好，大体的商业知识也已了解，可通过阅读《商业创业指南》加深对英国商业的理解，在做好充分的准备工作后可申请英国的企业家签证。";
                }
                // case - 06 : 英国商业知识得分 60-90, 签证知识得分 60-90, 基本信息得分 60及以上
                else if (S3Score >= 60 && S3Score <= 90 && S2Score >= 60 && S2Score <= 90 && S1Score >= 60)
                {
                    S3ScoreMsg = "您的个人背景很好，对商业知识和企业家签证也有一定的了解，可通过阅读《商业创业指南》加深对英国商业和企业家签证的理解，在做好充分的准备工作后可尝试申请英国的企业家签证。";
                }
                // case - 07 : 英国商业知识得分 60-90, 签证知识得分 100, 基本信息得分 30-50
                else if (S3Score >= 60 && S3Score <= 90 && S2Score == 100 && S1Score >= 30 && S1Score <= 50)
                {
                    S3ScoreMsg = "您对企业家签证非常了解，对英国商业知识也有一定的了解，个人背景也有一定的基础，请阅读《商业创业指南》在深入了解、提高后可尝试申请企业家签证。";
                }
                // case - 08 : 英国商业知识得分 60-90, 签证知识得分 60-90, 基本信息得分 30-50
                else if (S3Score >= 60 && S3Score <= 90 && S2Score >= 60 && S2Score <= 90 && S1Score >= 30 && S1Score <= 50)
                {
                    S3ScoreMsg = "您对英国商业知识、企业家签证都有一定的了解，个人背景也有一定的基础，请阅读《商业创业指南》在深入了解、提高后可尝试申请企业家签证。";
                }
                // case - 09 : 英国商业知识得分 0-50, 签证知识得分 100, 基本信息得分 60及以上
                else if (S3Score <= 50 && S2Score == 100 && S1Score >= 60)
                {
                    S3ScoreMsg = "您的个人背景和签证知识都非常好，但缺乏英国的商业知识，请阅读《商业创业指南》，深入了解英国商业知识后可尝试申请英国的企业家签证。";
                }
                // case - 10 : 英国商业知识得分 0-50, 签证知识得分 60-90, 基本信息得分 60及以上
                else if (S3Score <= 50 && S2Score >= 60 && S2Score <= 90 && S1Score >= 60)
                {
                    S3ScoreMsg = "您的个人背景很好，对企业家签证也有一定的了解，需加强商业知识，可通过阅读《商业创业指南》加深对英国商业和企业家签证的理解，在做好充分的准备工作后可尝试申请英国的企业家签证。";
                }
                // case - 11 : 英国商业知识得分 0-50, 签证知识得分 100, 基本信息得分 30-50
                else if (S3Score <= 50 && S2Score == 100 && S1Score >= 30 && S1Score <= 50)
                {
                    S3ScoreMsg = "您对企业家签证非常了解，个人背景也有一定的基础，但缺乏英国商业知识，可在阅读《商业创业指南》加深了解后，可尝试申请企业家签证。";
                }
                // case - 12 : 英国商业知识得分 0-50, 签证知识得分 60-90, 基本信息得分 30-50
                else if (S3Score <= 50 && S2Score >= 60 && S2Score <= 90 && S1Score >= 30 && S1Score <= 50)
                {
                    S3ScoreMsg = "您对企业家签证知识有一定的了解，个人背景也有一定的基础，但英国商业知识有待加强，可通过阅读《商业创业指南》加深了解后，再来参加我们的企业家评测。";
                }

                ViewBag.Section1Score = S1Score;
                ViewBag.Section2Score = S2Score;
                ViewBag.Section3Score = S3Score;
                ViewBag.Section3Message = S3ScoreMsg;
            }
            else
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
        public ActionResult Create([Bind(Include = "SurveyID,StartTime,S1Q1Answer,S1Q2Answer,S1Q3Answer,S1Q3Score,S1Q4Answer,S1Q5Answer,S1Q6Answer,S1Q6Score,S1Q7Answer,S1Q8Answer,S1Q9Answer,S1Q9Score,S1Q10Answer,S1Q10Score,S1Q11Answer,S1Q12Answer,S1Q13Answer,S1Q13Score,S1Q14Answer,S1Q14Score,S1Q15Answer,S1Q16Answer,S1Q16Score,S1Q17Answer,S1Q18Answer,S1Q19Answer,S2Q1Answer,S2Q1Score,S2Q2Answer,S2Q2Score,S2Q3Answer,S2Q3Score,S2Q4Answer,S2Q4Score,S2Q5Answer,S2Q5Score,S2Q6Answer,S2Q6Score,S2Q7Answer,S2Q7Score,S2Q8Answer,S2Q8Score,S2Q9Answer,S2Q9Score,S2Q10Answer,S2Q10Score,S3Q1Answer,S3Q1Score,S3Q2Answer,S3Q2Score,S3Q3Answer,S3Q3Score,S3Q4Answer,S3Q4Score,S3Q5Answer,S3Q5Score,S3Q6Answer,S3Q6Score,S3Q7Answer,S3Q7Score,S3Q8Answer,S3Q8Score,S3Q9Answer,S3Q9Score,S3Q10Answer,S3Q10Score,S4aQ1Answer,S4aQ2Answer,S4aQ3Answer,S4aQ4Answer,S4aQ5Answer,S4aQ6Answer,S4aQ7Answer,S4aQ8Answer,S4aQ9Answer,S4aQ10Answer,S4aQ11Answer,S4aQ12Answer,S4aQ13Answer,S4aQ14Answer,S4aQ15Answer,S4aQ16Answer,S4aQ17Answer,S4aQ18Answer,S4aQ19Answer,S4aQ20Answer,S4aQ21Answer,S4aQ22Answer,S4aQ23Answer,S4aQ24Answer,S4aQ25Answer,S4aQ26Answer,S4aQ27Answer,S4aQ28Answer,S4aQ29Answer,S4aQ30Answer,S4aQ31Answer,S4aQ32Answer,S4aQ33Answer,S4aQ34Answer,S4aQ35Answer,S4aQ36Answer,S4aQ37Answer,S4aQ38Answer,S4aQ39Answer,S4aQ40Answer,S4aQ41Answer,S4aQ42Answer,S4aQ43Answer,S4aQ44Answer,S4aQ45Answer,S4aQ46Answer,S4aQ47Answer,S4aQ48Answer,S4aQ49Answer,S4aQ50Answer,S4aQ51Answer,S4aQ52Answer,S4aQ53Answer,S4aQ54Answer,S4aQ55Answer,S4aQ56Answer,S4aQ57Answer,S4aQ58Answer,S4aQ59Answer,S4aQ60Answer,S4aQ61Answer,S4aQ62Answer,S4aQ63Answer,S4bQ1Answer,S4bQ2Answer,S4bQ3Answer,S4bQ4Answer,S4bQ5Answer,S4bQ6Answer,S4bQ7Answer,S4bQ8Answer,S4bQ9Answer,S4bQ10Answer,S4bQ11Answer,S4bQ12Answer,S4bQ13Answer,S4bQ14Answer,S4bQ15Answer,S4bQ16Answer,S4bQ17Answer,S4bQ18Answer,S4bQ19Answer,S4bQ20Answer,S4bQ21Answer,S4bQ22Answer,S4bQ23Answer,S4bQ24Answer,S4bQ25Answer,S4bQ26Answer,S4bQ27Answer,S4bQ28Answer,S4bQ29Answer,S4bQ30Answer,S4bQ31Answer,S4bQ32Answer,S4bQ33Answer,S4bQ34Answer,S4bQ35Answer,S4bQ36Answer,S4bQ37Answer,S4bQ38Answer,S4bQ39Answer,S4bQ40Answer,S4bQ41Answer,S4bQ42Answer,S4bQ43Answer,S4bQ44Answer,S4bQ45Answer,S4bQ46Answer,S4bQ47Answer,S4bQ48Answer,S4bQ49Answer,S4bQ50Answer,S4bQ51Answer,S4bQ52Answer,S4bQ53Answer,S4bQ54Answer,S4bQ55Answer,S4bQ56Answer,S4bQ57Answer,S4bQ58Answer,S4bQ59Answer,S4bQ60Answer,S4bQ61Answer,S4bQ62Answer,S4bQ63Answer")] Survey survey)
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
        public ActionResult Edit([Bind(Include = "SurveyID,StartTime,S1Q1Answer,S1Q2Answer,S1Q3Answer,S1Q3Score,S1Q4Answer,S1Q5Answer,S1Q6Answer,S1Q6Score,S1Q7Answer,S1Q8Answer,S1Q9Answer,S1Q9Score,S1Q10Answer,S1Q10Score,S1Q11Answer,S1Q12Answer,S1Q13Answer,S1Q13Score,S1Q14Answer,S1Q14Score,S1Q15Answer,S1Q16Answer,S1Q16Score,S1Q17Answer,S1Q18Answer,S1Q19Answer,S2Q1Answer,S2Q1Score,S2Q2Answer,S2Q2Score,S2Q3Answer,S2Q3Score,S2Q4Answer,S2Q4Score,S2Q5Answer,S2Q5Score,S2Q6Answer,S2Q6Score,S2Q7Answer,S2Q7Score,S2Q8Answer,S2Q8Score,S2Q9Answer,S2Q9Score,S2Q10Answer,S2Q10Score,S3Q1Answer,S3Q1Score,S3Q2Answer,S3Q2Score,S3Q3Answer,S3Q3Score,S3Q4Answer,S3Q4Score,S3Q5Answer,S3Q5Score,S3Q6Answer,S3Q6Score,S3Q7Answer,S3Q7Score,S3Q8Answer,S3Q8Score,S3Q9Answer,S3Q9Score,S3Q10Answer,S3Q10Score,S4aQ1Answer,S4aQ2Answer,S4aQ3Answer,S4aQ4Answer,S4aQ5Answer,S4aQ6Answer,S4aQ7Answer,S4aQ8Answer,S4aQ9Answer,S4aQ10Answer,S4aQ11Answer,S4aQ12Answer,S4aQ13Answer,S4aQ14Answer,S4aQ15Answer,S4aQ16Answer,S4aQ17Answer,S4aQ18Answer,S4aQ19Answer,S4aQ20Answer,S4aQ21Answer,S4aQ22Answer,S4aQ23Answer,S4aQ24Answer,S4aQ25Answer,S4aQ26Answer,S4aQ27Answer,S4aQ28Answer,S4aQ29Answer,S4aQ30Answer,S4aQ31Answer,S4aQ32Answer,S4aQ33Answer,S4aQ34Answer,S4aQ35Answer,S4aQ36Answer,S4aQ37Answer,S4aQ38Answer,S4aQ39Answer,S4aQ40Answer,S4aQ41Answer,S4aQ42Answer,S4aQ43Answer,S4aQ44Answer,S4aQ45Answer,S4aQ46Answer,S4aQ47Answer,S4aQ48Answer,S4aQ49Answer,S4aQ50Answer,S4aQ51Answer,S4aQ52Answer,S4aQ53Answer,S4aQ54Answer,S4aQ55Answer,S4aQ56Answer,S4aQ57Answer,S4aQ58Answer,S4aQ59Answer,S4aQ60Answer,S4aQ61Answer,S4aQ62Answer,S4aQ63Answer,S4bQ1Answer,S4bQ2Answer,S4bQ3Answer,S4bQ4Answer,S4bQ5Answer,S4bQ6Answer,S4bQ7Answer,S4bQ8Answer,S4bQ9Answer,S4bQ10Answer,S4bQ11Answer,S4bQ12Answer,S4bQ13Answer,S4bQ14Answer,S4bQ15Answer,S4bQ16Answer,S4bQ17Answer,S4bQ18Answer,S4bQ19Answer,S4bQ20Answer,S4bQ21Answer,S4bQ22Answer,S4bQ23Answer,S4bQ24Answer,S4bQ25Answer,S4bQ26Answer,S4bQ27Answer,S4bQ28Answer,S4bQ29Answer,S4bQ30Answer,S4bQ31Answer,S4bQ32Answer,S4bQ33Answer,S4bQ34Answer,S4bQ35Answer,S4bQ36Answer,S4bQ37Answer,S4bQ38Answer,S4bQ39Answer,S4bQ40Answer,S4bQ41Answer,S4bQ42Answer,S4bQ43Answer,S4bQ44Answer,S4bQ45Answer,S4bQ46Answer,S4bQ47Answer,S4bQ48Answer,S4bQ49Answer,S4bQ50Answer,S4bQ51Answer,S4bQ52Answer,S4bQ53Answer,S4bQ54Answer,S4bQ55Answer,S4bQ56Answer,S4bQ57Answer,S4bQ58Answer,S4bQ59Answer,S4bQ60Answer,S4bQ61Answer,S4bQ62Answer,S4bQ63Answer")] Survey survey)
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
