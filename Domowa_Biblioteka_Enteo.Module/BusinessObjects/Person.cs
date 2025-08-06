using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;

namespace Domowa_Biblioteka_Enteo.Module.BusinessObjects
{
    [DefaultClassOptions]
 public class Person : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Person(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        string _firstName;
        [RuleRequiredField]
        [Size(10)]
        public string FirstName 
        {
            get => _firstName;
            set => SetPropertyValue(nameof(FirstName), ref _firstName, value);
        }

        string _lastName;

        [RuleRequiredField]
        [Size(15)]
        public string LastName 
        { 
            get => _lastName;
            set => SetPropertyValue(nameof(LastName), ref _lastName, value);
        }

        [Association("Person-Loans")]
        public XPCollection<Loan> Loans => GetCollection<Loan>(nameof(Loans));
    }
}