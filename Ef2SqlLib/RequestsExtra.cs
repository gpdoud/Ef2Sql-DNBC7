using System;
using System.Collections.Generic;
using System.Text;

namespace Ef2SqlLib {

    public partial class Requests {

        public override string ToString() {
            return $"Id[{Id}],Description[{Description}],Justification[{Justification}]"
                + $",RejectionReason[{RejectionReason}],DeliveryMode[{DeliveryMode}]"
                + $",Status[{Status}],Total[{Total},User[{User.Firstname} {User.Lastname}]";
        }

    }
}
