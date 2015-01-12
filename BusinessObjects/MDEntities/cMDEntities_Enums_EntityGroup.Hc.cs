using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.MDEntities
{
    public partial class cMDEntities_Enums_EntityGroup
    {
    }

    [Serializable]
    public class EntityGroup_Criteria : Csla.CriteriaBase<EntityGroup_Criteria>
    {
        private int? _companyId;
        private int? _includeInactiveId;
        private int? _excludeGroupId;

        public int? CompanyId
        {
            get { return _companyId; }
        }

        public int? IncludeInactiveId
        {
            get { return _includeInactiveId; }
        }

        public int? ExcludeGroupId
        {
            get { return _excludeGroupId; }
        }

        public EntityGroup_Criteria(int? companyId, int? includeInactiveId, int? excludeGroupId)
        { _companyId = companyId; _includeInactiveId = includeInactiveId; _excludeGroupId = excludeGroupId; }
    }
 
}
