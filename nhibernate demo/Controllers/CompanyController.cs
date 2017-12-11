using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nhibernate_demo.Repositories.interfaces;
using nhibernate_demo.Models;
using Autofac;
namespace nhibernate_demo.Controllers
{
    public class CompanyController : Controller
    {
        public virtual ActionResult Index()
        {
            var repository = MvcApplication.container.Resolve<ICompanyRepository>();
            return View(repository.GetCompanies().ToList());
        }

        public virtual ActionResult Create()
        {
            // when its clicked on "create new company"
            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(Company newCompany)
        {
            var repository = MvcApplication.container.Resolve<ICompanyRepository>();
            if (String.IsNullOrEmpty(newCompany.Name))
            {
                ViewBag.notUniqueCompanyName = true;
                return View("Create");
            }
            var uniqueCompanyName = true;
            var companies = repository.GetCompanies().ToArray();
            var count = 0;
            while (uniqueCompanyName && count < companies.Count())
            {
                if (companies[count].Name.Equals(newCompany.Name))
                {
                    uniqueCompanyName = false;
                }
                else
                {
                    count++;
                }
            }
           
            if (uniqueCompanyName)
            {
                repository.Save(newCompany);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.notUniqueCompanyName = true;
                return View("Create");
            }
        
            
        }

        [HttpPost]
        public virtual ActionResult Edit(Company company)
        {
            var repository = MvcApplication.container.Resolve<ICompanyRepository>();
            var hasError = false;

            if (String.IsNullOrEmpty(company.Name))
            {
                ViewBag.EmptyName = true;
                hasError = true;
            }
            // todo add another dummy validation
            if (!hasError)
            {
                repository.Edit(company);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }

        public virtual ActionResult Delete(int id)
        {
            var repository = MvcApplication.container.Resolve<ICompanyRepository>();
            repository.Delete(id);
            return RedirectToAction("Index");
        }
    } 
}
