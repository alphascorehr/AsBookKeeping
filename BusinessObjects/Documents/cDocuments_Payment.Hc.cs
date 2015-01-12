using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Documents
{
    public partial class cDocuments_Payment
    {
        public void CheckRules()
        {
            BusinessRules.CheckRules();
        }
    }

    [Serializable]
    internal class Documents_Payment_Criteria : Csla.CriteriaBase<Documents_Payment_Criteria>
    {
        private int _documentId;
        private int? _paymentItemId;

        public int DocumentId
        {
            get { return _documentId; }
        }

        public int? PaymentItemId
        {
            get { return _paymentItemId; }
        }

        public Documents_Payment_Criteria(int documentId, int? paymentItemId)
        { _documentId = documentId; _paymentItemId = paymentItemId; }
    }


}
