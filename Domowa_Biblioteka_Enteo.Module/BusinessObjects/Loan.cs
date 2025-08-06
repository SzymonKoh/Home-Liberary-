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
using System.Linq;
using System.Text;

namespace Domowa_Biblioteka_Enteo.Module.BusinessObjects
{
    [DefaultClassOptions]
  public class Loan : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Loan(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        DateTime _loanDate;
        public DateTime LoanDate 
        {
            get => _loanDate;
            set => SetPropertyValue(nameof(LoanDate), ref _loanDate, value);
        }

        Book _book;
        [Association("Book-Loans")]
        public Book Book
        {
            get => _book;
            set => SetPropertyValue(nameof(Book), ref _book, value);
        }

        Person _borrower;
        [Association("Person-Loans")]
        public Person Borrower
        {
            get => _borrower;
            set => SetPropertyValue(nameof(Borrower), ref _borrower, value);
        }
    }
}