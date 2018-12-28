using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accent.KShortestPaths.Model.Abstracts;

namespace Accent.KShortestPaths.Model
{
    public class Path : BaseElementWithWeight
    {
        private List<BaseVertex> _vertex_list = new List<BaseVertex>();
        private double _weight = -1;

        public Path()
        {
        }

        public Path(List<BaseVertex> _vertex_list, double _weight)
        {
            this._vertex_list = _vertex_list;
            this._weight = _weight;
        }

        public double get_weight()
        {
            return _weight;
        }

        public void set_weight(double weight)
        {
            _weight = weight;
        }

        public List<BaseVertex> get_vertices()
        {
            return _vertex_list;
        }



        /* (non-Javadoc)
		 * @see java.lang.Object#equals(java.lang.Object)
		 */
        public override bool Equals(object right)
        {
            if (right is Path)
            {
                Path r_path = (Path)right;
                //JAVA TO C# CONVERTER WARNING: LINQ 'SequenceEqual' is not always identical to Java AbstractList 'equals':
                //ORIGINAL LINE: return _vertex_list.equals(r_path._vertex_list);
                return _vertex_list.SequenceEqual(r_path._vertex_list);
            }
            return false;
        }

        /* (non-Javadoc)
		 * @see java.lang.Object#hashCode()
		 */
        public override int GetHashCode()
        {
            return _vertex_list.GetHashCode();
        }

        public override string ToString()
        {
            return _vertex_list.ToString() + ":" + _weight;
        }

    }
}
