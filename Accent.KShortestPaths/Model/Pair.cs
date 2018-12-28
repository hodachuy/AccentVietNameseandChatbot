using Accent.KShortestPaths.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accent.KShortestPaths.Model
{
    public class Pair<TYPE1, TYPE2>
    {
        public TYPE1 o1;
        public TYPE2 o2;

        public Pair(TYPE1 o1, TYPE2 o2)
        {
            this.o1 = o1;
            this.o2 = o2;
        }

        public virtual TYPE1 first()
        {
            return o1;
        }

        public virtual TYPE2 second()
        {
            return o2;
        }


        /* (non-Javadoc)
		 * @see java.lang.Object#hashCode()
		 * Note, I don't know if it works well. Maybe in some tricky case, collision may happen.
		 */
        public override int GetHashCode()
        {
            int code = 0;
            if (o1 != null)
            {
                code = o1.GetHashCode();
            }
            if (o2 != null)
            {
                code = code / 2 + o2.GetHashCode() / 2;
            }
            return code;
        }

        public static bool same(object o1, object o2)
        {
            return o1 == null ? o2 == null : o1.Equals(o2);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @SuppressWarnings("unchecked") public boolean equals(Object obj)
        public override bool Equals(object obj)
        {
            if (!(obj is Pair<TYPE1, TYPE2>))
            {
                return false;
            }
            Pair<TYPE1, TYPE2> p = (Pair<TYPE1, TYPE2>)obj;
            return same(p.o1, this.o1) && same(p.o2, this.o2);
        }

        public override string ToString()
        {
            return "Pair{" + o1 + ", " + o2 + "}";
        }
    }
}
