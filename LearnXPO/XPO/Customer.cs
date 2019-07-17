using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevExpress.Xpo;
namespace LearnXPO.XPO
{
    public partial class Customer : XPObject
    {
        public Customer(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        ushort fAge;
        public ushort Age
        {
            get { return fAge; }
            set { SetPropertyValue<ushort>("Age", ref fAge, value); }
        }
    }
}