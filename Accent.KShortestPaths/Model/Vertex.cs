using Accent.KShortestPaths.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accent.KShortestPaths.Model
{
    public class Vertex : BaseVertex, IComparable<Vertex>
    {
        private static int CURRENT_VERTEX_NUM = 0;
        private int _id = CURRENT_VERTEX_NUM++;
        private double _weight = 0;

        /// 
        public virtual int get_id()
        {
            return _id;
        }

        public override string ToString()
        {
            return "" + _id;
        }

        public virtual double get_weight()
        {
            return _weight;
        }

        public virtual void set_weight(double status)
        {
            _weight = status;
        }

        public virtual int CompareTo(Vertex r_vertex)
        {
            double diff = this._weight - r_vertex._weight;
            if (diff > 0)
            {
                return 1;
            }
            else if (diff < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public static void reset()
        {
            CURRENT_VERTEX_NUM = 0;
        }        
    }
}
