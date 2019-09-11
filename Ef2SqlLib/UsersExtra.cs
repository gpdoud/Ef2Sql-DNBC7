using System;
using System.Collections.Generic;
using System.Text;

namespace Ef2SqlLib {

    public partial class Users {

        public override string ToString() {
            return $"Id[{Id}],Username[{Username}],Name[{Firstname} {Lastname}]"
                + $",Phone[{Phone}],Email[{Email}],Reviewer[{IsReviewer}],Admin[{IsAdmin}]";
        }
    }
}
