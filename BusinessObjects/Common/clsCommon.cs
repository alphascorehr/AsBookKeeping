using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Common
{
    public enum SubjectType { Person = 0, Company = 1, Obrt = 2, SoleProprietor = 3, Employee = 4 };
    public enum DocumentType { IncomingInvoice = 0, Invoice = 1, Offer = 2, Payment = 3, PriceList = 4, Quote = 5, OrderForm = 6, WorkOrder = 7, Dispatch = 8, TravelOrder = 9, Payoff = 10, TakeoverOfGoods = 11, Compensation = 12, TransferOrder = 13, Virman = 14, CashBoxBill = 15, PSPotrazuje = 98, PSDuguje = 99 };
    public enum EntityType { Product = 0, Service = 1 };
    public enum BillingMethodes {HourlyStaffRate = 0, HourlyTaskRate = 1, HourlyProjectRate = 2, FlatProjectAmount = 3 }
    public enum PDVType { R1 = 0, R2 = 1 }
    public enum Status { Otvoren = 0, Poslan = 1, Placen = 2, Odbijen = 3, Zatvoren = 4, Obrisan = 5, Obracunat = 6 }
    public enum FiscalizationTypeOfPayment { Virman = 0, Novcanica = 1, Kartica = 2, Cek = 3, TransakcijskiRacun = 4, Ostalo = 5 }

    public class clsCommon
    {
      
    }

    [Serializable]
    public class ActiveEnums_Criteria : Csla.CriteriaBase<ActiveEnums_Criteria>
    {
        private int? _companyId;
        private int? _includeInactiveId;

        public int? CompanyId
        {
            get { return _companyId; }
        }

        public int? IncludeInactiveId
        {
            get { return _includeInactiveId; }
        }

        public ActiveEnums_Criteria(int? companyId, int? includeInactiveId)
        { _companyId = companyId; _includeInactiveId = includeInactiveId; }
    }
}
