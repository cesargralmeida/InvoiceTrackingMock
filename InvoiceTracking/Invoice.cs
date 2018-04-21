using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTracking
{
    public class InvoiceDatabase
    {
        //Codes for actions
        private static int INVALID = -1;
        private static int ADD_INVOICE = 1;
        private static int UPDATE_INVOICE = 2;
        private static int DO_NOTHING = 3;

        public Dictionary<string, Invoice> InvoiceDB;
        /// <summary>
        /// Instantiates a new Invoice Class
        /// </summary>
        public InvoiceDatabase()
        {
            InvoiceDB = new Dictionary<string, Invoice>();
        }
        /// <summary>
        /// Read invoices and updates the database.
        /// </summary>
        /// <param name="invs">Invoice list</param>
        public void ReadInvoices(List<Invoice> invs)
        {
            CheckActionForInvoice(invs);
            return;
        }
        /// <summary>
        /// Do action for each invoice list
        /// </summary>
        /// <param name="inv">Invoice list</param>
        private void CheckActionForInvoice(List<Invoice> inv)
        {
            foreach(var invoice in inv)
            {
                int ActionToBeTaken = CheckActionForInvoice(invoice);

                switch (ActionToBeTaken)
                {
                    case 1:
                        AddInvoice(invoice);
                        break;
                    case 2:
                        UpdateInvoice(invoice);
                        break;
                    default:
                        break;
                }

            }
        }
        /// <summary>
        /// Return the action code to be taken for the invoice
        /// </summary>
        /// <param name="inv">Invoice</param>
        /// <returns>Action code</returns>
        int CheckActionForInvoice(Invoice inv)
        {
            if (IsValidInvoice(inv))
            {
                return INVALID;
            }
            if (!InvoiceDB.ContainsKey(inv.InvoiceNumber))
            {
                return ADD_INVOICE;
            }
            else
            {
                var CurrentInvoice = InvoiceDB[inv.InvoiceNumber];

                if(inv.EstimatedDate != CurrentInvoice.EstimatedDate || inv.NeedDate != CurrentInvoice.NeedDate)
                {
                    return UPDATE_INVOICE;
                }
                else
                {
                    return DO_NOTHING;
                }
            }
        }
        /// <summary>
        /// Checks if an invoice is valid
        /// </summary>
        /// <param name="inv">Invoice</param>
        /// <returns>Boolean</returns>
        private bool IsValidInvoice(Invoice inv)
        {
            return inv.InvoiceNumber != null
                && inv.InvoiceNumber.Count() > 4
                && inv.InvoiceNumber.Count() < 10
                && inv.EstimatedDate != null
                && inv.NeedDate != null;
        }
        /// <summary>
        /// Add invoice to the database
        /// </summary>
        /// <param name="inv">Invoice</param>
        private void AddInvoice(Invoice inv)
        {
            InvoiceDB.Add(inv.InvoiceNumber, inv);
        }
        /// <summary>
        /// Update the invoice in the database
        /// </summary>
        /// <param name="inv">Invoice to be updated</param>
        private void UpdateInvoice(Invoice inv)
        {
            InvoiceDB[inv.InvoiceNumber]=inv;
        }
    }
    /// <summary>
    /// Invoice Class
    /// </summary>
    public class Invoice
    {
        public string InvoiceNumber;
        public DateTime EstimatedDate;
        public DateTime NeedDate;
        /// <summary>
        /// Instantiates a new invoice class
        /// </summary>
        /// <param name="invNum">Invoice</param>
        /// <param name="estDate">Estimated Date</param>
        /// <param name="needDate">Need Date</param>
        public Invoice(string invNum, DateTime estDate, DateTime needDate)
        {
            InvoiceNumber = invNum;
            EstimatedDate = estDate;
            NeedDate = needDate;
        }
    }
}
