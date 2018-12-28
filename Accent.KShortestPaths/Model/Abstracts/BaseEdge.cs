using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accent.KShortestPaths.Model.Abstracts
{
    public interface BaseEdge
    {
        int get_weight();
        BaseVertex get_start_vertex();
        BaseVertex get_end_vertex();
    }
}
