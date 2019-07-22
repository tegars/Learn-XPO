using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using LearnXPO.XPO;
using LearnXPO.Models;
namespace LearnXPO.Controllers
{
    public class CustomerController : Controller
    {
        private Session session => XpoDefault.Session;
        public ActionResult Index()
        {
            XPCollection<Customer> dataCustomer = new XPCollection<Customer>(session);
            var model = new ListCustomerVM();
            model.customer = dataCustomer.Select(x => new CustomerVM { OID = x.Oid, Name = x.Name, Age = x.Age }).ToList();    
            return View(model);
        }
        public ActionResult Detail(int id)
        {
            var model = new CustomerVM();
            Customer dataCustomer = session.GetObjectByKey<Customer>(id);
            if (dataCustomer!=null)
            {
                model.OID = dataCustomer.Oid;
                model.Name = dataCustomer.Name;
                model.Age = dataCustomer.Age;
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var model = new CustomerVM();
            Customer dataCustomer = session.FindObject<Customer>(new BinaryOperator("Oid", id));
            //Customer dataCustomer = session.FindObject<Customer>(new BinaryOperator("Oid", id, BinaryOperatorType.Greater));
            if (dataCustomer != null)
            {
                model.OID = dataCustomer.Oid;
                model.Name = dataCustomer.Name;
                model.Age = dataCustomer.Age;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(CustomerVM model)
        {
            //kalau pakai ini softdelete akan tetap di temukan
            Customer customer = session.GetObjectByKey<Customer>(model.OID);
            customer.Name = model.Name;
            customer.Age = model.Age;
            customer.Save();
            return RedirectToAction("Index", "Customer");
        }
        public ActionResult Create()
        {
            var model = new CustomerVM();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(CustomerVM model)
        {
            Customer customer = new Customer(session);
            customer.Name = model.Name;
            customer.Age = model.Age;
            customer.Save();
            return RedirectToAction("Index","Customer");
        }
        public ActionResult Delete(int id)
        {
            Customer customer = session.GetObjectByKey<Customer>(id);
            customer.Delete();
            return RedirectToAction("Index", "Customer");
        }
    }
}