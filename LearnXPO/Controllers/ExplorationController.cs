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
        private UnitOfWork uow; 
        public ExplorationController()
        {
            uow = new UnitOfWork();
            uow.ConnectionString = @"XpoProvider=MSSqlServer;data source=WFR-BTM-046;integrated security=SSPI;initial catalog=LearnXPO; uid=sa; pwd=aaaa123";
        }
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

            //5. In Operator
            //xpCollection1.Filter = new InOperator("Name", new string[] {"John", "Mike", "Nick"});

            #endregion
            return View();
        }
        public ActionResult LearnFilter()
        {
            XPCollection collection = new XPCollection(typeof(Customer), null);
            collection.Sorting.Add(new SortProperty("Age", DevExpress.Xpo.DB.SortingDirection.Ascending));
            collection.Sorting.Add(new SortProperty("Age", DevExpress.Xpo.DB.SortingDirection.Descending));
            collection.TopReturnedObjects = 15;

            //Contoh Pengguna'an Filter (Client Side)
            //collection.Filter= new BetweenOperator("Age", 20, 30);
            collection.Filter = null;
            collection.TopReturnedObjects = 2;

            //ketika sudah di komsumsi "collection.TopReturnedObjects" sudah tidak bisa bekerja
            //contoh di komsumsi collection.count > 0
            //ketika sudah di sorot mode debugging, "TopReturnedObjects" pun juga sudah tidak bisa bekerja
            return View();
        }
        public ActionResult LearnTopReturn()
        {
            XPCollection<Customer> Customer = new XPCollection<Customer>(uow);
            Customer.TopReturnedObjects = 5;

            //Contoh Pengguna'an Filter (Client Side)
            //collection.Filter= new BetweenOperator("Age", 20, 30);
            Customer.Filter = null;
            Customer.TopReturnedObjects = 2;

            return View();
        }
        public ActionResult LearnUow()
        {
            Customer customer = uow.FindObject<Customer>(new BinaryOperator("Name", "Susan"));
            XPCollection<Customer> listCustomer = new XPCollection<Customer>(uow);

            //uow = insert update delete harus pakai begin transaction
            uow.BeginTransaction();
            Customer newCustomer = new Customer(uow);
            newCustomer.Name = "Susanti";
            newCustomer.Age = 42;
            newCustomer.Save();
            //uow.RollbackTransaction();
            uow.CommitTransaction();

            return View();
        }
        public ActionResult LearnPersistent()
        {
            uow.BeginTransaction();
            Customer customer = new Customer(uow);
            customer.Name = "Susanti";
            customer.Age = 42;
            
            Customer susanti = uow.FindObject<Customer>(PersistentCriteriaEvaluationBehavior.InTransaction, new BinaryOperator("Name","Susanti"));
            Customer susanti2 = uow.FindObject<Customer>(new BinaryOperator("Name","Susanti"));
            return View();
        }
    }
}