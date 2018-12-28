using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accent.KShortestPaths.Model.Abstracts
{
    public interface BaseGraph
    {
        List<BaseVertex> get_vertex_list();

        double get_edge_weight(BaseVertex source, BaseVertex sink);
        HashSet<BaseVertex> get_adjacent_vertices(BaseVertex vertex);
        HashSet<BaseVertex> get_precedent_vertices(BaseVertex vertex);
    }
}
