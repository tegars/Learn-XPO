using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using LearnXPO.XPO;
namespace LearnXPO
{
    public static class Connection
    {
        public static void dbconnect()
        {
            try
            {
                const string ConnectionString = @"XpoProvider=MSSqlServer;data source=WFR-BTM-046;integrated security=SSPI;initial catalog=LearnXPO; uid=sa; pwd=aaaa123";
                XpoDefault.DataLayer = XpoDefault.GetDataLayer(ConnectionString, AutoCreateOption.DatabaseAndSchema);
                using (Session session = new Session(XpoDefault.DataLayer))
                {
                    var assembly = typeof(Customer).Assembly;
                    session.CreateObjectTypeRecords(assembly);
                    session.UpdateSchema(assembly);
                }
                XpoDefault.Session = null;
            }
            catch (Exception ex)
            {

            }
        }
    }
}