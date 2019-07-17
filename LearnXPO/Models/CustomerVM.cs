using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnXPO.Models
{
    public class CustomerVM
    {
        public int OID { set; get; }
        public String Name { set; get; }
        public ushort Age { set; get; }
    }
    public class ListCustomerVM
    {
        public List<CustomerVM> customer;
        public ListCustomerVM()
        {
            customer = new List<CustomerVM>();
        }
    }
}