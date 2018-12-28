using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accent.KShortestPaths.Model;
using Accent.KShortestPaths.Model.Abstracts;
//using Accent.KShortestPaths.Common;
using Priority_Queue;
namespace Accent.KShortestPaths.Controller
{
    public class DijkstraShortestPathAlg
    {
        // Input
        internal BaseGraph _graph = null;

        // Intermediate variables
        internal HashSet<BaseVertex> _determined_vertex_set = new HashSet<BaseVertex>();
        internal SimplePriorityQueue<BaseVertex> _vertex_candidate_queue = new SimplePriorityQueue<BaseVertex>();
        internal Dictionary<BaseVertex, double> _start_vertex_distance_index = new Dictionary<BaseVertex, double>();

        internal Dictionary<BaseVertex, BaseVertex> _predecessor_index = new Dictionary<BaseVertex, BaseVertex>();

        public DijkstraShortestPathAlg(BaseGraph graph)
        {
            _graph = graph;
        }

        /// <summary>
        /// Clear intermediate variables.
        /// </summary>
        public virtual void clear()
        {
            _determined_vertex_set.Clear();
            _vertex_candidate_queue.Clear();
            _start_vertex_distance_index.Clear();
            _predecessor_index.Clear();
        }

        /// <summary>
		/// Getter for the distance in terms of the start vertex
		/// 
		/// @return
		/// </summary>
		public virtual Dictionary<BaseVertex, double> get_start_vertex_distance_index()
        {
            return _start_vertex_distance_index;
        }

        /// <summary>
        /// Getter for the index of the predecessors of vertices
        /// @return
        /// </summary>
        public virtual Dictionary<BaseVertex, BaseVertex> get_predecessor_index()
        {
            return _predecessor_index;
        }

        /// <summary>
        /// Construct a tree rooted at "root" with 
        /// the shortest paths to the other vertices.
        /// </summary>
        /// <param name="root"> </param>
        public virtual void get_shortest_path_tree(BaseVertex root)
        {
            determine_shortest_paths(root, null, true);
        }

        /// <summary>
        /// Construct a flower rooted at "root" with 
        /// the shortest paths from the other vertices.
        /// </summary>
        /// <param name="root"> </param>
        public virtual void get_shortest_path_flower(BaseVertex root)
        {
            determine_shortest_paths(null, root, false);
        }

        /// <summary>
		/// Do the work
		/// </summary>
		protected internal virtual void determine_shortest_paths(BaseVertex source_vertex, BaseVertex sink_vertex, bool is_source2sink)
        {
            // 0. clean up variables
            clear();

            // 1. initialize members
            BaseVertex end_vertex = is_source2sink ? sink_vertex : source_vertex;
            BaseVertex start_vertex = is_source2sink ? source_vertex : sink_vertex;
            _start_vertex_distance_index[start_vertex] = 0d;
            start_vertex.set_weight(0d);
            _vertex_candidate_queue.Enqueue(start_vertex,0);

            // 2. start searching for the shortest path
            while (_vertex_candidate_queue.Count() != 0)
            {
                BaseVertex cur_candidate = _vertex_candidate_queue.Dequeue();

                if (cur_candidate.Equals(end_vertex))
                {
                    break;
                }

                _determined_vertex_set.Add(cur_candidate);

                _improve_to_vertex(cur_candidate, is_source2sink);
            }
        }
        /// <summary>
		/// Update the distance from the source to the concerned vertex. </summary>
		/// <param name="vertex"> </param>
		private void _improve_to_vertex(BaseVertex vertex, bool is_source2sink)
        {

            // 1. get the neighboring vertices 
            HashSet<BaseVertex> neighbor_vertex_list = is_source2sink ? _graph.get_adjacent_vertices(vertex) : _graph.get_precedent_vertices(vertex);

            // 2. update the distance passing on current vertex
            foreach (BaseVertex cur_adjacent_vertex in neighbor_vertex_list)
            {
                // 2.1 skip if visited before
                if (_determined_vertex_set.Contains(cur_adjacent_vertex))
                {
                    continue;
                }

                // 2.2 calculate the new distance
                double distance = _start_vertex_distance_index.ContainsKey(vertex) ? _start_vertex_distance_index[vertex] : Graph.DISCONNECTED;

                distance += is_source2sink ? _graph.get_edge_weight(vertex, cur_adjacent_vertex) : _graph.get_edge_weight(cur_adjacent_vertex, vertex);

                // 2.3 update the distance if necessary
                if (!_start_vertex_distance_index.ContainsKey(cur_adjacent_vertex) || _start_vertex_distance_index[cur_adjacent_vertex] > distance)
                {
                    _start_vertex_distance_index[cur_adjacent_vertex] = distance;

                    _predecessor_index[cur_adjacent_vertex] = vertex;

                    cur_adjacent_vertex.set_weight(distance);
                    _vertex_candidate_queue.EnqueueWithoutDuplicates(cur_adjacent_vertex, (float)distance);// Lớp PriorytyQueue quan trong can thuc hien đổi lại

                }
            }
        }
        /// <summary>
        /// Note that, the source should not be as same as the sink! (we could extend 
        /// this later on)
        /// </summary>
        /// <param name="source_vertex"> </param>
        /// <param name="sink_vertex">
        /// @return </param>
        public virtual Path get_shortest_path(BaseVertex source_vertex, BaseVertex sink_vertex)
        {
            determine_shortest_paths(source_vertex, sink_vertex, true);
            //
            List<BaseVertex> vertex_list = new List<BaseVertex>();
            double weight = _start_vertex_distance_index.ContainsKey(sink_vertex) ? _start_vertex_distance_index[sink_vertex] : Graph.DISCONNECTED;
            if (weight != Graph.DISCONNECTED)
            {
                BaseVertex cur_vertex = sink_vertex;
                do
                {
                    vertex_list.Add(cur_vertex);
                    cur_vertex = _predecessor_index[cur_vertex];
                } while (cur_vertex != null && cur_vertex != source_vertex);
                //
                vertex_list.Add(source_vertex);
                vertex_list.Reverse();
            }
            //
            return new Path(vertex_list, weight);
        }
        /// for updating the cost

