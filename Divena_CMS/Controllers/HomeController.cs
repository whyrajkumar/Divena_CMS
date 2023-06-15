using Divena_CMS.Data;
using Divena_CMS.Infrastructure;
using Divena_CMS.Interface;
using Divena_CMS.Models;
using Divena_CMS.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//https://www.free-css.com/assets/files/free-css-templates/preview/page235/words/
namespace Divena_CMS.Controllers
{
    public class HomeController : Controller
    {
        //private UserManager<AppUser> userManager;
        //public HomeController(UserManager<AppUser> userMgr)
        //{
        //    userManager = userMgr;
        //}
        private readonly IMemoryCache _cache;
        private IEmailHelper _emailHelper;
        public HomeController(IMemoryCache memoryCache, IEmailHelper emailHelper)
        {
            _cache = memoryCache;
            _emailHelper = emailHelper;
        }

        public async Task<IActionResult> Index()
        {
            Page page = new Page();
            using (var context = new Divena_CMSContext())
            {
                page = await context.Page.Where(t => t.Name == "Home").FirstOrDefaultAsync();
            }
            return View(page);
        }

        public async Task<IActionResult> Page(string url)
        {
            Page page = new Page();
            using (var context = new Divena_CMSContext())
            {
                page = await context.Page.Where(t => t.Url == url).FirstOrDefaultAsync();
            }
            if (page != null)
                return View("Index", page);
            else
                return new NotFoundViewResult("error404");
        }

        public async Task<IActionResult> ViewBlog(int id)
        {
            Blog blog = new Blog();
            using (var context = new Divena_CMSContext())
            {
                blog = await context.Blog.Where(t => t.Id == id).FirstOrDefaultAsync();
                if (blog == null)
                {
                    return new NotFoundViewResult("error404");
                }
                blog.PrimaryImageUrl = null;
                blog.PrimaryImageUrl = blog.PrimaryImageId != null ? "/" + context.Media.Where(x => x.Id == blog.PrimaryImageId).Select(x => x.Url).FirstOrDefault() : "/images/addphoto.jpg";

                ViewBag.BlogCategory = context.BlogCategory.Where(t => t.Status == true).ToList();
            }
            return View(blog);

        }

        public async Task<IActionResult> MyBlogCategory(string name, int id, string url)
        {
            BlogList list = new BlogList();

            BlogCategory blogCategory = new BlogCategory();
            using (var context = new Divena_CMSContext())
            {
                blogCategory = await context.BlogCategory.Where(x => x.Url == url).FirstOrDefaultAsync();
            }

            list = GetBlog(id, null, 1, blogCategory.Id);

            ViewData["Meta"] = new string[3] { blogCategory.Name, "", "Welcome to My Blogs" };
            ViewBag.url = url;
            return View("MyBlog", list);
        }

        public IActionResult MyBlog(int id)
        {
            BlogList list = new BlogList();
            list = GetBlog(id, null, 1, 0);

            ViewData["Meta"] = new string[3] { "My blogs", "", "Welcome to My Blogs" };
            return View(list);
        }

