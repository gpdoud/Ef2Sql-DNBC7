using System;
using System.Collections.Generic;
using System.Text;

namespace Ef2SqlLib {

    public partial class RequestLines {

        public override string ToString() {
            return $"Id[{Id}],Request[{Request.Description}],Product[{Product.Name}]"
                + $",Quantity[{Quantity}]";
        }

    }
}