        /// <summary>
        /// Calculate the distance from the target vertex to the input 
        /// vertex using forward star form. 
        /// (FLOWER)
        /// </summary>
        /// <param name="vertex"> </param>
        public virtual Path update_cost_forward(BaseVertex vertex)
        {
            double cost = Graph.DISCONNECTED;

            // 1. get the set of successors of the input vertex
            HashSet<BaseVertex> adj_vertex_set = _graph.get_adjacent_vertices(vertex);

            // 2. make sure the input vertex exists in the index
            if (!_start_vertex_distance_index.ContainsKey(vertex))
            {
                _start_vertex_distance_index[vertex] = Graph.DISCONNECTED;
            }

            // 3. update the distance from the root to the input vertex if necessary
            foreach (BaseVertex cur_vertex in adj_vertex_set)
            {
                // 3.1 get the distance from the root to one successor of the input vertex
                double distance = _start_vertex_distance_index.ContainsKey(cur_vertex) ? _start_vertex_distance_index[cur_vertex] : Graph.DISCONNECTED;

                // 3.2 calculate the distance from the root to the input vertex
                distance += _graph.get_edge_weight(vertex, cur_vertex);
                //distance += ((VariableGraph)_graph).get_edge_weight_of_graph(vertex, cur_vertex);

                // 3.3 update the distance if necessary 
                double cost_of_vertex = _start_vertex_distance_index[vertex];
                if (cost_of_vertex > distance)
                {
                    _start_vertex_distance_index[vertex] = distance;
                    _predecessor_index[vertex] = cur_vertex;
                    cost = distance;
                }
            }

            // 4. create the sub_path if exists
            Path sub_path = null;
            if (cost < Graph.DISCONNECTED)
            {
                sub_path = new Path();
                sub_path.set_weight(cost);
                List<BaseVertex> vertex_list = sub_path.get_vertices();
                vertex_list.Add(vertex);

                BaseVertex sel_vertex = _predecessor_index[vertex];
                while (sel_vertex != null)
                {
                    vertex_list.Add(sel_vertex);
                    
                    if (_predecessor_index.ContainsKey(sel_vertex))
                    {
                        sel_vertex = _predecessor_index[sel_vertex];
                    }
                    else
                    {
                        sel_vertex = null;
                    }
                }
            }

            return sub_path;
        }

        public virtual void correct_cost_backward(BaseVertex vertex)
        {
            // 1. initialize the list of vertex to be updated
            List<BaseVertex> vertex_list = new List<BaseVertex>();
            vertex_list.Add(vertex);

            // 2. update the cost of relevant precedents of the input vertex
            if (vertex_list.Count != 0)
            {
                //vertex_list.RemoveFirst();
                //if (vertex_list.Count > 1)
                //    vertex_list.RemoveAt(0);
                BaseVertex cur_vertex = vertex_list[0];
                double cost_of_cur_vertex = _start_vertex_distance_index[cur_vertex];

                HashSet<BaseVertex> pre_vertex_set = _graph.get_precedent_vertices(cur_vertex);
                foreach (BaseVertex pre_vertex in pre_vertex_set)
                {
                    double cost_of_pre_vertex = _start_vertex_distance_index.ContainsKey(pre_vertex) ? _start_vertex_distance_index[pre_vertex] : Graph.DISCONNECTED;

                    double fresh_cost = cost_of_cur_vertex + _graph.get_edge_weight(pre_vertex, cur_vertex);
                    //double fresh_cost = cost_of_cur_vertex + ((VariableGraph)_graph).get_edge_weight_of_graph(pre_vertex, cur_vertex);
                    if (cost_of_pre_vertex > fresh_cost)
                    {
                        // _start_vertex_distance_index[pre_vertex] = fresh_cost;
                        if (_start_vertex_distance_index.ContainsKey(pre_vertex))
                        {
                            _start_vertex_distance_index[pre_vertex] = fresh_cost;
                        }
                        else
                        {
                            _start_vertex_distance_index.Add(pre_vertex, fresh_cost);
                        }
                        //_start_vertex_distance_index[pre_vertex] = fresh_cost;
                        if (_predecessor_index.ContainsKey(pre_vertex))
                        {
                            _start_vertex_distance_index[pre_vertex] = fresh_cost;
                        }
                        else
                        {
                            _predecessor_index.Add(pre_vertex, cur_vertex);
                        }


                        vertex_list.Add(pre_vertex);
                    }
                }
            }
        }


    }
}
