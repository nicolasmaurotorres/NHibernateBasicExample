using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Configuration;
using NHibernate.Tool.hbm2ddl;
using nhibernate_demo.Maps;

namespace nhibernate_demo.Repositories
{
    public class SessionFactoryProvider // Singleton
    {
        private static ISessionFactory _session;

        public static ISessionFactory BuildSessionFactory()
        {
            if (_session == null)
            {
                _session = Fluently.Configure()
                                   .Mappings(x => x.FluentMappings.AddFromAssemblyOf<GrainMap>().Conventions.AddFromAssemblyOf<CascadeConvention>())
                                   .Database(MsSqlConfiguration.MsSql2008.ConnectionString(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                                   .ExposeConfiguration(UpdateSchema)
                                   .CurrentSessionContext("web")
                                   .BuildSessionFactory();
                  /*
                   _sessionFactory = Fluently.Configure()
                                             .Database(MsSqlConfiguration.MsSql2008)
                                             .ConnectionString(@"Server=(local);initial catalog=xxxxx;user=xxxxx;password=xxxxx;") // Modify your ConnectionString
                                             .ShowSql()
                                             .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                                             .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                                             .BuildSessionFactory();
                  */

            }
            return _session;
        }

        private static void UpdateSchema(NHibernate.Cfg.Configuration conf)
        {
            bool cleanDatabaseOnLaunch = false;
            if (ConfigurationManager.AppSettings["CleanDatabaseOnLaunch"] !=  null 
             && bool.TryParse(ConfigurationManager.AppSettings["CleanDatabaseOnLaunch"], out cleanDatabaseOnLaunch) 
             && cleanDatabaseOnLaunch)
            {
                new SchemaExport(conf).Execute(true, true, false);
            }
            
            new SchemaUpdate(conf).Execute(true, true);
        }
    }
}