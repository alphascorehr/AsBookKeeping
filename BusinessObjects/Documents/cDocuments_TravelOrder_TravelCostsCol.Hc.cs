using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using DalEf;

namespace BusinessObjects.Documents
{
    public partial class cDocuments_TravelOrder_TravelCosts
    {
        #region Custom propeties

        protected static readonly PropertyInfo<System.String> fromPlaceNameProperty = RegisterProperty<System.String>(p => p.FromPlaceName, string.Empty);
        public System.String FromPlaceName
        {
            get { return GetProperty(fromPlaceNameProperty); }
            set { SetProperty(fromPlaceNameProperty, (value ?? "").Trim()); }
        }

        protected static readonly PropertyInfo<System.String> toPlaceNameProperty = RegisterProperty<System.String>(p => p.ToPlaceName, string.Empty);
        public System.String ToPlaceName
        {
            get { return GetProperty(toPlaceNameProperty); }
            set { SetProperty(toPlaceNameProperty, (value ?? "").Trim()); }
        }

        #endregion

        public void MarkChild()
        {
            this.MarkAsChild();
        }

        partial void  ItemLoaded()
        {
            
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var fromPlaceItem = ctx.ObjectContext.MDPlaces_Enums_Geo_Place.SingleOrDefault(p => p.Id == this.MDPlaces_Enums_Geo_FromPlaceId);
                if (fromPlaceItem != null)
                    LoadProperty<string>(fromPlaceNameProperty, fromPlaceItem.Name);

                var toPlaceItem = ctx.ObjectContext.MDPlaces_Enums_Geo_Place.SingleOrDefault(p => p.Id == this.MDPlaces_Enums_Geo_ToPlaceId);
                if (toPlaceItem != null)
                    LoadProperty<string>(toPlaceNameProperty, toPlaceItem.Name);

            }

        }
     
    }
}
