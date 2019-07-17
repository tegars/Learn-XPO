using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using LearnXPO.XPO;
namespace LearnXPO.Controllers
{
    public class HomeController : Controller
    {
        //public Session session => XpoDefault.Session;
        Session session1;
        public ActionResult Index()
        {
            session1 = new Session();
            //session1 = XPOUtils.
            XpoDefault.Session = session1;
            //XPCollection collection = new XPCollection(typeof(Customer));
            //var collection = new XPCollection<Customer>(session);
            XPCollection<Customer> collection = new XPCollection<Customer>(session1, new BinaryOperator("Age", "21"));

            Customer oneRow = session1.GetObjectByKey<Customer>(1);

            Customer oneRow2 = session1.FindObject<Customer>(new BinaryOperator("Name", "Tono"));
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}