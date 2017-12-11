﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nhibernate_demo.Repositories;
using nhibernate_demo.Models;
using Autofac;
using nhibernate_demo.Repositories.interfaces;

namespace nhibernate_demo.Controllers
{
    public partial class FarmerController : Controller
    {

        public virtual ActionResult Index()
        {
            //ese container es algo propio del MVC o lo creaste vos Robb ?
            var repository = MvcApplication.container.Resolve<IFarmerRepository>();
            return View(repository.GetFarmers().ToList());
        }


        public virtual ActionResult Details(int id)
        {
            return View();
        }


        public virtual ActionResult Create()
        {
            ViewBag.Companies = MvcApplication.container.Resolve<ICompanyRepository>().GetCompanies();
            return View();
        } 

        [HttpPost]
        public virtual ActionResult Create(Farmer farmer)
        {
            var repository = MvcApplication.container.Resolve<IFarmerRepository>();
            repository.Save(farmer);
            return RedirectToAction("Index");
        }

        public virtual ActionResult Edit(int id)
        {
            var repository = MvcApplication.container.Resolve<IFarmerRepository>();
            return View(repository.GetByID(id));
        }

        public virtual ActionResult GetBiggestFarmer(string pantColor)
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Edit(Farmer farmer)
        {
            var repository = MvcApplication.container.Resolve<IFarmerRepository>();
            if (String.IsNullOrEmpty(farmer.Name))
            {
                ViewBag.EmptyName = true;
                return View("Edit");
            }
            else
            {
                repository.Edit(farmer);
                return RedirectToAction("Index");
            }    
        }

        public virtual ActionResult Delete(int id)
        {
            var repository = MvcApplication.container.Resolve<IFarmerRepository>();
            repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
