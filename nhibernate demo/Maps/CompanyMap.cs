using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping; // import del NHibernate
using nhibernate_demo.Models;

namespace nhibernate_demo.Maps
{
    public class CompanyMap : ClassMap<Company>
    {
        public CompanyMap()
        {

            Id(x => x.ID);

            Map(x => x.Name); // Map: Used to create column for the selected property
        }
    }
}