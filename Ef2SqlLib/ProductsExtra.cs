using System;
using System.Collections.Generic;
using System.Text;

namespace Ef2SqlLib {

    public partial class Products {

        public override string ToString() {
            return $"Id[{Id}],PartNbr[{PartNbr}],Name[{Name}]"
                + $",Price[{Price}],Unit[{Unit}],PhotoPath[{PhotoPath}],Vendor[{Vendor.Name}]";
        }
    }
}
