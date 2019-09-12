using System;
using System.Collections.Generic;
using System.Text;

namespace Ef2SqlLib {

    public partial class Vendors {

        public override string ToString() {
            return $"Id[{Id}],Code[{Code}],Name[{Name}]"
                + $",Address[{Address}, {City}, {State} {Zip}"
                + $",Phone[{Phone}],Email[{Email}]";
        }
    }
}
