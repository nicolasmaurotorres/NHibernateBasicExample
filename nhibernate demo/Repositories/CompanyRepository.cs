using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using nhibernate_demo.Models;
using nhibernate_demo.Repositories.interfaces;
using NHibernate.Linq; // se usa para el _session.Query y poder queriar a la base de datos

namespace nhibernate_demo.Repositories
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository(ISession session) : base(session) {}

        public void Delete(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(_session.Load<Company>(id));
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (transaction.IsActive)
                    {
                        transaction.Rollback();
                    }
                    throw ex;
                }
            }
        }

        public void Edit(Company company)
        {
            var instanceCompany = _session.Get<Company>(company.ID);
            using (var transaction = _session.BeginTransaction())
            {
                instanceCompany.Name = company.Name;
                _session.Update(instanceCompany);
                transaction.Commit();

            }
        }

        public Company GetByID(int id)
        {
            return _session.Load<Company>(id);
        }

        public IQueryable<Company> GetCompanies()
        {
            return _session.Query<Company>();
        }

        public IQueryable<Company> GetEcoFriendlyCompanies()
        {
            // testear si devuelve algo
            return _session.Query<Company>().Where(x => x.EcoFriendly == true);
        }

        public void Save(Company company)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(company);
                transaction.Commit();
            }
        }
    }
}