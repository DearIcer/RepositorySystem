using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Enums
{
    public enum WorkFlow_InstanceStepStatusEnum
    {
        审核中 = 1,
        同意  = 2,
        驳回 = 3,
        作废 = 4,
        已被他人审核 = 5
    }
}
