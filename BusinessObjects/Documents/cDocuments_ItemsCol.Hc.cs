using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using DalEf;

namespace BusinessObjects.Documents
{
    public partial class cDocuments_Items
    {
        #region Custom propeties

        protected static readonly PropertyInfo<System.String> labelProperty = RegisterProperty<System.String>(p => p.Label, string.Empty);
        public System.String Label
        {
            get { return GetProperty(labelProperty); }
            set { SetProperty(labelProperty, (value ?? "").Trim()); }
        }

        #endregion

        partial void ItemLoaded()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var productItem = ctx.ObjectContext.vEntitiesForDocuments.SingleOrDefault(p => p.Id == this.ProductId);
                if (productItem != null)
                    LoadProperty<string>(labelProperty, productItem.Label);

            }

        }
      
    }
}
