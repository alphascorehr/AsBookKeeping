using System;
using Csla;
using Csla.Data;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;

namespace BusinessObjects.Documents
{
	[Serializable]
	public abstract class cDocuments_Document<T> : CoreBusinessClass<T> where T : cDocuments_Document<T>
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

		protected static readonly PropertyInfo< System.Int32 >companyUsingServiceIdProperty = RegisterProperty<System.Int32>(p => p.CompanyUsingServiceId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 CompanyUsingServiceId
		{
			get { return GetProperty(companyUsingServiceIdProperty); }
			set { SetProperty(companyUsingServiceIdProperty, value); }
		}


		protected static readonly PropertyInfo<Guid> GUIDProperty = RegisterProperty<Guid>(p => p.GUID, string.Empty, Guid.NewGuid());
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public Guid GUID
		{
			get { return GetProperty(GUIDProperty); }
			set { SetProperty(GUIDProperty, value); }
		}

		protected static readonly PropertyInfo<short> documentTypeProperty = RegisterProperty<short>(p => p.DocumentType, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public short DocumentType
		{
			get { return GetProperty(documentTypeProperty); }
			set { SetProperty(documentTypeProperty, value); }
		}

		protected static readonly PropertyInfo< System.String > uniqueIdentifierProperty = RegisterProperty<System.String>(p => p.UniqueIdentifier, String.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(50, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String UniqueIdentifier
		{
			get { return GetProperty(uniqueIdentifierProperty); }
			set { SetProperty(uniqueIdentifierProperty, (value ?? "").Trim()); }
		}

		protected static readonly PropertyInfo< System.String > barcodeProperty = RegisterProperty<System.String>(p => p.Barcode, string.Empty, (System.String)null);
		public System.String Barcode
		{
			get { return GetProperty(barcodeProperty); }
			set { SetProperty(barcodeProperty, (value ?? "").Trim()); }
		}

		protected static readonly PropertyInfo< System.Int32 > ordinalNumberProperty = RegisterProperty<System.Int32>(p => p.OrdinalNumber, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 OrdinalNumber
		{
			get { return GetProperty(ordinalNumberProperty); }
			set { SetProperty(ordinalNumberProperty, value); }
		}

		protected static readonly PropertyInfo< System.Int32 > ordinalNumberInYearProperty = RegisterProperty<System.Int32>(p => p.OrdinalNumberInYear, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 OrdinalNumberInYear
		{
			get { return GetProperty(ordinalNumberInYearProperty); }
			set { SetProperty(ordinalNumberInYearProperty, value); }
		}

		protected static readonly PropertyInfo<System.DateTime> documentDateTimeProperty = RegisterProperty<System.DateTime>(p => p.DocumentDateTime, string.Empty, System.DateTime.Now);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.DateTime DocumentDateTime
		{
			get { return GetProperty(documentDateTimeProperty); }
			set { SetProperty(documentDateTimeProperty, value); }
		}

		protected static readonly PropertyInfo< System.DateTime > creationDateTimeProperty = RegisterProperty<System.DateTime>(p => p.CreationDateTime, string.Empty,System.DateTime.Now);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.DateTime CreationDateTime
		{
			get { return GetProperty(creationDateTimeProperty); }
			set { SetProperty(creationDateTimeProperty, value); }
		}

		protected static readonly PropertyInfo< System.Int32 > mDSubjects_Employee_AuthorIdProperty = RegisterProperty<System.Int32>(p => p.MDSubjects_Employee_AuthorId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 MDSubjects_Employee_AuthorId
		{
			get { return GetProperty(mDSubjects_Employee_AuthorIdProperty); }
			set { SetProperty(mDSubjects_Employee_AuthorIdProperty, value); }
		}

		protected static readonly PropertyInfo< System.String > descriptionProperty = RegisterProperty<System.String>(p => p.Description, string.Empty, (System.String)null);
		public System.String Description
		{
			get { return GetProperty(descriptionProperty); }
			set { SetProperty(descriptionProperty, (value ?? "").Trim()); }
		}

		protected static readonly PropertyInfo< bool > inactiveProperty = RegisterProperty<bool>(p => p.Inactive, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public bool Inactive
		{
			get { return GetProperty(inactiveProperty); }
			set { SetProperty(inactiveProperty, value); }
		}

		protected static readonly PropertyInfo< System.DateTime > lastActivityDateProperty = RegisterProperty<System.DateTime>(p => p.LastActivityDate, string.Empty,System.DateTime.Now);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.DateTime LastActivityDate
		{
			get { return GetProperty(lastActivityDateProperty); }
			set { SetProperty(lastActivityDateProperty, value); }
		}

		protected static readonly PropertyInfo< System.Int32 > mDSubjects_EmployeeWhoChengedIdProperty = RegisterProperty<System.Int32>(p => p.MDSubjects_EmployeeWhoChengedId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 MDSubjects_EmployeeWhoChengedId
		{
			get { return GetProperty(mDSubjects_EmployeeWhoChengedIdProperty); }
			set { SetProperty(mDSubjects_EmployeeWhoChengedIdProperty, value); }
		}
		protected static readonly PropertyInfo<bool> lockedProperty = RegisterProperty<bool>(p => p.Locked, string.Empty);
		public bool Locked
		{
			get { return GetProperty(lockedProperty); }
			set { SetProperty(lockedProperty, value); }
		}

		protected static readonly PropertyInfo<System.String> cashBoxCodeProperty = RegisterProperty<System.String>(p => p.CashBoxCode, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(20, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		public System.String CashBoxCode
		{
			get { return GetProperty(cashBoxCodeProperty); }
			set { SetProperty(cashBoxCodeProperty, value.Trim()); }
		}

        protected static readonly PropertyInfo<System.String> locationCodeProperty = RegisterProperty<System.String>(p => p.LocationCode, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(20, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		public System.String LocationCode
		{
			get { return GetProperty(locationCodeProperty); }
			set { SetProperty(locationCodeProperty, value.Trim()); }
		}
		
		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		#region Kolekcije
		public static PropertyInfo<cDocuments_ItemsCol> Documents_ItemsColProperty = RegisterProperty<cDocuments_ItemsCol>(c => c.Documents_ItemsCol);
		public cDocuments_ItemsCol Documents_ItemsCol
		{
			get
			{
				if (!FieldManager.FieldExists(Documents_ItemsColProperty))
					LoadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty, cDocuments_ItemsCol.NewDocuments_ItemsCol());
				return GetProperty(Documents_ItemsColProperty);
			}
			private set { SetProperty(Documents_ItemsColProperty, value); }
		}

		#endregion
		#endregion
	}
}
