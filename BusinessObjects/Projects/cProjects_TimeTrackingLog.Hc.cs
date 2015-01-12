using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Projects
{
    public partial class cProjects_TimeTrackingLog
    {
    }

    public partial class cProjects_TimeTrackingLog_List
    {
        [Serializable]
        internal class TimeTracking_Criteria : Csla.CriteriaBase<TimeTracking_Criteria>
        {
            private int? _workorderId;
            private int? _projectId;

            public int? WorkorderId
            {
                get { return _workorderId; }
            }

            public int? ProjectId
            {
                get { return _projectId; }
            }

            public TimeTracking_Criteria(int? workorderId, int? projectId)
            { _workorderId = workorderId; _projectId = projectId; }
        }
    }
}
