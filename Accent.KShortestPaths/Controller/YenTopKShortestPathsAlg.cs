using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accent.KShortestPaths.Model;
using Accent.KShortestPaths.Model.Abstracts;
using System.Collections;

namespace Accent.KShortestPaths.Controller
{
    public class YenTopKShortestPathsAlg
    {
        private VariableGraph _graph = null;

        // intermediate variables
        private List<Path> _result_list = new List<Path>();
        private Dictionary<Path, BaseVertex> _path_derivation_vertex_index = new Dictionary<Path, BaseVertex>();
        private QYPriorityQueue<Path> _path_candidates = new QYPriorityQueue<Path>();

        // the ending vertices of the paths
        private BaseVertex _source_vertex = null;
        private BaseVertex _target_vertex = null;

        // variables for debugging and testing
        private int _generated_path_num = 0;

        /// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="graph"> </param>
		/// <param name="k"> </param>
		public YenTopKShortestPathsAlg(BaseGraph graph) : this(graph, null, null)
        {

        }

        /// <summary>
		/// Constructor 2
		/// </summary>
		/// <param name="graph"> </param>
		/// <param name="source_vt"> </param>
		/// <param name="target_vt"> </param>
		public YenTopKShortestPathsAlg(BaseGraph graph, BaseVertex source_vt, BaseVertex target_vt)
        {
            if (graph == null)
            {
                throw new System.ArgumentException("A NULL graph object occurs!");
            }
            //
            _graph = new VariableGraph((Graph)graph);
            _source_vertex = source_vt;
            _target_vertex = target_vt;
            //
            _init();
        }

        /// <summary>
		/// Initiate members in the class. 
		/// </summary>
		private void _init()
        {
            clear();
            // get the shortest path by default if both source and target exist
            if (_source_vertex != null && _target_vertex != null)
            {
                Path shortest_path = get_shortest_path(_source_vertex, _target_vertex);
                if (shortest_path.get_vertices().Count != 0)
                {
                    _path_candidates.add(shortest_path);
                    _path_derivation_vertex_index[shortest_path] = _source_vertex;
                }
            }
        }

        /// <summary>
        /// Clear the variables of the class. 
        /// </summary>
        public void clear()
        {
            _path_candidates = new QYPriorityQueue<Path>();
            _path_derivation_vertex_index.Clear();
            _result_list.Clear();
            _generated_path_num = 0;
        }

        /// <summary>
		/// Obtain the shortest path connecting the source and the target, by using the
		/// classical Dijkstra shortest path algorithm. 
		/// </summary>
		/// <param name="source_vt"> </param>
		/// <param name="target_vt">
		/// @return </param>
		public Path get_shortest_path(BaseVertex source_vt, BaseVertex target_vt)
        {
            DijkstraShortestPathAlg dijkstra_alg = new DijkstraShortestPathAlg(_graph);
            return dijkstra_alg.get_shortest_path(source_vt, target_vt);
        }

        /// <summary>
        /// Check if there exists a path, which is the shortest among all candidates.  
        /// 
        /// @return
        /// </summary>
        public bool has_next()
        {
            return !_path_candidates.Empty;
        }

