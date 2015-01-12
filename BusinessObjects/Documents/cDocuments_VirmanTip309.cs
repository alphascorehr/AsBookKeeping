using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;

namespace BusinessObjects.Documents
{
	[Serializable]
	public partial class cDocuments_VirmanTip309: cDocuments_Document<cDocuments_VirmanTip309>
    {
        #region Business Method

        private static readonly PropertyInfo<System.String> S309IBANRNPRIMProperty = RegisterProperty<System.String>(p => p.S309IBANRNPRIM, string.Empty, (System.String)null);
        public System.String S309IBANRNPRIM
		{
            get { return GetProperty(S309IBANRNPRIMProperty); }
            set { SetProperty(S309IBANRNPRIMProperty, (value ?? "").Trim()); }
		}

        private static readonly PropertyInfo<System.String> S309NAZIVPRIMProperty = RegisterProperty<System.String>(p => p.S309NAZIVPRIM, string.Empty, (System.String)null);
        public System.String S309NAZIVPRIM
        {
            get { return GetProperty(S309NAZIVPRIMProperty); }
            set { SetProperty(S309NAZIVPRIMProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> S309ADRPRIMProperty = RegisterProperty<System.String>(p => p.S309ADRPRIM, string.Empty, (System.String)null);
        public System.String S309ADRPRIM
        {
            get { return GetProperty(S309ADRPRIMProperty); }
            set { SetProperty(S309ADRPRIMProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> S309SJEDPRIMProperty = RegisterProperty<System.String>(p => p.S309SJEDPRIM, string.Empty, (System.String)null);
        public System.String S309SJEDPRIM
        {
            get { return GetProperty(S309SJEDPRIMProperty); }
            set { SetProperty(S309SJEDPRIMProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Int32?> S309SFZEMPRIMProperty = RegisterProperty<System.Int32?>(p => p.S309SFZEMPRIM, string.Empty);
        public System.Int32? S309SFZEMPRIM
        {
            get { return GetProperty(S309SFZEMPRIMProperty); }
            set { SetProperty(S309SFZEMPRIMProperty, value); }
        }

        private static readonly PropertyInfo<System.String> S309BRMODPLATProperty = RegisterProperty<System.String>(p => p.S309BRMODPLAT, string.Empty, (System.String)null);
        public System.String S309BRMODPLAT
        {
            get { return GetProperty(S309BRMODPLATProperty); }
            set { SetProperty(S309BRMODPLATProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> S309PNBPLATProperty = RegisterProperty<System.String>(p => p.S309PNBPLAT, string.Empty, (System.String)null);
        public System.String S309PNBPLAT
        {
            get { return GetProperty(S309PNBPLATProperty); }
            set { SetProperty(S309PNBPLATProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Int32?> S309SIFNAMProperty = RegisterProperty<System.Int32?>(p => p.S309SIFNAM, string.Empty);
        public System.Int32? S309SIFNAM
        {
            get { return GetProperty(S309SIFNAMProperty); }
            set { SetProperty(S309SIFNAMProperty, value); }
        }

        private static readonly PropertyInfo<System.String> S309OPISPLProperty = RegisterProperty<System.String>(p => p.S309OPISPL, string.Empty, (System.String)null);
        public System.String S309OPISPL
        {
            get { return GetProperty(S309OPISPLProperty); }
            set { SetProperty(S309OPISPLProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Decimal?> S309IZNProperty = RegisterProperty<System.Decimal?>(p => p.S309IZN, string.Empty, (System.Decimal?)null);
        public System.Decimal? S309IZN
        {
            get { return GetProperty(S309IZNProperty); }
            set { SetProperty(S309IZNProperty, value); }
        }

        private static readonly PropertyInfo<System.String> S309BRMODPRIMProperty = RegisterProperty<System.String>(p => p.S309BRMODPRIM , string.Empty, (System.String)null);
        public System.String S309BRMODPRIM
        {
            get { return GetProperty(S309BRMODPRIMProperty); }
            set { SetProperty(S309BRMODPRIMProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> S309PNBPRIMProperty = RegisterProperty<System.String>(p => p.S309PNBPRIM , string.Empty, (System.String)null);
        public System.String S309PNBPRIM 
        {
            get { return GetProperty(S309PNBPRIMProperty); }
            set { SetProperty(S309PNBPRIMProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> S309BICBANPRIMProperty = RegisterProperty<System.String>(p => p.S309BICBANPRIM , string.Empty, (System.String)null);
        public System.String S309BICBANPRIM
        {
            get { return GetProperty(S309BICBANPRIMProperty); }
            set { SetProperty(S309BICBANPRIMProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> S309NAZBANPRIMProperty = RegisterProperty<System.String>(p => p.S309NAZBANPRIM , string.Empty, (System.String)null);
        public System.String S309NAZBANPRIM
        {
            get { return GetProperty(S309NAZBANPRIMProperty); }
            set { SetProperty(S309NAZBANPRIMProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> S309ADRBNPRIMProperty = RegisterProperty<System.String>(p => p.S309ADRBNPRIM , string.Empty, (System.String)null);
        public System.String S309ADRBNPRIM
        {
            get { return GetProperty(S309ADRBNPRIMProperty); }
            set { SetProperty(S309ADRBNPRIMProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> S309SJEDBNPRIMProperty = RegisterProperty<System.String>(p => p.S309SJEDBNPRIM , string.Empty, (System.String)null);
        public System.String S309SJEDBNPRIM
        {
            get { return GetProperty(S309SJEDBNPRIMProperty); }
            set { SetProperty(S309SJEDBNPRIMProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Int32?> S309SFZEMBNPRIMProperty = RegisterProperty<System.Int32?>(p => p.S309SFZEMBNPRIM, string.Empty);
        public System.Int32? S309SFZEMBNPRIM
        {
            get { return GetProperty(S309SFZEMBNPRIMProperty); }
            set { SetProperty(S309SFZEMBNPRIMProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> S309VRSTAPRIMProperty = RegisterProperty<System.Int32?>(p => p.S309VRSTAPRIM, string.Empty);
        public System.Int32? S309VRSTAPRIM
        {
            get { return GetProperty(S309VRSTAPRIMProperty); }
            set { SetProperty(S309VRSTAPRIMProperty, value); }
        }

        private static readonly PropertyInfo<System.String> S309VALPOKRProperty = RegisterProperty<System.String>(p => p.S309VALPOKR, string.Empty, (System.String)null);
        public System.String S309VALPOKR
        {
            get { return GetProperty(S309VALPOKRProperty); }
            set { SetProperty(S309VALPOKRProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Int32?> S309TROSOPProperty = RegisterProperty<System.Int32?>(p => p.S309TROSOP, string.Empty);
        public System.Int32? S309TROSOP
        {
            get { return GetProperty(S309TROSOPProperty); }
            set { SetProperty(S309TROSOPProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> S309OZNHITNProperty = RegisterProperty<System.Int32?>(p => p.S309OZNHITN, string.Empty);
        public System.Int32? S309OZNHITN
        {
            get { return GetProperty(S309OZNHITNProperty); }
            set { SetProperty(S309OZNHITNProperty, value); }
        }

        private static readonly PropertyInfo<System.String> S309REZERVAProperty = RegisterProperty<System.String>(p => p.S309REZERVA, string.Empty, (System.String)null);
        public System.String S309REZERVA
        {
            get { return GetProperty(S309REZERVAProperty); }
            set { SetProperty(S309REZERVAProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Int32?> S309TIPSLOGProperty = RegisterProperty<System.Int32?>(p => p.S309TIPSLOG, string.Empty);
        public System.Int32? S309TIPSLOG
        {
            get { return GetProperty(S309TIPSLOGProperty); }
            set { SetProperty(S309TIPSLOGProperty, value); }
        }

        private static readonly PropertyInfo<bool?> AutorizedForPaymentProperty = RegisterProperty<bool?>(p => p.AutorizedForPayment, string.Empty, (bool?)null);
        public bool? AutorizedForPayment
        {
            get { return GetProperty(AutorizedForPaymentProperty); }
            set { SetProperty(AutorizedForPaymentProperty, value); }
        }

        private static readonly PropertyInfo<Int32?> UserWhoAuthorizedItIdProperty = RegisterProperty<Int32?>(p => p.UserWhoAuthorizedItId, string.Empty);
        public Int32? UserWhoAuthorizedItId
        {
            get { return GetProperty(UserWhoAuthorizedItIdProperty); }
            set { SetProperty(UserWhoAuthorizedItIdProperty, value); }
        }

        private static readonly PropertyInfo<System.DateTime?> DateOfAuthorizationProperty = RegisterProperty<System.DateTime?>(p => p.DateOfAuthorization, string.Empty, (System.DateTime?)null);
        public System.DateTime? DateOfAuthorization
		{
            get { return GetProperty(DateOfAuthorizationProperty); }
            set { SetProperty(DateOfAuthorizationProperty, value); }
		}

        private static readonly PropertyInfo<bool?> SentProperty = RegisterProperty<bool?>(p => p.Sent, string.Empty, (bool?)null);
        public bool? Sent
        {
            get { return GetProperty(SentProperty); }
            set { SetProperty(SentProperty, value); }
        }

        private static readonly PropertyInfo<Int32?> UserWhoSentItIdProperty = RegisterProperty<Int32?>(p => p.UserWhoSentItId, string.Empty);
        public Int32? UserWhoSentItId
        {
            get { return GetProperty(UserWhoSentItIdProperty); }
            set { SetProperty(UserWhoSentItIdProperty, value); }
        }

        private static readonly PropertyInfo<System.DateTime?> SentDateProperty = RegisterProperty<System.DateTime?>(p => p.SentDate, string.Empty, (System.DateTime?)null);
        public System.DateTime? SentDate
        {
            get { return GetProperty(SentDateProperty); }
            set { SetProperty(SentDateProperty, value); }
        }

        private static readonly PropertyInfo<Int32?> IncomingInvoiceIdProperty = RegisterProperty<Int32?>(p => p.IncomingInvoiceId, string.Empty);
        public Int32? IncomingInvoiceId
        {
            get { return GetProperty(IncomingInvoiceIdProperty); }
            set { SetProperty(IncomingInvoiceIdProperty, value); }
        }

        private static readonly PropertyInfo<System.DateTime?> PaymentDateProperty = RegisterProperty<System.DateTime?>(p => p.PaymentDate, string.Empty, (System.DateTime?)null);
        public System.DateTime? PaymentDate
        {
            get { return GetProperty(PaymentDateProperty); }
            set { SetProperty(PaymentDateProperty, value); }
        }

        private static readonly PropertyInfo<Int32?> CompanyIdProperty = RegisterProperty<Int32?>(p => p.CompanyId, string.Empty);
        public Int32? CompanyId
        {
            get { return GetProperty(CompanyIdProperty); }
            set { SetProperty(CompanyIdProperty, value); }
        }
        private static readonly PropertyInfo<System.String> CompanyAccountProperty = RegisterProperty<System.String>(p => p.CompanyAccount, string.Empty, (System.String)null);
        public System.String CompanyAccount
        {
            get { return GetProperty(CompanyAccountProperty); }
            set { SetProperty(CompanyAccountProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> CurrencyProperty = RegisterProperty<System.String>(p => p.Currency, string.Empty, (System.String)null);
        public System.String Currency
        {
            get { return GetProperty(CurrencyProperty); }
            set { SetProperty(CurrencyProperty, (value ?? "").Trim()); }
        }
        private static readonly PropertyInfo<System.Int16?> statusProperty = RegisterProperty<System.Int16?>(p => p.Status, string.Empty, (System.Int16?)null);
        public System.Int16? Status
        {
            get { return GetProperty(statusProperty); }
            set { SetProperty(statusProperty, value); }
        }

        #endregion

        #region Factory Methods

        public static cDocuments_VirmanTip309 NewDocuments_VirmanTip309()
        {
            return DataPortal.Create<cDocuments_VirmanTip309>();

        }

        public static cDocuments_VirmanTip309 GetDocuments_VirmanTip309(int uniqueId)
        {
            return DataPortal.Fetch<cDocuments_VirmanTip309>(new SingleCriteria<cDocuments_VirmanTip309, int>(uniqueId));
        }

        internal static cDocuments_VirmanTip309 GetDocuments_VirmanTip309(cDocuments_VirmanTip309 data)
        {
            return DataPortal.Fetch<cDocuments_VirmanTip309>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cDocuments_VirmanTip309, int> criteria)
        {

            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Document.OfType<Documents_VirmanTip309>().First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

                LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LoadProperty<Guid>(GUIDProperty, data.GUID);
                LoadProperty<short>(documentTypeProperty, data.DocumentType);
                LoadProperty<string>(uniqueIdentifierProperty, data.UniqueIdentifier);
                LoadProperty<string>(barcodeProperty, data.Barcode);
                LoadProperty<int>(ordinalNumberProperty, data.OrdinalNumber);
                LoadProperty<int>(ordinalNumberInYearProperty, data.OrdinalNumberInYear);
                LoadProperty<DateTime>(documentDateTimeProperty, data.DocumentDateTime);
                LoadProperty<DateTime>(creationDateTimeProperty, data.CreationDateTime);
                LoadProperty<int>(mDSubjects_Employee_AuthorIdProperty, data.MDSubjects_Employee_AuthorId);
                LoadProperty<string>(descriptionProperty, data.Description);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty, data.MDSubjects_EmployeeWhoChengedId);
                LoadProperty<short?>(statusProperty, data.Status);

                LoadProperty<string>(S309IBANRNPRIMProperty, data.S309IBANRNPRIM);
                LoadProperty<string>(S309NAZIVPRIMProperty, data.S309NAZIVPRIM);
                LoadProperty<string>(S309ADRPRIMProperty, data.S309ADRPRIM);
                LoadProperty<string>(S309SJEDPRIMProperty, data.S309SJEDPRIM);
                LoadProperty<int?>(S309SFZEMPRIMProperty, data.S309SFZEMPRIM);
                LoadProperty<string>(S309BRMODPLATProperty, data.S309BRMODPLAT);
                LoadProperty<string>(S309PNBPLATProperty, data.S309PNBPLAT);
                LoadProperty<int?>(S309SIFNAMProperty, data.S309SIFNAM);
                LoadProperty<string>(S309OPISPLProperty, data.S309OPISPL);
                LoadProperty<decimal?>(S309IZNProperty, data.S309IZN);
                LoadProperty<string>(S309BRMODPRIMProperty, data.S309BRMODPRIM);
                LoadProperty<string>(S309PNBPRIMProperty, data.S309PNBPRIM);
                LoadProperty<string>(S309BICBANPRIMProperty, data.S309BICBANPRIM);
                LoadProperty<string>(S309NAZBANPRIMProperty, data.S309NAZBANPRIM);
                LoadProperty<string>(S309ADRBNPRIMProperty, data.S309ADRBNPRIM);
                LoadProperty<string>(S309SJEDBNPRIMProperty, data.S309SJEDBNPRIM);
                LoadProperty<int?>(S309SFZEMBNPRIMProperty, data.S309SFZEMBNPRIM);
                LoadProperty<int?>(S309VRSTAPRIMProperty, data.S309VRSTAPRIM);
                LoadProperty<string>(S309VALPOKRProperty, data.S309VALPOKR);
                LoadProperty<int?>(S309TROSOPProperty, data.S309TROSOP);
                LoadProperty<int?>(S309OZNHITNProperty, data.S309OZNHITN);
                LoadProperty<string>(S309REZERVAProperty, data.S309REZERVA);
                LoadProperty<int?>(S309TIPSLOGProperty, data.S309TIPSLOG);
                LoadProperty<bool?>(AutorizedForPaymentProperty,data.AutorizedForPayment);
                LoadProperty<int?>(UserWhoAuthorizedItIdProperty, data.UserWhoAuthorizedItId);
                LoadProperty<DateTime?>(DateOfAuthorizationProperty, data.DateOfAuthorization);
                LoadProperty<bool?>(SentProperty, data.Sent);
                LoadProperty<int?>(UserWhoSentItIdProperty, data.UserWhoSentItId);
                LoadProperty<DateTime?>(SentDateProperty, data.SentDate);
                LoadProperty<int?>(IncomingInvoiceIdProperty, data.IncomingInvoiceId);

                LoadProperty<DateTime?>(PaymentDateProperty, data.PaymentDate);
                LoadProperty<int?>(CompanyIdProperty, data.CompanyId);
                LoadProperty<string>(CompanyAccountProperty, data.CompanyAccount);
                LoadProperty<string>(CurrencyProperty, data.Currency);
                
                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_VirmanTip309();

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.DocumentType = ReadProperty<short>(documentTypeProperty);
                data.UniqueIdentifier = ReadProperty<string>(uniqueIdentifierProperty);
                data.Barcode = ReadProperty<string>(barcodeProperty);
                data.OrdinalNumber = ReadProperty<int>(ordinalNumberProperty);
                data.OrdinalNumberInYear = ReadProperty<int>(ordinalNumberInYearProperty);
                data.DocumentDateTime = ReadProperty<DateTime>(documentDateTimeProperty);
                data.CreationDateTime = ReadProperty<DateTime>(creationDateTimeProperty);
                data.MDSubjects_Employee_AuthorId = ReadProperty<int>(mDSubjects_Employee_AuthorIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.Status = ReadProperty<short?>(statusProperty);


                data.S309IBANRNPRIM = ReadProperty<string>(S309IBANRNPRIMProperty);
                data.S309NAZIVPRIM = ReadProperty<string>(S309NAZIVPRIMProperty);
                data.S309ADRPRIM = ReadProperty<string>(S309ADRPRIMProperty);
                data.S309SJEDPRIM = ReadProperty<string>(S309SJEDPRIMProperty);
                data.S309SFZEMPRIM = ReadProperty<int?>(S309SFZEMPRIMProperty);
                data.S309BRMODPLAT = ReadProperty<string>(S309BRMODPLATProperty);
                data.S309PNBPLAT = ReadProperty<string>(S309PNBPLATProperty);
                data.S309SIFNAM = ReadProperty<int?>(S309SIFNAMProperty);
                data.S309OPISPL = ReadProperty<string>(S309OPISPLProperty);
                data.S309IZN = ReadProperty<decimal?>(S309IZNProperty);
                data.S309BRMODPRIM = ReadProperty<string>(S309BRMODPRIMProperty);
                data.S309PNBPRIM = ReadProperty<string>(S309PNBPRIMProperty);
                data.S309BICBANPRIM = ReadProperty<string>(S309BICBANPRIMProperty);
                data.S309NAZBANPRIM = ReadProperty<string>(S309NAZBANPRIMProperty);
                data.S309ADRBNPRIM = ReadProperty<string>(S309ADRBNPRIMProperty);
                data.S309SJEDBNPRIM = ReadProperty<string>(S309SJEDBNPRIMProperty);
                data.S309SFZEMBNPRIM = ReadProperty<int?>(S309SFZEMBNPRIMProperty);
                data.S309VRSTAPRIM = ReadProperty<int?>(S309VRSTAPRIMProperty);
                data.S309VALPOKR = ReadProperty<string>(S309VALPOKRProperty);
                data.S309TROSOP = ReadProperty<int?>(S309TROSOPProperty);
                data.S309OZNHITN = ReadProperty<int?>(S309OZNHITNProperty);
                data.S309REZERVA = ReadProperty<string>(S309REZERVAProperty);
                data.S309TIPSLOG = ReadProperty<int?>(S309TIPSLOGProperty);
                data.AutorizedForPayment = ReadProperty<bool?>(AutorizedForPaymentProperty);
                data.UserWhoAuthorizedItId = ReadProperty<int?>(UserWhoAuthorizedItIdProperty);
                data.DateOfAuthorization = ReadProperty<DateTime?>(DateOfAuthorizationProperty);
                data.Sent= ReadProperty<bool?>(SentProperty);
                data.UserWhoSentItId = ReadProperty<int?>(UserWhoSentItIdProperty);
                data.SentDate = ReadProperty<DateTime?>(SentDateProperty);
                data.IncomingInvoiceId= ReadProperty<int?>(IncomingInvoiceIdProperty);

                data.PaymentDate = ReadProperty<DateTime?>(PaymentDateProperty);
                data.CompanyId = ReadProperty<int?>(CompanyIdProperty);
                data.CompanyAccount = ReadProperty<string>(CompanyAccountProperty);
                data.Currency = ReadProperty<string>(CurrencyProperty);

                ctx.ObjectContext.AddToDocuments_Document(data);

                ctx.ObjectContext.SaveChanges();


                //Get New id
                int newId = data.Id;
                //Load New Id into object
                LoadProperty(IdProperty, newId);
                //Load New EntityKey into Object
                LoadProperty(EntityKeyDataProperty, Serialize(data.EntityKey));

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_VirmanTip309();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.DocumentType = ReadProperty<short>(documentTypeProperty);
                data.UniqueIdentifier = ReadProperty<string>(uniqueIdentifierProperty);
                data.Barcode = ReadProperty<string>(barcodeProperty);
                data.OrdinalNumber = ReadProperty<int>(ordinalNumberProperty);
                data.OrdinalNumberInYear = ReadProperty<int>(ordinalNumberInYearProperty);
                data.DocumentDateTime = ReadProperty<DateTime>(documentDateTimeProperty);
                data.CreationDateTime = ReadProperty<DateTime>(creationDateTimeProperty);
                data.MDSubjects_Employee_AuthorId = ReadProperty<int>(mDSubjects_Employee_AuthorIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.Status = ReadProperty<short?>(statusProperty);

                data.S309IBANRNPRIM = ReadProperty<string>(S309IBANRNPRIMProperty);
                data.S309NAZIVPRIM = ReadProperty<string>(S309NAZIVPRIMProperty);
                data.S309ADRPRIM = ReadProperty<string>(S309ADRPRIMProperty);
                data.S309SJEDPRIM = ReadProperty<string>(S309SJEDPRIMProperty);
                data.S309SFZEMPRIM = ReadProperty<int?>(S309SFZEMPRIMProperty);
                data.S309BRMODPLAT = ReadProperty<string>(S309BRMODPLATProperty);
                data.S309PNBPLAT = ReadProperty<string>(S309PNBPLATProperty);
                data.S309SIFNAM = ReadProperty<int?>(S309SIFNAMProperty);
                data.S309OPISPL = ReadProperty<string>(S309OPISPLProperty);
                data.S309IZN = ReadProperty<decimal?>(S309IZNProperty);
                data.S309BRMODPRIM = ReadProperty<string>(S309BRMODPRIMProperty);
                data.S309PNBPRIM = ReadProperty<string>(S309PNBPRIMProperty);
                data.S309BICBANPRIM = ReadProperty<string>(S309BICBANPRIMProperty);
                data.S309NAZBANPRIM = ReadProperty<string>(S309NAZBANPRIMProperty);
                data.S309ADRBNPRIM = ReadProperty<string>(S309ADRBNPRIMProperty);
                data.S309SJEDBNPRIM = ReadProperty<string>(S309SJEDBNPRIMProperty);
                data.S309SFZEMBNPRIM = ReadProperty<int?>(S309SFZEMBNPRIMProperty);
                data.S309VRSTAPRIM = ReadProperty<int?>(S309VRSTAPRIMProperty);
                data.S309VALPOKR = ReadProperty<string>(S309VALPOKRProperty);
                data.S309TROSOP = ReadProperty<int?>(S309TROSOPProperty);
                data.S309OZNHITN = ReadProperty<int?>(S309OZNHITNProperty);
                data.S309REZERVA = ReadProperty<string>(S309REZERVAProperty);
                data.S309TIPSLOG = ReadProperty<int?>(S309TIPSLOGProperty);
                data.AutorizedForPayment = ReadProperty<bool?>(AutorizedForPaymentProperty);
                data.UserWhoAuthorizedItId = ReadProperty<int?>(UserWhoAuthorizedItIdProperty);
                data.DateOfAuthorization = ReadProperty<DateTime?>(DateOfAuthorizationProperty);
                data.Sent = ReadProperty<bool?>(SentProperty);
                data.UserWhoSentItId = ReadProperty<int?>(UserWhoSentItIdProperty);
                data.SentDate = ReadProperty<DateTime?>(SentDateProperty);

                data.IncomingInvoiceId = ReadProperty<int?>(IncomingInvoiceIdProperty);

                data.PaymentDate = ReadProperty<DateTime?>(PaymentDateProperty);
                data.CompanyId = ReadProperty<int?>(CompanyIdProperty);
                data.CompanyAccount = ReadProperty<string>(CompanyAccountProperty);
                data.Currency = ReadProperty<string>(CurrencyProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }
}
