using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LearnXPO.XPO;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace LearnXPO.Controllers
{
    public class ExplorationController : Controller
    {
        private Session session => XpoDefault.Session;
        private UnitOfWork uow => new UnitOfWork();
        public ActionResult Index()
        {
            #region --- Criteria Operator (parse), Group Operator, Binary Operator, Between Operator ---
            //1. binary with condition
            BinaryOperator filter = new BinaryOperator("Age", "23");
            XPCollection<Customer> dataCustomer = new XPCollection<Customer>(session, filter);

            BinaryOperator filter1 = new BinaryOperator();
            XPCollection<Customer> dataCustomer1 = new XPCollection<Customer>(session, filter1);

            //2. Criteria Operator, Group Operator
            CriteriaOperator filter2 = CriteriaOperator.Parse("Age < 20");
            CriteriaOperator filter3 = CriteriaOperator.Parse("Age > 30");
            XPCollection<Customer> dataCustomer2 = new XPCollection<Customer>(session, GroupOperator.Or(filter2, filter3));

            

            CriteriaOperator filter4 = CriteriaOperator.Parse("Age > 20");
            CriteriaOperator filter5 = CriteriaOperator.Parse("Age < 30");
            XPCollection<Customer> dataCustomer3 = new XPCollection<Customer>(session, GroupOperator.And(filter4, filter5));

            //3. Between Operator
            CriteriaOperator filter6 = new BetweenOperator("Age", 20, 30);
            XPCollection<Customer> dataCustomer4 = new XPCollection<Customer>(session, filter6);

            //4. Contain Operator ???
            //ContainsOperator filter7 = new ContainsOperator("Name","on");
            //XPCollection<Customer> dataCustomer5 = new XPCollection<Customer>(session, filter7);

            #endregion
            return View();
        }
        public ActionResult LearnFilter()
        {
            XPCollection collection = new XPCollection(typeof(Customer), null);
            collection.Sorting.Add(new SortProperty("Age", DevExpress.Xpo.DB.SortingDirection.Ascending));
            collection.Sorting.Add(new SortProperty("Age", DevExpress.Xpo.DB.SortingDirection.Descending));
            collection.TopReturnedObjects = 3;

            //Contoh Pengguna'an Filter (Client Side)
            collection.Filter= new BetweenOperator("Age", 20, 30);
            collection.Filter = null;

            return View();
        }
        public ActionResult LearnTransaction()
        {
            session.BeginTransaction();
            Customer customer = new Customer(session);
            customer.Name = "Susan";
            customer.Age = 39;
            customer.Save();
            //session.CommitTransaction();
            session.RollbackTransaction();
            return View();
        }
        public ActionResult LearnUow()
        {
             Customer customer = uow.FindObject<Customer>(new BinaryOperator("Name", "Susan"));
            return View();
        }
        public ActionResult LearnPersistent() //???
        {
            uow.BeginTransaction();
            Customer customer = new Customer(uow);
            customer.Name = "Susanti";
            customer.Age = 42;
            //customer.Save();

            Customer susanti = uow.FindObject<Customer>(PersistentCriteriaEvaluationBehavior.BeforeTransaction, new BinaryOperator("Name","Susanti"));
            return View();
        }
    }
}