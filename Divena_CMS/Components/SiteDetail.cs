using Divena_CMS.Data;
using Divena_CMS.Infrastructure;
using Divena_CMS.Models;
using Divena_CMS.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Divena_CMS.Components
{
    public class SiteInfo : ViewComponent
    {
        private IWebHostEnvironment hostingEnvironment;

        public SiteInfo(IWebHostEnvironment environment)
        {
            hostingEnvironment = environment;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Info info = new Info();

            if (!System.IO.File.Exists(System.IO.Path.Combine(hostingEnvironment.WebRootPath, "json/info.xml")))
                CreateXml(info);

            XmlSerializer xmlFormat = new XmlSerializer(typeof(Info));
            using (Stream stream = System.IO.File.OpenRead(System.IO.Path.Combine(hostingEnvironment.WebRootPath, "json/info.xml")))
            {
                info = (Info)xmlFormat.Deserialize(stream);
            }

            info.logo = info.logo != null ? info.logo : "/images/addphoto.jpg";
            return View(info);
        }

        void CreateXml(Info info)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(Info));
            using (Stream fStream = new FileStream(System.IO.Path.Combine(hostingEnvironment.WebRootPath, "json/info.xml"), FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, info);
            }
        }
    }
    public class SiteMenu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Menu menu = new Menu();
            using (var context = new Divena_CMSContext())
            {
                menu = context.Menu.Where(x => x.Name == "Main").FirstOrDefault();
            }
            if (menu != null)
            {
                return View((object)BindMenu(menu));
            }
            else
            {
                string childString = "<ul><li><a href=home> Home</a></li><ul>";
                return View((object)childString);
            }
        }

        string BindMenu(Menu menu)
        {
            //json has [] therore changed the below code accordingly
            var rootObject = JsonConvert.DeserializeObject<List<MenuJsonRoot>>(menu.Item);
            string mainString = "<ul>";

            for (int i = 0; i < rootObject.Count; i++)
            {
                var children = rootObject[i].children;
                if (children != null)
                {
                    string childString = "";
                    for (int j = 0; j < children.Count; j++)
                        childString = childString + CreateMenuItem(children[j]);
                    childString = "<ul>" + childString + "</ul>";

                    string parentString = "";
                    MenuJsonChild child = new MenuJsonChild();
                    child.deleted = rootObject[i].deleted;
                    child.@new = rootObject[i].@new;
                    child.slug = rootObject[i].slug;
                    child.name = rootObject[i].name;
                    child.id = rootObject[i].id;
                    parentString = CreateMenuItem(child).Replace("</li>", "") + childString + "</li>";
                    mainString = mainString + parentString;
                }
                else
                {
                    string parentString = "";
                    MenuJsonChild child = new MenuJsonChild();
                    child.deleted = rootObject[i].deleted;
                    child.@new = rootObject[i].@new;
                    child.slug = rootObject[i].slug;
                    child.name = rootObject[i].name;
                    child.id = rootObject[i].id;

                    parentString = CreateMenuItem(child);
                    mainString = mainString + parentString;
                }
            }
            mainString = mainString + "</ul>";
            return mainString;
        }

        string CreateMenuItem(MenuJsonChild child)
        {
            string childString = "<li><a href=\"" + child.slug + "\">" + child.name + "</a></li>";
            return childString;
        }
    }
    public class SiteFooter : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Menu menu = new Menu();
            using (var context = new Divena_CMSContext())
            {
                menu = context.Menu.Where(x => x.Name == "Footer").FirstOrDefault();
            }
            if (menu != null)
            {
                return View((object)BindMenu(menu));
            }
            else
            {
                string childString = "<ul><li><a href=> Home</a></li><ul>";
                return View((object)childString);
            }
        }

        string BindMenu(Menu menu)
        {
            //json has [] therore changed the below code accordingly
            var rootObject = JsonConvert.DeserializeObject<List<MenuJsonRoot>>(menu.Item);
            string mainString = "<ul>";

            for (int i = 0; i < rootObject.Count; i++)
            {
                var children = rootObject[i].children;
                if (children != null)
                {
                    string childString = "";
                    for (int j = 0; j < children.Count; j++)
                        childString = childString + CreateMenuItem(children[j]);
                    childString = "<ul>" + childString + "</ul>";

                    string parentString = "";
                    MenuJsonChild child = new MenuJsonChild();
                    child.deleted = rootObject[i].deleted;
                    child.@new = rootObject[i].@new;
                    child.slug = rootObject[i].slug;
                    child.name = rootObject[i].name;
                    child.id = rootObject[i].id;
                    parentString = CreateMenuItem(child).Replace("</li>", "") + childString + "</li>";
                    mainString = mainString + parentString;
                }
                else
                {
                    string parentString = "";
                    MenuJsonChild child = new MenuJsonChild();
                    child.deleted = rootObject[i].deleted;
                    child.@new = rootObject[i].@new;
                    child.slug = rootObject[i].slug;
                    child.name = rootObject[i].name;
                    child.id = rootObject[i].id;

                    parentString = CreateMenuItem(child);
                    mainString = mainString + parentString;
                }
            }
            mainString = mainString + "</ul>";
            return mainString;
        }

        string CreateMenuItem(MenuJsonChild child)
        {
            string childString = "<li><a href=\"" + child.slug + "\">" + child.name + "</a></li>";
            return childString;
        }
    }
    public class SiteSocial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Menu menu = new Menu();
            using (var context = new Divena_CMSContext())
            {
                menu = context.Menu.Where(x => x.Name == "Social").FirstOrDefault();
            }
            if (menu != null)
            {
                return View((object)BindMenu(menu));
            }
            else
            {
                string childString = "<ul><li><a href=https://www.facebook.com /> www.facebook.com</a></li><ul>";
                return View((object)childString);
            }
        }

        string BindMenu(Menu menu)
        {
            //json has [] therore changed the below code accordingly
            var rootObject = JsonConvert.DeserializeObject<List<MenuJsonRoot>>(menu.Item);
            string mainString = "<ul>";

            for (int i = 0; i < rootObject.Count; i++)
            {
                var children = rootObject[i].children;
                if (children != null)
                {
                    string childString = "";
                    for (int j = 0; j < children.Count; j++)
                        childString = childString + CreateMenuItem(children[j]);
                    childString = "<ul>" + childString + "</ul>";

                    string parentString = "";
                    MenuJsonChild child = new MenuJsonChild();
                    child.deleted = rootObject[i].deleted;
                    child.@new = rootObject[i].@new;
                    child.slug = rootObject[i].slug;
                    child.name = rootObject[i].name;
                    child.id = rootObject[i].id;
                    parentString = CreateMenuItem(child).Replace("</li>", "") + childString + "</li>";
                    mainString = mainString + parentString;
                }
                else
                {
                    string parentString = "";
                    MenuJsonChild child = new MenuJsonChild();
                    child.deleted = rootObject[i].deleted;
                    child.@new = rootObject[i].@new;
                    child.slug = rootObject[i].slug;
                    child.name = rootObject[i].name;
                    child.id = rootObject[i].id;

                    parentString = CreateMenuItem(child);
                    mainString = mainString + parentString;
                }
            }
            mainString = mainString + "</ul>";
            return mainString;
        }

        string CreateMenuItem(MenuJsonChild child)
        {
            string childString = "<li><a href=\"" + child.slug + "\">" + child.name + "</a></li>";
            return childString;
        }
    }
}
