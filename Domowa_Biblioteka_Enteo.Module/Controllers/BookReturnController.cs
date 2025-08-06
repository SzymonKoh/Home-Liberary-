using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Domowa_Biblioteka_Enteo.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domowa_Biblioteka_Enteo.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class BookReturnController : ObjectViewController<ListView, Book>
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public BookReturnController()
        {
            InitializeComponent();
            SimpleAction returnAction = new SimpleAction(this, "ReturnBook", DevExpress.Persistent.Base.PredefinedCategory.Edit)
            {
                Caption = "Return book",
                ImageName = "Actions_Refresh",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects
            };
            returnAction.Execute += ReturnAction_Execute;
        }

        private void ReturnAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (e.SelectedObjects == null || e.SelectedObjects.Count == 0) return;
            var os = View.ObjectSpace;
            int returnedCount = 0;
            foreach (Book book in e.SelectedObjects)
            {
                if (book.Status != Status.OnLoan) continue;

                book.Status = Status.Returned;
                returnedCount++;
            }
            os.CommitChanges();
            View.ObjectSpace.Refresh();
            if (returnedCount > 0)
            {
                if(returnedCount == 1) Application.ShowViewStrategy.ShowMessage(string.Format("Zwrócono {0} książke.", returnedCount), 
                                                                                InformationType.Success, 3000, InformationPosition.Bottom);
                else Application.ShowViewStrategy.ShowMessage(string.Format("Zwrócono {0} książek.", returnedCount),
                                                              InformationType.Success, 3000, InformationPosition.Bottom);
            }
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