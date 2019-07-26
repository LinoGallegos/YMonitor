using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YMonitor.Controllers
{
    public class RoleController : Controller

    {
        Models.ApplicationDbContext context;
        //class AddingRoles.Models.ApplicationDbContext

        public RoleController()
        {
            context = new Models.ApplicationDbContext();

        }

        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }

        public ActionResult Create()
        {
            var Roles = new IdentityRole();
            return View(Roles);
        }
            [HttpPost]
            public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        }

    }
