using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nhibernate_demo.Models;

namespace nhibernate_demo.Repositories.interfaces
{
    public interface ICompanyRepository
    {
        IQueryable<Company> GetCompanies();
        Company GetByID(int id);
        void Delete(int id);
        void Save(Company company);
        void Edit(Company company);
        IQueryable<Company> GetEcoFriendlyCompanies();
    }
}