        public BlogList GetBlog(int? page, string searchText, int? status, int blogCategoryId)
        {

            int pageSize = 3;
            int pageNo = page == null ? 1 : Convert.ToInt32(page);

            var skip = pageSize * (Convert.ToInt32(pageNo) - 1);

            BlogList bList = new BlogList();
            using (var context = new Divena_CMSContext())
            {
                var result = context.Blog.Where(x => x.Status == (status == null ? x.Status : (status == 1 ? true : false)) && x.Name.Contains(searchText == null ? x.Name : searchText) && (blogCategoryId == 0 || x.CategoryId == blogCategoryId)).OrderByDescending(x => x.Id).Skip(skip).Take(pageSize).ToList();
                result.ForEach(u => u.PrimaryImageUrl = u.PrimaryImageId != null ? "/" + context.Media.Where(x => x.Id == u.PrimaryImageId).Select(x => x.Url).FirstOrDefault() : "/images/addphoto.jpg");

                int total = context.Blog.Where(x => x.Status == (status == null ? x.Status : (status == 1 ? true : false)) && x.Name.Contains(searchText == null ? x.Name : searchText) && (blogCategoryId == 0 || x.CategoryId == blogCategoryId)).Count();

                PagingInfo pagingInfo = new PagingInfo();
                pagingInfo.CurrentPage = pageNo;
                pagingInfo.TotalItems = total;
                pagingInfo.ItemsPerPage = pageSize;

                bList.blog = result;
                bList.allTotal = context.Blog.Count();
                bList.activeTotal = context.Blog.Where(x => x.Status == true).Count();
                bList.inactiveTotal = context.Blog.Where(x => x.Status == false).Count();
                bList.searchText = searchText;
                bList.status = null;
                bList.pagingInfo = pagingInfo;
            }
            return bList;
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public class NotFoundViewResult : ViewResult
        {
            public NotFoundViewResult(string viewName)
            {
                ViewName = viewName;
                StatusCode = (int)HttpStatusCode.NotFound;
            }
        }



        #region Sitemap and Robots
        //Robots File and SiteMap



        [Route("robots.txt", Name = "GetRobotsText")]
        public ContentResult RobotsText()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("user-agent: *");
            stringBuilder.AppendLine("disallow: /error/");
            stringBuilder.AppendLine("Disallow: /Admin/");
            stringBuilder.AppendLine("Disallow: /Identity/");
            stringBuilder.AppendLine("allow: /error/foo");
            stringBuilder.Append("sitemap: ");
            stringBuilder.AppendLine(this.Url.RouteUrl("sitemapindex", null, this.Request.Scheme).TrimEnd('/'));


            return this.Content(stringBuilder.ToString(), "text/plain", Encoding.UTF8);
        }


        [Route("sitemap_index.xml", Name = "sitemapindex")]
        public async Task<IActionResult> SitemapXml()
        {

            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            string segment = "blog";
            string contentType = "application/xml";

            string cacheKey = "sitemap_index.xml";

            // For showing in browser (Without download)
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = cacheKey,
                Inline = true,
            };
            Response.Headers.Append("Content-Disposition", cd.ToString());

            // Cache
            var bytes = _cache.Get<byte[]>(cacheKey);
            if (bytes != null)
                return File(bytes, contentType);

            Divena_CMSContext context = new Divena_CMSContext();

            var blogs = await context.Blog.ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine($"<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"");
            sb.AppendLine($"   xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
            sb.AppendLine($"   xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");


            var dt = DateTime.Now;
            string lastmod = $"{dt.Year}-{dt.Month.ToString("00")}-{dt.Day.ToString("00")}";

            sb.AppendLine($"    <url>");
            //sb.AppendLine($"        <loc>{baseUrl}/{segment}/{m.Url}</loc>");
            sb.AppendLine($"        <loc>{this.Url.RouteUrl("pagesitemap", null, this.Request.Scheme).TrimEnd('/')}</loc>");
            sb.AppendLine($"        <loc>{this.Url.RouteUrl("postsitemap", null, this.Request.Scheme).TrimEnd('/')}</loc>");
            sb.AppendLine($"        <lastmod>{lastmod}</lastmod>");
            sb.AppendLine($"        <changefreq>daily</changefreq>");
            sb.AppendLine($"        <priority>0.8</priority>");

            sb.AppendLine($"    </url>");

            sb.AppendLine($"</urlset>");

            bytes = Encoding.UTF8.GetBytes(sb.ToString());

            _cache.Set(cacheKey, bytes, TimeSpan.FromHours(24));
            return File(bytes, contentType);

        }
        [Route("/page-sitemap.xml", Name = "pagesitemap")]
        public async Task<IActionResult> SitemapPage()
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            // string segment = "page";
            string contentType = "application/xml";

            string cacheKey = "page-sitemap.xml";

            // For showing in browser (Without download)
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = cacheKey,
                Inline = true,
            };
            Response.Headers.Append("Content-Disposition", cd.ToString());

            // Cache
            var bytes = _cache.Get<byte[]>(cacheKey);
            if (bytes != null)
                return File(bytes, contentType);

            Divena_CMSContext context = new Divena_CMSContext();

