using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DB.Constant
{
    public enum EventSlotType
    {
        Morning,
        Evening,
        FullDay,
        MultipleDay
    }
    public enum Status
    {
        pending,
        OnProcess,
        Finish
    }
    public enum Userrole
    {
        Admin,
        Operator,
        Staff
    }

    public enum InquiryStatusType
    {
        InDiscussion,
        Reject,
        EventGenerated
    }
}
