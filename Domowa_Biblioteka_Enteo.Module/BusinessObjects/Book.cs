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
   public class Book : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Book(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Status = Status.InStock;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        string _title;

        [RuleRequiredField]
        [Size(20)]
        public string Title 
        { get => _title;
            set => SetPropertyValue(nameof(Title), ref _title, value); 
        }

        string _isbn;
        [RuleRegularExpression(@"\d{3}-\d{10}", CustomMessageTemplate = "ISBN must be in format 000-0000000000")]
        [Size(50)]
        public string ISBN 
        { 
            get => _isbn;
            set => SetPropertyValue(nameof(ISBN), ref _isbn, value);
        }

        DateTime _publishDate;
        public DateTime PublishDate 
        {
            get => _publishDate;
            set => SetPropertyValue(nameof(PublishDate), ref _publishDate, value);
        }

        Status _status;

        public Status Status
        {
            get => _status;
            set => SetPropertyValue(nameof(Status), ref _status, value);
        }

        [Association("Book-Authors")]
        public XPCollection<Author> Authors => GetCollection<Author>(nameof(Authors));

        [Association("Book-Loans")]
        public XPCollection<Loan> Loans => GetCollection<Loan>(nameof(Loans));
    }

    public enum Status
    {
        Unknown = 0,
        Returned = 1,
        OnLoan = 2,
        InStock = 3,
    }
}