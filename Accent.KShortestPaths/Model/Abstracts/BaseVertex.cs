using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accent.KShortestPaths.Model.Abstracts
{
    public interface BaseVertex
    {
        int get_id();
        double get_weight();
        void set_weight(double weight);
    }
}
