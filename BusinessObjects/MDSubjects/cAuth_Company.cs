using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;
using System.Net.Mail;
using BusinessObjects.Security;


namespace BusinessObjects.MDSubjects
{
	[Serializable]
	public partial class cAuth_Company: CoreBusinessClass<cAuth_Company>
	{
		#region Business Methods
		public static readonly PropertyInfo< System.Int32 > IdProperty = RegisterProperty< System.Int32 >(p => p.Id, string.Empty);
#if !SILVERLIGHT
		[System.ComponentModel.DataObjectField(true, true)]
#endif
		public System.Int32 Id
		{
			get { return GetProperty(IdProperty); }
			internal set { SetProperty(IdProperty, value); }
		}

		private static readonly PropertyInfo< System.String > nameProperty = RegisterProperty<System.String>(p => p.Name, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Name
		{
			get { return GetProperty(nameProperty); }
			set { SetProperty(nameProperty, value.Trim()); }
		}

		private static readonly PropertyInfo< System.String > oIBProperty = RegisterProperty<System.String>(p => p.OIB, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(11, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String OIB
		{
			get { return GetProperty(oIBProperty); }
			set { SetProperty(oIBProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo<System.String> accountProperty = RegisterProperty<System.String>(p => p.Account, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(40, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Account
		{
			get { return GetProperty(accountProperty); }
			set { SetProperty(accountProperty, value.Trim()); }
		}

		private static readonly PropertyInfo<System.String> account1Property = RegisterProperty<System.String>(p => p.Account1, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(40, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		public System.String Account1
		{
			get { return GetProperty(account1Property); }
			set { SetProperty(account1Property, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo<System.String> account2Property = RegisterProperty<System.String>(p => p.Account2, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(40, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		public System.String Account2
		{
			get { return GetProperty(account2Property); }
			set { SetProperty(account2Property, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo<System.String> account3Property = RegisterProperty<System.String>(p => p.Account3, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(40, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		public System.String Account3
		{
			get { return GetProperty(account3Property); }
			set { SetProperty(account3Property, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo<System.Byte[]> systemLogoProperty = RegisterProperty<System.Byte[]>(p => p.SystemLogo, string.Empty, (System.Byte[])null);
		public System.Byte[] SystemLogo
		{
			get { return GetProperty(systemLogoProperty); }
			set { SetProperty(systemLogoProperty, value); }
		}

		private static readonly PropertyInfo< System.Byte[] > invoiceLogoProperty = RegisterProperty<System.Byte[]>(p => p.InvoiceLogo, string.Empty, (System.Byte[])null);
		public System.Byte[] InvoiceLogo
		{
			get { return GetProperty(invoiceLogoProperty); }
			set { SetProperty(invoiceLogoProperty, value); }
		}

		private static readonly PropertyInfo< System.Byte[] > highQualityLogoProperty = RegisterProperty<System.Byte[]>(p => p.HighQualityLogo, string.Empty, (System.Byte[])null);
		public System.Byte[] HighQualityLogo
		{
			get { return GetProperty(highQualityLogoProperty); }
			set { SetProperty(highQualityLogoProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > timeZoneIdProperty = RegisterProperty<System.Int32?>(p => p.TimeZoneId, string.Empty,(System.Int32?)null);
		public System.Int32? TimeZoneId
		{
			get { return GetProperty(timeZoneIdProperty); }
			set { SetProperty(timeZoneIdProperty, value); }
		}

		private static readonly PropertyInfo< System.String > contactPersonProperty = RegisterProperty<System.String>(p => p.ContactPerson, string.Empty, (System.String)null);
		public System.String ContactPerson
		{
			get { return GetProperty(contactPersonProperty); }
			set { SetProperty(contactPersonProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.String > businessPhoneProperty = RegisterProperty<System.String>(p => p.BusinessPhone, string.Empty, (System.String)null);
		public System.String BusinessPhone
		{
			get { return GetProperty(businessPhoneProperty); }
			set { SetProperty(businessPhoneProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.String > mobileProperty = RegisterProperty<System.String>(p => p.Mobile, string.Empty, (System.String)null);
		public System.String Mobile
		{
			get { return GetProperty(mobileProperty); }
			set { SetProperty(mobileProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.String > faxProperty = RegisterProperty<System.String>(p => p.Fax, string.Empty, (System.String)null);
		public System.String Fax
		{
			get { return GetProperty(faxProperty); }
			set { SetProperty(faxProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.String > emailProperty = RegisterProperty<System.String>(p => p.Email, string.Empty, (System.String)null);
		public System.String Email
		{
			get { return GetProperty(emailProperty); }
			set { SetProperty(emailProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< bool? > staffCanSendInvoiceAndQuotesProperty = RegisterProperty<bool?>(p => p.StaffCanSendInvoiceAndQuotes, string.Empty,(bool?)null);
		public bool? StaffCanSendInvoiceAndQuotes
		{
			get { return GetProperty(staffCanSendInvoiceAndQuotesProperty); }
			set { SetProperty(staffCanSendInvoiceAndQuotesProperty, value); }
		}

		private static readonly PropertyInfo< bool? > staffCanSeeReportsProperty = RegisterProperty<bool?>(p => p.StaffCanSeeReports, string.Empty,(bool?)null);
		public bool? StaffCanSeeReports
		{
			get { return GetProperty(staffCanSeeReportsProperty); }
			set { SetProperty(staffCanSeeReportsProperty, value); }
		}

		private static readonly PropertyInfo< bool? > staffProjectAccessProperty = RegisterProperty<bool?>(p => p.StaffProjectAccess, string.Empty,(bool?)null);
		public bool? StaffProjectAccess
		{
			get { return GetProperty(staffProjectAccessProperty); }
			set { SetProperty(staffProjectAccessProperty, value); }
		}

		private static readonly PropertyInfo< System.String > accountURLProperty = RegisterProperty<System.String>(p => p.AccountURL, string.Empty, (System.String)null);
		public System.String AccountURL
		{
			get { return GetProperty(accountURLProperty); }
			set { SetProperty(accountURLProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.Int32? > mDPlaces_Enums_Geo_CountryIdProperty = RegisterProperty<System.Int32?>(p => p.MDPlaces_Enums_Geo_CountryId, string.Empty,(System.Int32?)null);
		public System.Int32? MDPlaces_Enums_Geo_CountryId
		{
			get { return GetProperty(mDPlaces_Enums_Geo_CountryIdProperty); }
			set { SetProperty(mDPlaces_Enums_Geo_CountryIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDGeneral_Enums_CurrencyIdProperty = RegisterProperty<System.Int32?>(p => p.MDGeneral_Enums_CurrencyId, string.Empty,(System.Int32?)null);
		public System.Int32? MDGeneral_Enums_CurrencyId
		{
			get { return GetProperty(mDGeneral_Enums_CurrencyIdProperty); }
			set { SetProperty(mDGeneral_Enums_CurrencyIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > homeAddress_PlaceIdProperty = RegisterProperty<System.Int32?>(p => p.HomeAddress_PlaceId, string.Empty,(System.Int32?)null);
		public System.Int32? HomeAddress_PlaceId
		{
			get { return GetProperty(homeAddress_PlaceIdProperty); }
			set { SetProperty(homeAddress_PlaceIdProperty, value); }
		}

		private static readonly PropertyInfo< System.String > homeAddress_StateProperty = RegisterProperty<System.String>(p => p.HomeAddress_State, string.Empty, (System.String)null);
		public System.String HomeAddress_State
		{
			get { return GetProperty(homeAddress_StateProperty); }
			set { SetProperty(homeAddress_StateProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.String > homeAddress_StreetProperty = RegisterProperty<System.String>(p => p.HomeAddress_Street, string.Empty, (System.String)null);
		public System.String HomeAddress_Street
		{
			get { return GetProperty(homeAddress_StreetProperty); }
			set { SetProperty(homeAddress_StreetProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.String > homeAddress_NumberProperty = RegisterProperty<System.String>(p => p.HomeAddress_Number, string.Empty, (System.String)null);
		public System.String HomeAddress_Number
		{
			get { return GetProperty(homeAddress_NumberProperty); }
			set { SetProperty(homeAddress_NumberProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.Int32 > numberOfUsersProperty = RegisterProperty<System.Int32>(p => p.NumberOfUsers, string.Empty, 1);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 NumberOfUsers
		{
			get { return GetProperty(numberOfUsersProperty); }
			set { SetProperty(numberOfUsersProperty, value); }
		}

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		private static readonly PropertyInfo< System.DateTime > activationDateProperty = RegisterProperty<System.DateTime>(p => p.ActivationDate, string.Empty,System.DateTime.Now);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.DateTime ActivationDate
		{
			get { return GetProperty(activationDateProperty); }
			set { SetProperty(activationDateProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > numbnerOfDaysProperty = RegisterProperty<System.Int32>(p => p.NumbnerOfDays, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 NumbnerOfDays
		{
			get { return GetProperty(numbnerOfDaysProperty); }
			set { SetProperty(numbnerOfDaysProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > numberOfUsersOrderedProperty = RegisterProperty<System.Int32?>(p => p.NumberOfUsersOrdered, string.Empty,(System.Int32?)null);
		public System.Int32? NumberOfUsersOrdered
		{
			get { return GetProperty(numberOfUsersOrderedProperty); }
			set { SetProperty(numberOfUsersOrderedProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> pDVTypeProperty = RegisterProperty<System.Int32?>(p => p.PDVType, string.Empty, (System.Int32?)null);
		public System.Int32? PDVType
		{
			get { return GetProperty(pDVTypeProperty); }
			set { SetProperty(pDVTypeProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> firstNumberDispatchProperty = RegisterProperty<System.Int32?>(p => p.FirstNumberDispatch, string.Empty, (System.Int32?)null);
		public System.Int32? FirstNumberDispatch
		{
			get { return GetProperty(firstNumberDispatchProperty); }
			set { SetProperty(firstNumberDispatchProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> firstNumberIncomingInvoiceProperty = RegisterProperty<System.Int32?>(p => p.FirstNumberIncomingInvoice, string.Empty, (System.Int32?)null);
		public System.Int32? FirstNumberIncomingInvoice
		{
			get { return GetProperty(firstNumberIncomingInvoiceProperty); }
			set { SetProperty(firstNumberIncomingInvoiceProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> firstNumberInvoiceProperty = RegisterProperty<System.Int32?>(p => p.FirstNumberInvoice, string.Empty, (System.Int32?)null);
		public System.Int32? FirstNumberInvoice
		{
			get { return GetProperty(firstNumberInvoiceProperty); }
			set { SetProperty(firstNumberInvoiceProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> firstNumberOfferProperty = RegisterProperty<System.Int32?>(p => p.FirstNumberOffer, string.Empty, (System.Int32?)null);
		public System.Int32? FirstNumberOffer
		{
			get { return GetProperty(firstNumberOfferProperty); }
			set { SetProperty(firstNumberOfferProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> firstNumberOrderFormProperty = RegisterProperty<System.Int32?>(p => p.FirstNumberOrderForm, string.Empty, (System.Int32?)null);
		public System.Int32? FirstNumberOrderForm
		{
			get { return GetProperty(firstNumberOrderFormProperty); }
			set { SetProperty(firstNumberOrderFormProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> firstNumberPaymentProperty = RegisterProperty<System.Int32?>(p => p.FirstNumberPayment, string.Empty, (System.Int32?)null);
		public System.Int32? FirstNumberPayment
		{
			get { return GetProperty(firstNumberPaymentProperty); }
			set { SetProperty(firstNumberPaymentProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> firstNumberPayoffProperty = RegisterProperty<System.Int32?>(p => p.FirstNumberPayoff, string.Empty, (System.Int32?)null);
		public System.Int32? FirstNumberPayoff
		{
			get { return GetProperty(firstNumberPayoffProperty); }
			set { SetProperty(firstNumberPayoffProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> firstNumberPriceListProperty = RegisterProperty<System.Int32?>(p => p.FirstNumberPriceList, string.Empty, (System.Int32?)null);
		public System.Int32? FirstNumberPriceList
		{
			get { return GetProperty(firstNumberPriceListProperty); }
			set { SetProperty(firstNumberPriceListProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> firstNumberQuoteProperty = RegisterProperty<System.Int32?>(p => p.FirstNumberQuote, string.Empty, (System.Int32?)null);
		public System.Int32? FirstNumberQuote
		{
			get { return GetProperty(firstNumberQuoteProperty); }
			set { SetProperty(firstNumberQuoteProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> firstNumberTakeoverOfGoodsProperty = RegisterProperty<System.Int32?>(p => p.FirstNumberTakeoverOfGoods, string.Empty, (System.Int32?)null);
		public System.Int32? FirstNumberTakeoverOfGoods
		{
			get { return GetProperty(firstNumberTakeoverOfGoodsProperty); }
			set { SetProperty(firstNumberTakeoverOfGoodsProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> firstNumberTravelOrderProperty = RegisterProperty<System.Int32?>(p => p.FirstNumberTravelOrder, string.Empty, (System.Int32?)null);
		public System.Int32? FirstNumberTravelOrder
		{
			get { return GetProperty(firstNumberTravelOrderProperty); }
			set { SetProperty(firstNumberTravelOrderProperty, value); }
		}

		private static readonly PropertyInfo<System.Int32?> firstNumberWorkOrderProperty = RegisterProperty<System.Int32?>(p => p.FirstNumberWorkOrder, string.Empty, (System.Int32?)null);
		public System.Int32? FirstNumberWorkOrder
		{
			get { return GetProperty(firstNumberWorkOrderProperty); }
			set { SetProperty(firstNumberWorkOrderProperty, value); }
		}
		private static readonly PropertyInfo<System.String> fiscalizationConsistenceCodeProperty = RegisterProperty<System.String>(p => p.FiscalizationConsistenceCode, string.Empty, (System.String)null);
		public System.String FiscalizationConsistenceCode
		{
			get { return GetProperty(fiscalizationConsistenceCodeProperty); }
			set { SetProperty(fiscalizationConsistenceCodeProperty, (value ?? "").Trim()); }
		}
		private static readonly PropertyInfo<bool?> taxPayerProperty = RegisterProperty<bool?>(p => p.TaxPayer, string.Empty, (bool?)null);
		public bool? TaxPayer
		{
			get { return GetProperty(taxPayerProperty); }
			set { SetProperty(taxPayerProperty, value); }
		}
		private static readonly PropertyInfo<System.Decimal?> consumptionTaxProperty = RegisterProperty<System.Decimal?>(p => p.ConsumptionTax, string.Empty, (System.Decimal?)null);
		public System.Decimal? ConsumptionTax
		{
			get { return GetProperty(consumptionTaxProperty); }
			set { SetProperty(consumptionTaxProperty, value); }
		}
		private static readonly PropertyInfo<System.Byte[]> certificateProperty = RegisterProperty<System.Byte[]>(p => p.Certificate, string.Empty, (System.Byte[])null);
		public System.Byte[] Certificate
		{
			get { return GetProperty(certificateProperty); }
			set { SetProperty(certificateProperty, value); }
		}
		private static readonly PropertyInfo<System.String> passwordProperty = RegisterProperty<System.String>(p => p.Password, string.Empty, (System.String)null);
		public System.String Password
		{
			get { return GetProperty(passwordProperty); }
			set { SetProperty(passwordProperty, (value ?? "").Trim()); }
		}

		#endregion


		#region Factory Methods

		public static cAuth_Company NewAuth_Company()
		{
			return DataPortal.Create<cAuth_Company>();

		}

		public static cAuth_Company GetAuth_Company(int uniqueId)
		{
			return DataPortal.Fetch<cAuth_Company>(new SingleCriteria<cAuth_Company, int>(uniqueId));
		}

		internal static cAuth_Company GetAuth_Company(Auth_Company data)
		{
			return DataPortal.Fetch<cAuth_Company>(data);
		}

		#endregion

		#region Data Access
		[RunLocal]
		protected override void DataPortal_Create()
		{
			BusinessRules.CheckRules();
		}

		private void DataPortal_Fetch(SingleCriteria<cAuth_Company, int> criteria)
		{
			using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
			{
				var data = ctx.ObjectContext.Auth_Company.First(p => p.Id == criteria.Value);

				LoadProperty<int>(IdProperty, data.Id);
				LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

				LoadProperty<string>(nameProperty, data.Name);
				LoadProperty<string>(oIBProperty, data.OIB);
				LoadProperty<byte[]>(systemLogoProperty, data.SystemLogo);
				LoadProperty<byte[]>(invoiceLogoProperty, data.InvoiceLogo);
				LoadProperty<byte[]>(highQualityLogoProperty, data.HighQualityLogo);
				LoadProperty<int?>(timeZoneIdProperty, data.TimeZoneId);
				LoadProperty<string>(contactPersonProperty, data.ContactPerson);
				LoadProperty<string>(businessPhoneProperty, data.BusinessPhone);
				LoadProperty<string>(mobileProperty, data.Mobile);
				LoadProperty<string>(faxProperty, data.Fax);
				LoadProperty<string>(emailProperty, data.Email);
				LoadProperty<bool?>(staffCanSendInvoiceAndQuotesProperty, data.StaffCanSendInvoiceAndQuotes);
				LoadProperty<bool?>(staffCanSeeReportsProperty, data.StaffCanSeeReports);
				LoadProperty<bool?>(staffProjectAccessProperty, data.StaffProjectAccess);
				LoadProperty<string>(accountURLProperty, data.AccountURL);
				LoadProperty<int?>(mDPlaces_Enums_Geo_CountryIdProperty, data.MDPlaces_Enums_Geo_CountryId);
				LoadProperty<int?>(mDGeneral_Enums_CurrencyIdProperty, data.MDGeneral_Enums_CurrencyId);
				LoadProperty<int?>(homeAddress_PlaceIdProperty, data.HomeAddress_PlaceId);
				LoadProperty<string>(homeAddress_StateProperty, data.HomeAddress_State);
				LoadProperty<string>(homeAddress_StreetProperty, data.HomeAddress_Street);
				LoadProperty<string>(homeAddress_NumberProperty, data.HomeAddress_Number);
				LoadProperty<int>(numberOfUsersProperty, data.NumberOfUsers);
				LoadProperty<DateTime>(activationDateProperty, data.ActivationDate);
				LoadProperty<int>(numbnerOfDaysProperty, data.NumbnerOfDays);
				LoadProperty<int?>(numberOfUsersOrderedProperty, data.NumberOfUsersOrdered);
				LoadProperty<int?>(pDVTypeProperty, data.PDVType);
				LoadProperty<int?>(firstNumberDispatchProperty, data.FirstNumberDispatch);
				LoadProperty<int?>(firstNumberIncomingInvoiceProperty, data.FirstNumberIncomingInvoice);
				LoadProperty<int?>(firstNumberInvoiceProperty, data.FirstNumberInvoice);
				LoadProperty<int?>(firstNumberOfferProperty, data.FirstNumberOffer);
				LoadProperty<int?>(firstNumberOrderFormProperty, data.FirstNumberOrderForm);
				LoadProperty<int?>(firstNumberPaymentProperty, data.FirstNumberPayment);
				LoadProperty<int?>(firstNumberPayoffProperty, data.FirstNumberPayoff);
				LoadProperty<int?>(firstNumberPriceListProperty, data.FirstNumberPriceList);
				LoadProperty<int?>(firstNumberQuoteProperty, data.FirstNumberQuote);
				LoadProperty<int?>(firstNumberTakeoverOfGoodsProperty, data.FirstNumberTakeoverOfGoods);
				LoadProperty<int?>(firstNumberTravelOrderProperty, data.FirstNumberTravelOrder);
				LoadProperty<int?>(firstNumberWorkOrderProperty, data.FirstNumberWorkOrder);

				LoadProperty<string>(accountProperty, data.Account);
				LoadProperty<string>(account1Property, data.Account1);
				LoadProperty<string>(account2Property, data.Account2);
				LoadProperty<string>(account3Property, data.Account3);

				LoadProperty<bool?>(taxPayerProperty, data.TaxPayer);
				LoadProperty<decimal?>(consumptionTaxProperty, data.ConsumptionTax);

				LoadProperty<string>(fiscalizationConsistenceCodeProperty, data.FiscalizationConsistenceCode);

				LoadProperty<byte[]>(certificateProperty, data.Certificate);
				LoadProperty<string>(passwordProperty, data.Password);

				LastChanged = data.LastChanged;

				BusinessRules.CheckRules();

			}
		}

		[Transactional(TransactionalTypes.TransactionScope)]
		protected override void DataPortal_Insert()
		{
		   
			using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
			{
			 
				var data = new Auth_Company();

				data.Name = ReadProperty<string>(nameProperty);
				data.OIB = ReadProperty<string>(oIBProperty);
				data.SystemLogo = ReadProperty<byte[]>(systemLogoProperty);
				data.InvoiceLogo = ReadProperty<byte[]>(invoiceLogoProperty);
				data.HighQualityLogo = ReadProperty<byte[]>(highQualityLogoProperty);
				data.TimeZoneId = ReadProperty<int?>(timeZoneIdProperty);
				data.ContactPerson = ReadProperty<string>(contactPersonProperty);
				data.BusinessPhone = ReadProperty<string>(businessPhoneProperty);
				data.Mobile = ReadProperty<string>(mobileProperty);
				data.Fax = ReadProperty<string>(faxProperty);
				data.Email = ReadProperty<string>(emailProperty);
				data.StaffCanSendInvoiceAndQuotes = ReadProperty<bool?>(staffCanSendInvoiceAndQuotesProperty);
				data.StaffCanSeeReports = ReadProperty<bool?>(staffCanSeeReportsProperty);
				data.StaffProjectAccess = ReadProperty<bool?>(staffProjectAccessProperty);
				data.AccountURL = ReadProperty<string>(accountURLProperty);
				data.MDPlaces_Enums_Geo_CountryId = ReadProperty<int?>(mDPlaces_Enums_Geo_CountryIdProperty);
				data.MDGeneral_Enums_CurrencyId = ReadProperty<int?>(mDGeneral_Enums_CurrencyIdProperty);
				data.HomeAddress_PlaceId = ReadProperty<int?>(homeAddress_PlaceIdProperty);
				data.HomeAddress_State = ReadProperty<string>(homeAddress_StateProperty);
				data.HomeAddress_Street = ReadProperty<string>(homeAddress_StreetProperty);
				data.HomeAddress_Number = ReadProperty<string>(homeAddress_NumberProperty);
				data.NumberOfUsers = ReadProperty<int>(numberOfUsersProperty);
				data.ActivationDate = ReadProperty<DateTime>(activationDateProperty);
				data.NumbnerOfDays = ReadProperty<int>(numbnerOfDaysProperty);
				data.NumberOfUsersOrdered = ReadProperty<int?>(numberOfUsersOrderedProperty);
				data.PDVType = ReadProperty<int?>(pDVTypeProperty);
				data.FirstNumberDispatch = ReadProperty<int?>(firstNumberDispatchProperty);
				data.FirstNumberIncomingInvoice = ReadProperty<int?>(firstNumberIncomingInvoiceProperty);
				data.FirstNumberInvoice = ReadProperty<int?>(firstNumberInvoiceProperty);
				data.FirstNumberOffer = ReadProperty<int?>(firstNumberOfferProperty);
				data.FirstNumberOrderForm = ReadProperty<int?>(firstNumberOrderFormProperty);
				data.FirstNumberPayment = ReadProperty<int?>(firstNumberPaymentProperty);
				data.FirstNumberPayoff = ReadProperty<int?>(firstNumberPayoffProperty);
				data.FirstNumberPriceList = ReadProperty<int?>(firstNumberPriceListProperty);
				data.FirstNumberQuote = ReadProperty<int?>(firstNumberQuoteProperty);
				data.FirstNumberTakeoverOfGoods = ReadProperty<int?>(firstNumberTakeoverOfGoodsProperty);
				data.FirstNumberTravelOrder = ReadProperty<int?>(firstNumberTravelOrderProperty);
				data.FirstNumberWorkOrder = ReadProperty<int?>(firstNumberWorkOrderProperty);

				data.Account = ReadProperty<string>(accountProperty);
				data.Account1 = ReadProperty<string>(account1Property);
				data.Account2 = ReadProperty<string>(account2Property);
				data.Account3 = ReadProperty<string>(account3Property);
				
				data.FiscalizationConsistenceCode= ReadProperty<string>(fiscalizationConsistenceCodeProperty);

				data.TaxPayer = ReadProperty<bool?>(taxPayerProperty);
				data.ConsumptionTax = ReadProperty<decimal?>(consumptionTaxProperty);

				data.Certificate= ReadProperty<byte[]>(certificateProperty);
				data.Password = ReadProperty<string>(passwordProperty);

				ctx.ObjectContext.AddToAuth_Company(data);
				ctx.ObjectContext.SaveChanges();


				//Get New id
				int newId = data.Id;
				//Load New Id into object
				LoadProperty(IdProperty, newId);
				//Load New EntityKey into Object
				LoadProperty(EntityKeyDataProperty, Serialize(data.EntityKey));

				cMDSubjects_Employee admin = cMDSubjects_Employee.NewMDSubjects_Employee();
				admin.IsAdmin = true;
				admin.CompanyUsingServiceId = Id;
				
				admin.UserName = GenerateUsername(data.Name);
				
				admin.Password = GetRandomPassword(6, Convert.ToInt32(DateTime.Now.Millisecond));

				admin.FirstName = data.ContactPerson;
				admin.LastName = data.ContactPerson;
				admin.Name = data.ContactPerson;

				admin.IncomingInvoice = true;
				admin.Invoice = true;
				admin.Quote = true;
				admin.Offer = true;
				admin.TravelOrder = true;
				admin.WorkOrder = true;
				admin.PriceList = true;
				admin.Payment = true;

		   

				cMDSubjects_Employee temp = admin.Clone();
				admin = temp.Save();

				SendData(admin.UserName, admin.Password, ReadProperty<string>(emailProperty));
			}

			
		}

		public void SendData(string uname, string pass, string email)
		{

			string body = "Veliko nam je zadovoljstvo što ste postali korisnik Internet servisa Alpha raèuni.<br><br>";
			body += "Vaši podaci za pristup servisu su:<br><br>";
			body += "Korisnièko ime: [$UNAME$]<br>";
			body += "Lozinka: [$PASS$]<br><br>";
			body += "Servisu možete pristupiti klikom na gumb PRIJAVA na stranici: <a href=\"http://online.alphascore.hr\">http://online.alphascore.hr</a><br><br>";
			//body += "Za pomoæ pri prvom korištenju servisa preporuèamo pogledati video „Kako zapoèeti s korištenjem servisa“,koji možete pregledati na adresi: <a href=\"http://evidencija.alphascore.hr/video/evidencija_wizard.swf\">http://evidencija.alphascore.hr/video/evidencija_wizard.swf</a><br><br>";
			//body += "Dodatnu pomoæ možete pronaæi unutar samog servisa nakon što popunite èarobnjak za inicijalni unos podataka.<br><br>";
			body += "Želimo vam puno zadovoljstva, uspjeha i poveæanje efikasnosti korištenjem servisa Alpha raèuni.<br><br>";
			body += "S poštovanjem,<br>";
			body += "Vaš Alpha Score d.o.o.<br>";
			body += "<a href=\"http://online.alphascore.hr\">http://online.alphascore.hr</a><br>";
			body += "<a href=\"http://www.alphascore.hr\">www.alphascore.hr</a><br>";

			body = body.Replace("[$UNAME$]", uname);
			body = body.Replace("[$PASS$]", pass);


			
			//string server = System.Configuration.ConfigurationManager.AppSettings["server"];
			//string from = System.Configuration.ConfigurationManager.AppSettings["from"];
			//string subjektPodaci = System.Configuration.ConfigurationManager.AppSettings["subjectPodaci"];
			//string auth = System.Configuration.ConfigurationManager.AppSettings["auth"];
			//string SmtpUname = System.Configuration.ConfigurationManager.AppSettings["uname"];
			//string SmtpPass = System.Configuration.ConfigurationManager.AppSettings["pass"];
			//string admin = System.Configuration.ConfigurationManager.AppSettings["admin"];

			string server = "mail.alphascore.hr";
			string admin = "dino@alphascore.hr";
			string subject = "Alphascore Alpha raèuni";
			string subjectPodaci = "Alphascore Alpha raèuni - podaci o korisnièkom raèunu";
			string from ="evidencija@alphascore.hr";
			string auth = "da";
			string SmtpUname = "dino@alphascore.hr";
			string SmtpPass = "score908";

			MailMessage message = new MailMessage(from, email, subjectPodaci, body);
			message.IsBodyHtml = true;
			message.Bcc.Add(admin);
			SmtpClient emailClient = new SmtpClient(server);
			if (auth == "da")
			{
				System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(SmtpUname, SmtpPass);
				emailClient.Credentials = SMTPUserInfo;
			}
			try
			{
				emailClient.Send(message);
			}
			catch
			{

			}
		}

		public string GetRandomPassword(int numChars, int seed)
		{
			string[] chars = { "a", "b", "c", "d", "e", "f", "g", "i", "j", "k", "l", "m",
								"n", "o", "p", "r", "s", "t", "u", "w", "y", "z",
								"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", };


			Random rnd = new Random(seed);
			string random = string.Empty;
			for (int i = 0; i < numChars; i++)
			{
				random += chars[rnd.Next(0, 31)];
			}
			return random;
		}

		public string GenerateUsername(string Name)
		{
			string uname = Name.Replace(" ", "").ToLower();
			if (uname.Length > 11)
			{
				uname = uname.Substring(0, 11);
			}
			uname = uname.Replace("æ", "c");
			uname = uname.Replace("è", "c");
			uname = uname.Replace("š", "s");
			uname = uname.Replace("ð", "dj");
			uname = uname.Replace("ž", "z");
			uname = uname.Replace(".", "");
			uname = uname.Replace("\"", "");
			uname = uname.Replace("-", "");

			return uname;
		}

	

		[Transactional(TransactionalTypes.TransactionScope)]
		protected override void DataPortal_Update()
		{
			using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
			{
				var data = new Auth_Company();

				data.Id = ReadProperty<int>(IdProperty);
				data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

				ctx.ObjectContext.Attach(data);

				data.Name = ReadProperty<string>(nameProperty);
				data.OIB = ReadProperty<string>(oIBProperty);
				data.SystemLogo = ReadProperty<byte[]>(systemLogoProperty);
				data.InvoiceLogo = ReadProperty<byte[]>(invoiceLogoProperty);
				data.HighQualityLogo = ReadProperty<byte[]>(highQualityLogoProperty);
				data.TimeZoneId = ReadProperty<int?>(timeZoneIdProperty);
				data.ContactPerson = ReadProperty<string>(contactPersonProperty);
				data.BusinessPhone = ReadProperty<string>(businessPhoneProperty);
				data.Mobile = ReadProperty<string>(mobileProperty);
				data.Fax = ReadProperty<string>(faxProperty);
				data.Email = ReadProperty<string>(emailProperty);
				data.StaffCanSendInvoiceAndQuotes = ReadProperty<bool?>(staffCanSendInvoiceAndQuotesProperty);
				data.StaffCanSeeReports = ReadProperty<bool?>(staffCanSeeReportsProperty);
				data.StaffProjectAccess = ReadProperty<bool?>(staffProjectAccessProperty);
				data.AccountURL = ReadProperty<string>(accountURLProperty);
				data.MDPlaces_Enums_Geo_CountryId = ReadProperty<int?>(mDPlaces_Enums_Geo_CountryIdProperty);
				data.MDGeneral_Enums_CurrencyId = ReadProperty<int?>(mDGeneral_Enums_CurrencyIdProperty);
				data.HomeAddress_PlaceId = ReadProperty<int?>(homeAddress_PlaceIdProperty);
				data.HomeAddress_State = ReadProperty<string>(homeAddress_StateProperty);
				data.HomeAddress_Street = ReadProperty<string>(homeAddress_StreetProperty);
				data.HomeAddress_Number = ReadProperty<string>(homeAddress_NumberProperty);
				data.NumberOfUsers = ReadProperty<int>(numberOfUsersProperty);
				data.ActivationDate = ReadProperty<DateTime>(activationDateProperty);
				data.NumbnerOfDays = ReadProperty<int>(numbnerOfDaysProperty);
				data.NumberOfUsersOrdered = ReadProperty<int?>(numberOfUsersOrderedProperty);
				data.PDVType = ReadProperty<int?>(pDVTypeProperty);
				data.FirstNumberDispatch = ReadProperty<int?>(firstNumberDispatchProperty);
				data.FirstNumberIncomingInvoice = ReadProperty<int?>(firstNumberIncomingInvoiceProperty);
				data.FirstNumberInvoice = ReadProperty<int?>(firstNumberInvoiceProperty);
				data.FirstNumberOffer = ReadProperty<int?>(firstNumberOfferProperty);
				data.FirstNumberOrderForm = ReadProperty<int?>(firstNumberOrderFormProperty);
				data.FirstNumberPayment = ReadProperty<int?>(firstNumberPaymentProperty);
				data.FirstNumberPayoff = ReadProperty<int?>(firstNumberPayoffProperty);
				data.FirstNumberPriceList = ReadProperty<int?>(firstNumberPriceListProperty);
				data.FirstNumberQuote = ReadProperty<int?>(firstNumberQuoteProperty);
				data.FirstNumberTakeoverOfGoods = ReadProperty<int?>(firstNumberTakeoverOfGoodsProperty);
				data.FirstNumberTravelOrder = ReadProperty<int?>(firstNumberTravelOrderProperty);
				data.FirstNumberWorkOrder = ReadProperty<int?>(firstNumberWorkOrderProperty);

				data.Account = ReadProperty<string>(accountProperty);
				data.Account1 = ReadProperty<string>(account1Property);
				data.Account2 = ReadProperty<string>(account2Property);
				data.Account3 = ReadProperty<string>(account3Property);
				data.FiscalizationConsistenceCode = ReadProperty<string>(fiscalizationConsistenceCodeProperty);
				data.TaxPayer = ReadProperty<bool?>(taxPayerProperty);
				data.ConsumptionTax = ReadProperty<decimal?>(consumptionTaxProperty);

				data.Certificate = ReadProperty<byte[]>(certificateProperty);
				data.Password = ReadProperty<string>(passwordProperty);

				ctx.ObjectContext.SaveChanges();
			}
		}
		#endregion

	   
	}
}
