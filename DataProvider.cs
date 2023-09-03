using MasterForms.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MasterForms
{
    public class DataProvider
    {
        private static DataProvider dataProvider;
        public static DataProvider Ins
        {
            get
            {
                if (dataProvider == null)
                {
                    dataProvider = new DataProvider();
                }
                return dataProvider;
            }
            set
            {
                dataProvider = value;
            }
        }

        private DataProvider()
        {
            if (MyDB == null)
            {
                MyDB = new MasterFormsDBContext();
                AutoMigration();
            }
        }

        public MasterFormsDBContext MyDB;

        private void AutoMigration()
        {
            //var context = DataProvider.Ins.MyDB;
            var context = MyDB;
            var internalContext = context.GetType().GetProperty("InternalContext", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(context);
            var providerName = (string)internalContext.GetType().GetProperty("ProviderName").GetValue(internalContext);

            var configuration = new Configuration()
            {
                TargetDatabase = new DbConnectionInfo(context.Database.Connection.ConnectionString, providerName)
            };

            var migrator = new DbMigrator(configuration);

            migrator.Update();
        }
    }
}
