using System;
using Csla;
using Csla.Data;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;
using DalEf;
using System.Collections.Generic;

namespace BusinessObjects.Documents
{
    public partial class cDocuments_PaymentClosureG
    {
        public static readonly PropertyInfo<System.Decimal> payedProperty = RegisterProperty<System.Decimal>(p => p.Payed, string.Empty);
        /// <summary>
        /// SLuži za privremeno spremanje učitanog iznosa Ammounta (moramo ga znati i nakon promjene samog ammounta kod updatea plaćenih dokumenata)
        /// </summary>
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Decimal Payed
        {
            get { return GetProperty(payedProperty); }
            set { SetProperty(payedProperty, value); }
        }

        /// <summary>
        /// SLuži za privremeno spremanje učitanog kupca (za usporedbu kod snimanja - ako ne odgovara SubjectBuyerId na parentu, brise se!)
        /// </summary>
        public static readonly PropertyInfo<System.Int32?> subjectBuyerIdProperty = RegisterProperty<System.Int32?>(p => p.SubjectBuyerId, string.Empty);
        public System.Int32? SubjectBuyerId
        {
            get { return GetProperty(subjectBuyerIdProperty); }
            set { SetProperty(subjectBuyerIdProperty, value); }
        }

        /// <summary>
        /// SLuži za privremeno spremanje učitanog dobavljaca (za usporedbu kod snimanja - ako ne odgovara SubjectSupplierId na parentu, brise se!)
        /// </summary>
        public static readonly PropertyInfo<System.Int32?> subjectSupplierIdProperty = RegisterProperty<System.Int32?>(p => p.SubjectSupplierId, string.Empty);
        public System.Int32? SubjectSupplierId
        {
            get { return GetProperty(subjectSupplierIdProperty); }
            set { SetProperty(subjectSupplierIdProperty, value); }
        }
    }
}
