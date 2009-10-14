// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToLambdaExpression
#pragma warning disable 169
using System;
using System.Reflection;
using FluentNHibernate;
using Machine.Specifications;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using Environment=NHibernate.Cfg.Environment;

namespace neuralnet.tests
{
    public class with_in_memory_database : SpecBase
    {
        static Configuration Configuration;
        static ISessionFactory SessionFactory;
        static ISession Session;

        Establish context = () =>
                                {
                                    if (Configuration == null)
                                    {
                                        var assembly = Assembly.Load("Mylibrary.Web");

                                        Configuration = new Configuration()
                                            .SetProperty(Environment.ReleaseConnections, "on_close")
                                            .SetProperty(Environment.Dialect, typeof (SQLiteDialect).AssemblyQualifiedName)
                                            .SetProperty(Environment.ConnectionDriver, typeof (SQLite20Driver).AssemblyQualifiedName)
                                            .SetProperty(Environment.ConnectionString, "data source=:memory:")
                                            .SetProperty(Environment.ProxyFactoryFactoryClass,
                                                         typeof (ProxyFactoryFactory).AssemblyQualifiedName)
                                            .AddAssembly(assembly);

                

                                        var persistenceModel = new PersistenceModel();
                                        persistenceModel.AddMappingsFromAssembly(assembly);
                                        persistenceModel.Configure(Configuration);
                                        SessionFactory = Configuration.BuildSessionFactory();
                                    }

                                    Session = SessionFactory.OpenSession();

                                    new SchemaExport(Configuration).Execute(true, true, false, Session.Connection, Console.Out);
                                };
    }
}
#pragma warning restore 169
// ReSharper restore InconsistentNaming
// ReSharper restore ConvertToLambdaExpression