        public Path next()
        {
            //3.1 prepare for removing vertices and arcs
            Path cur_path = _path_candidates.poll();
            _result_list.Add(cur_path);
            BaseVertex cur_derivation = _path_derivation_vertex_index[cur_path];

            
            //#################### Tính lại hash code theo công thức giống như java đã giup thuật toán chạy đúng hehe #####################//

            int cur_path_hash = get_hashcode(cur_path.get_vertices().GetRange(0, cur_path.get_vertices().IndexOf(cur_derivation)));
            //int cur_path_hash = cur_path.get_vertices().GetRange(0, cur_path.get_vertices().IndexOf(cur_derivation)).GetHashCode();
            int count = _result_list.Count();

            //3.2 remove the vertices and arcs in the graph
            for (int i = 0; i < count - 1; ++i)
            {
                Path cur_result_path = _result_list[i];

                int cur_dev_vertex_id = cur_result_path.get_vertices().IndexOf(cur_derivation);

                if (cur_dev_vertex_id < 0)
                {
                    continue;
                }

                // Note that the following condition makes sure all candidates should be considered. 
                /// The algorithm in the paper is not correct for removing some candidates by mistake. 
                /// 


                int path_hash = get_hashcode(cur_result_path.get_vertices().GetRange(0, cur_dev_vertex_id));
                //int path_hash = cur_result_path.get_vertices().GetRange(0, cur_dev_vertex_id).GetHashCode();
                if (path_hash != cur_path_hash)
                {
                    continue;
                }

                BaseVertex cur_succ_vertex = cur_result_path.get_vertices()[cur_dev_vertex_id + 1];

                _graph.remove_edge(new Pair<int, int>(cur_derivation.get_id(), cur_succ_vertex.get_id()));
            }

            int path_length = cur_path.get_vertices().Count();
            List<BaseVertex> cur_path_vertex_list = cur_path.get_vertices();
            for (int i = 0; i < path_length - 1; ++i)
            {
                _graph.remove_vertex(cur_path_vertex_list[i].get_id());
                _graph.remove_edge(new Pair<int, int>(cur_path_vertex_list[i].get_id(), cur_path_vertex_list[i + 1].get_id()));
            }

            //3.3 calculate the shortest tree rooted at target vertex in the graph
            DijkstraShortestPathAlg reverse_tree = new DijkstraShortestPathAlg(_graph);
            reverse_tree.get_shortest_path_flower(_target_vertex);

            //3.4 recover the deleted vertices and update the cost and identify the new candidate results
            bool is_done = false;
            for (int i = path_length - 2; i >= 0 && !is_done; --i)
            {
                //3.4.1 get the vertex to be recovered
                BaseVertex cur_recover_vertex = cur_path_vertex_list[i];
                _graph.recover_removed_vertex(cur_recover_vertex.get_id());

                //3.4.2 check if we should stop continuing in the next iteration
                if (cur_recover_vertex.get_id() == cur_derivation.get_id())
                {
                    is_done = true;
                }

                //3.4.3 calculate cost using forward star form
                Path sub_path = reverse_tree.update_cost_forward(cur_recover_vertex);

                //3.4.4 get one candidate result if possible
                if (sub_path != null)
                {
                    ++_generated_path_num;

                    //3.4.4.1 get the prefix from the concerned path
                    double cost = 0;
                    List<BaseVertex> pre_path_list = new List<BaseVertex>();
                    reverse_tree.correct_cost_backward(cur_recover_vertex);

                    for (int j = 0; j < path_length; ++j)
                    {
                        BaseVertex cur_vertex = cur_path_vertex_list[j];
                        if (cur_vertex.get_id() == cur_recover_vertex.get_id())
                        {
                            j = path_length;
                        }
                        else
                        {
                            cost += _graph.get_edge_weight_of_graph(cur_path_vertex_list[j], cur_path_vertex_list[j + 1]);
                            pre_path_list.Add(cur_vertex);
                        }
                    }

                    //((List<BaseVertex>)pre_path_list).AddRange(sub_path.get_vertices());
                    foreach (var item in sub_path.get_vertices())
                    {
                        pre_path_list.Add(item);
                    }

                    //3.4.4.2 compose a candidate
                    sub_path.set_weight(cost + sub_path.get_weight());
                    sub_path.get_vertices().Clear();

                    //sub_path.get_vertices().AddRange(pre_path_list);
                    foreach (var item in pre_path_list)
                    {
                        sub_path.get_vertices().Add(item);
                    }


                    //3.4.4.3 put it in the candidate pool if new
                    if (!_path_derivation_vertex_index.ContainsKey(sub_path))
                    {
                        _path_candidates.add(sub_path);
                        //_path_derivation_vertex_index[sub_path] = cur_recover_vertex;
                        if (_path_derivation_vertex_index.ContainsKey(sub_path))
                        {
                            _path_derivation_vertex_index[sub_path] = cur_recover_vertex;
                        }
                        else
                        {
                            _path_derivation_vertex_index.Add(sub_path, cur_recover_vertex);
                        }

                    }
                }

                //3.4.5 restore the edge
                BaseVertex succ_vertex = cur_path_vertex_list[i + 1];
                _graph.recover_removed_edge(new Pair<int, int>(cur_recover_vertex.get_id(), succ_vertex.get_id()));

                //3.4.6 update cost if necessary
                double cost_1 = _graph.get_edge_weight(cur_recover_vertex, succ_vertex) + reverse_tree.get_start_vertex_distance_index()[succ_vertex];

                if (reverse_tree.get_start_vertex_distance_index()[cur_recover_vertex] > cost_1)
                {
                    //reverse_tree.get_start_vertex_distance_index()[cur_recover_vertex] = cost_1;
                    if (reverse_tree.get_start_vertex_distance_index().ContainsKey(cur_recover_vertex))
                    {
                        reverse_tree.get_start_vertex_distance_index()[cur_recover_vertex] = cost_1;
                    }
                    else
                    {
                        reverse_tree.get_start_vertex_distance_index().Add(cur_recover_vertex, cost_1);
                    }
                    //reverse_tree.get_predecessor_index()[cur_recover_vertex] = succ_vertex;
                    if (reverse_tree.get_predecessor_index().ContainsKey(cur_recover_vertex))
                    {
                        reverse_tree.get_predecessor_index()[cur_recover_vertex] = succ_vertex;
                    }
                    else
                    {
                        reverse_tree.get_predecessor_index().Add(cur_recover_vertex, succ_vertex);
                    }

                    reverse_tree.correct_cost_backward(cur_recover_vertex);
                }
            }

            //3.5 restore everything
            _graph.recover_removed_edges();
            _graph.recover_removed_vertices();

            //
            return cur_path;
        }

        /// <summary>
        /// Get the top-K shortest paths connecting the source and the target.  
        /// This is a batch execution of top-K results.
        /// </summary>
        /// <param name="source"> </param>
        /// <param name="sink"> </param>
        /// <param name="top_k">
        /// @return </param>
        public List<Path> get_shortest_paths(BaseVertex source_vertex, BaseVertex target_vertex, int top_k)
        {
            _source_vertex = source_vertex;
            _target_vertex = target_vertex;

            _init();
            int count = 0;
            while (has_next() && count < top_k)
            {
                next();
                ++count;
            }

            return _result_list;
        }

        /// <summary>
        /// Return the list of results generated on the whole.
        /// (Note that some of them are duplicates)
        /// @return
        /// </summary>
        public List<Path> get_result_list()
        {
            return _result_list;
        }

        /// <summary>
        /// The number of distinct candidates generated on the whole. 
        /// @return
        /// </summary>
        public int get_cadidate_size()
        {
            return _path_derivation_vertex_index.Count();
        }

        public int get_generated_path_size()
        {
            return _generated_path_num;
        }

        public int get_hashcode<T>(List<T> E)
        {
            int hashCode = 1;
            foreach (var e in E)
                hashCode = 31 * hashCode + (e == null ? 0 : hashCode);
            return hashCode;
        }
    }  
}