            var pages = await context.Page.Where(x => x.Status == true).ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine($"<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"");
            sb.AppendLine($"   xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
            sb.AppendLine($"   xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");

            foreach (var m in pages)
            {
                var dt = m.AddedOn;
                string lastmod = $"{dt.Year}-{dt.Month.ToString("00")}-{dt.Day.ToString("00")}";

                sb.AppendLine($"    <url>");
                //sb.AppendLine($"        <loc>{baseUrl}/{segment}/{m.Url}</loc>");
                sb.AppendLine($"        <loc>{baseUrl}/{m.Url}</loc>");
                sb.AppendLine($"        <lastmod>{lastmod}</lastmod>");
                sb.AppendLine($"        <changefreq>daily</changefreq>");
                sb.AppendLine($"        <priority>0.8</priority>");

                sb.AppendLine($"    </url>");
            }

            sb.AppendLine($"</urlset>");

            bytes = Encoding.UTF8.GetBytes(sb.ToString());

            _cache.Set(cacheKey, bytes, TimeSpan.FromHours(24));
            return File(bytes, contentType);
        }

        [Route("/post-sitemap.xml", Name = "postsitemap")]
        public async Task<IActionResult> SitemapBlog1()
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            string segment = "blog";
            string contentType = "application/xml";

            string cacheKey = "post-sitemap.xml";

            // For showing in browser (Without download)
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = cacheKey,
                Inline = true,
            };
            Response.Headers.Append("Content-Disposition", cd.ToString());

            // Cache
            var bytes = _cache.Get<byte[]>(cacheKey);
            if (bytes != null)
                return File(bytes, contentType);

            Divena_CMSContext context = new Divena_CMSContext();

            var blogs = await context.Blog.Where(x => x.Status == true).ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine($"<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"");
            sb.AppendLine($"   xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
            sb.AppendLine($"   xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");

            foreach (var m in blogs)
            {
                var dt = m.AddedOn;
                string lastmod = $"{dt.Year}-{dt.Month.ToString("00")}-{dt.Day.ToString("00")}";

                sb.AppendLine($"    <url>");
                //sb.AppendLine($"        <loc>{baseUrl}/{segment}/{m.Url}</loc>");
                sb.AppendLine($"        <loc>{baseUrl}/{m.Url}-id-{m.Id}</loc>");
                sb.AppendLine($"        <lastmod>{lastmod}</lastmod>");
                sb.AppendLine($"        <changefreq>daily</changefreq>");
                sb.AppendLine($"        <priority>0.8</priority>");

                sb.AppendLine($"    </url>");
            }

            sb.AppendLine($"</urlset>");

            bytes = Encoding.UTF8.GetBytes(sb.ToString());

            _cache.Set(cacheKey, bytes, TimeSpan.FromHours(24));
            return File(bytes, contentType);
        }

        #endregion


        #region Email
        public async Task<IActionResult> SendEmail(Contact model)
        {
            var emailModel = new EmailModel("bizdigitize@gmail.com", // To  
                "Email Test", // Subject  
                "Sending Email using Asp.Net Core.", // Message  
                false // IsBodyHTML  
            );
            if (ModelState.IsValid)
            {
                try {
                    await _emailHelper.SendCustomEmailAsync(model.Name, model.Email, model.Subject, model.Message);
                }
                catch
                {
                    return Json(new { success = false, message = "Your message was Fail." });
                }

            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        // Enter your email address and password
            //        var credentials = new NetworkCredential("YourEmailAddressHere", "YourPasswordHere");

            //        var mail = new MailMessage()
            //        {
            //            From = new MailAddress("YourEmailAddressHere"), // Enter your email address
            //            Subject = "Website Inquiry",
            //            Body = FormattedBody(form.Name, form.Email,"", form.Message)
            //        };

            //        mail.IsBodyHtml = true;
            //        mail.To.Add(new MailAddress("YourEmailAddressHere")); // Enter your email address

            //        // You may have to tweak these settings depending on your mail server's requirements
            //        var client = new SmtpClient()
            //        {
            //            UseDefaultCredentials = false,
            //            Host = "mail.somedomain.com", // Enter your mail server host
            //            Credentials = credentials,
            //            /* Port = 587,
            //            EnableSsl = true */
            //        };

            //        //if (!Validate(form.ReCaptcha))
            //        //{
            //        //    throw new Exception("The submission failed the spam bot verification. If you have " +
            //        //        "JavaScript disabled in your browser, please enable it and try again.");
            //        //}
            //        else
            //        {
            //            client.Send(mail);
            //        }

            //        return Json(new { success = true, message = "Your message was successfully sent." });
            //    }
            //    catch (Exception ex)
            //    {
            //        return Json(new { success = false, message = ex.Message });
            //    }
            //}

            return Json(new { success = true, message = "Your message was successfully sent." });

        }


        #endregion

    }
}