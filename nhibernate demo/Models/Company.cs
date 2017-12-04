using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nhibernate_demo.Models
{
    public class Company
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual bool EcoFriendly { get; set; } // if the company is ecoFriendly or not


    }
}