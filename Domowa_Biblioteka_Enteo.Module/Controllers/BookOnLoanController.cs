using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Domowa_Biblioteka_Enteo.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using LibraryModule.BusinessObjects;


namespace Domowa_Biblioteka_Enteo.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class BookOnLoanController : ObjectViewController<ListView, Book>
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public BookOnLoanController()
        {
            InitializeComponent();
            SimpleAction loanAction = new SimpleAction(this, "LoanBook", DevExpress.Persistent.Base.PredefinedCategory.Edit)
            {
                Caption = "Loan book",
                ImageName = "Actions_Book",
                SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            };
            loanAction.Execute += LoanAction_Execute;
        }

        private void LoanAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var book = View.CurrentObject as Book;
            if (book == null || View.ObjectSpace == null) return;
            if (book.Status == Status.OnLoan)
                throw new UserFriendlyException("Ta książka jest już wypożyczona.");

            var newObjectSpace = Application.CreateObjectSpace(typeof(Person));
            var personView = Application.CreateListView(newObjectSpace, typeof(Person), false);

            var showViewParameters = new ShowViewParameters(personView)
            {
                TargetWindow = TargetWindow.NewModalWindow
            };
            var dialogController = new DialogController();
            dialogController.Accepting += (s, args) => {
                var selectedPerson = dialogController.Window.View.CurrentObject as Person;
                if (selectedPerson != null)
                {
                    var loan = View.ObjectSpace.CreateObject<Loan>();
                    loan.Book = book;
                    loan.LoanDate = DateTime.Now;
                    loan.Borrower = View.ObjectSpace.GetObject(selectedPerson);
                    book.Status = Status.OnLoan;
                    View.ObjectSpace.CommitChanges();
                    View.ObjectSpace.Refresh();
                }
            };
            showViewParameters.Controllers.Add(dialogController);
            Application.ShowViewStrategy.ShowView(showViewParameters, new ShowViewSource(null, null));
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
