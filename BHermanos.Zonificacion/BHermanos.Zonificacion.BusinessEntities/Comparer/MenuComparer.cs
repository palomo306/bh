using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities.Comparer
{
    public class MenuComparer : IEqualityComparer<Menu>
    {
        public bool Equals(Menu x, Menu y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Menu obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}