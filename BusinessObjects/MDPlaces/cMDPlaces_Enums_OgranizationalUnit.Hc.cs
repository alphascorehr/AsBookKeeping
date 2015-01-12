using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.MDPlaces
{
    public partial class cMDPlaces_Enums_OgranizationalUnit
    {
     
    }

    public partial class cMDPlaces_Enums_OgranizationalUnit_List
    {
        [Serializable]
        internal class OgranizationalUnit_Criteria : Csla.CriteriaBase<OgranizationalUnit_Criteria>
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

            public OgranizationalUnit_Criteria(int? companyId, int? includeInactiveId)
            { _companyId = companyId; _includeInactiveId = includeInactiveId; }
        }
    }
}
