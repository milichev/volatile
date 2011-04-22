using System;
using System.Data.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LeDoyen.AtContract.Business.Entity;

namespace LeDoyen.AtContract.Client
{
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ComposeDocument : Window
    {
        private readonly LeDoyenEntities _context = new LeDoyenEntities();
        private Agreement _agreement;

        public ComposeDocument()
        {
            InitializeComponent();
            RebindContractors();
            RebindProjects();
            RebindPaymentTypes();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }

        private void RebindContractors()
        {
            cmbContractor.ItemsSource = Context.Contractors;
        }

        private void RebindProjects()
        {
            cmbProject.ItemsSource = Context.Projects;
        }

        private void RebindPaymentTypes()
        {
            cmbPaymentTypes.ItemsSource = Context.PaymentTypes;
        }

        private void SaveAgreement()
        {
            Context.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
        }

        private void cmbProcess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveAgreement();
        }

        private void btnDoc_Click(object sender, RoutedEventArgs e)
        {
            SaveAgreement();
        }

        public LeDoyenEntities Context
        {
            get { return _context; }
        }

        public Agreement CurrentAgreement
        {
            get
            {
                return _agreement ?? (_agreement = (from agreement in Context.Agreements
                                                    where agreement.ID == 1
                                                    select agreement).First());
            }
        }
    }
}