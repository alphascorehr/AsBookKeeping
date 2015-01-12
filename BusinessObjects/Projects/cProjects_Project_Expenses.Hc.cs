﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Projects
{
    public partial class cProjects_Project_Expenses
    {
        public void MarkChild()
        {
            this.MarkAsChild();
        }
    }

    public partial class cProjects_Project_Expenses_List
    {
        [Serializable]
        internal class ProjectExpenses_Criteria : Csla.CriteriaBase<ProjectExpenses_Criteria>
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

            public ProjectExpenses_Criteria(int? workorderId, int? projectId)
            { _workorderId = workorderId; _projectId = projectId; }
        }
    }
}
