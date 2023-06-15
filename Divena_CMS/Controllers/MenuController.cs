using Divena_CMS.Data;
using Divena_CMS.Infrastructure;
using Divena_CMS.Models;
using Divena_CMS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Divena_CMS.Controllers
{
    [Authorize(Roles = "Admin,Subadmin,Client")]
    public class MenuController : Controller
    {
        private readonly Divena_CMSContext _context;

        public MenuController(Divena_CMSContext context)
        {
            _context = context;
        }

        // GET: Menus
        public async Task<IActionResult> Index(int? id, string searchText, int? status)
        {
            MenuList list = new MenuList();
            list = GetMenu(id, searchText, status);
            return View(list);
        }
        public MenuList GetMenu(int? page, string searchText, int? status)
        {
            int pageSize = 10;
            int pageNo = page == null ? 1 : Convert.ToInt32(page);

            var skip = pageSize * (Convert.ToInt32(pageNo) - 1);
            MenuList list = new MenuList();


            var result = _context.Menu.Where(x => x.Status == (status == null ? x.Status : (status == 1 ? true : false))).OrderByDescending(x => x.Id).Skip(skip).Take(pageSize).ToList();

            int total = _context.Menu.Where(x => x.Status == (status == null ? x.Status : (status == 1 ? true : false))).Count();

            PagingInfo pagingInfo = new PagingInfo();
            pagingInfo.CurrentPage = pageNo;
            pagingInfo.TotalItems = total;
            pagingInfo.ItemsPerPage = pageSize;

            list.menu = result;
            list.allTotal = _context.Menu.Count();
            list.activeTotal = _context.Menu.Where(x => x.Status == true).Count();
            list.inactiveTotal = _context.Menu.Where(x => x.Status == false).Count();
            list.searchText = searchText;
            list.status = status;
            list.pagingInfo = pagingInfo;


            return list;
        }

        public IActionResult Add(int? id)
        {
            Menu menu = new Menu();
            if (id != null)
            {

                menu = _context.Menu.Where(x => x.Id == id).FirstOrDefault();
                BindMenu(menu);
                ViewBag.Title = "Update Menu";
                return View(menu);

            }

            ViewBag.Title = "Add New Menu";
            return View(menu);
        }

        [HttpPost]
        public IActionResult Add(Menu menu, int? id)
        {
            if (ModelState.IsValid)
            {

                if (id == null)
                {
                    _context.Menu.Add(menu);
                    int result = _context.SaveChanges();

                    TempData["result"] = result == 1 ? "Insert Successful" : "Failed";
                    if (result == 1)
                        return RedirectToAction("add", new { id = menu.Id });
                }
                else
                {
                    var menuResult = _context.Menu.Where(x => x.Id == id).FirstOrDefault();
                    menuResult.Id = menu.Id;
                    menuResult.Name = menu.Name;
                    menuResult.Item = menu.Item;
                    menuResult.Status = Convert.ToBoolean(menu.Status);

                    int result = _context.SaveChanges();
                    ModelState.Clear();
                    BindMenu(menu);
                    TempData["result"] = result == 1 ? "Update Successful" : "Failed";
                }

            }
            ViewBag.Title = id == null ? "Add New Menu" : "Update Menu";
            return View(menu);
        }

        void BindMenu(Menu menu)
        {
            //json has [] therore changed the below code accordingly
            var rootObject = JsonConvert.DeserializeObject<List<MenuJsonRoot>>(menu.Item == null ? "[]" : menu.Item);
            string mainString = "<ol class=\"dd-list\">";

            for (int i = 0; i < rootObject.Count; i++)
            {
                var children = rootObject[i].children;
                if (children != null)
                {
                    string childString = "";
                    for (int j = 0; j < children.Count; j++)
                        childString = childString + CreateMenuItem(children[j]);
                    childString = "<ol class=\"dd-list\">" + childString + "</ol>";

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
            mainString = mainString + "</ol>";
            ViewBag.Menu = mainString;
        }

        string CreateMenuItem(MenuJsonChild child)
        {
            string childString = "<li class=\"dd-item\" data-id=\"" + child.id + "\" data-name=\"" + child.name + "\" data-slug=\"" + child.slug + "\" data-new=\"" + child.@new + "\" data-deleted=\"" + child.deleted + "\"><div class=\"dd-handle\">" + child.name + "</div><span class=\"button-delete btn btn-default btn-xs pull-right\" data-owner-id=\"" + child.id + "\"><i class=\"fa fa-times-circle-o\" aria-hidden=\"true\"></i></span><span class=\"button-edit btn btn-default btn-xs pull-right\" data-owner-id=\"" + child.id + "\"><i class=\"fa fa-pencil\" aria-hidden=\"true\"></i></span></li>";
            return childString;
        }

        public string UpdateBulkStatus(string idChecked, string statusToChange)
        {
            string result = "";
            if (statusToChange == "Status")
                result = "Select Status";
            else if (idChecked == null)
                result = "Select at least 1 item";
            else
            {

                var menu = _context.Menu.Where(x => idChecked.Contains(x.Id.ToString())).ToList();
                menu.ForEach(x => x.Status = Convert.ToBoolean(Convert.ToInt32(statusToChange)));
                _context.SaveChanges().ToString();
                result = "Success";

            }
            return result;
        }
    }
}
