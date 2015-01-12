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
    public partial class cDocuments_PaymentItems
    {

        /// <summary>
        /// SLuži za privremeno spremanje učitanog kupca (za usporedbu kod snimanja - ako ne odgovara SubjectBuyerId na parentu, brise se!)
        /// </summary>
        public static readonly PropertyInfo<System.Int32?> linkOverpaidWithOrdinalNumberProperty = RegisterProperty<System.Int32?>(p => p.LinkOverpaidWithOrdinalNumber, string.Empty);
        public System.Int32? LinkOverpaidWithOrdinalNumber
        {
            get { return GetProperty(linkOverpaidWithOrdinalNumberProperty); }
            set { SetProperty(linkOverpaidWithOrdinalNumberProperty, value); }
        }
    }

   

}
