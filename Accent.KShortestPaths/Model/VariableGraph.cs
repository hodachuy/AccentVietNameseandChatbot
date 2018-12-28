using Accent.KShortestPaths.Controller;
using Accent.KShortestPaths.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accent.KShortestPaths.Model
{
    public class VariableGraph : Graph
    {
        internal HashSet<int> _rem_vertex_id_set = new HashSet<int>();
        internal HashSet<Pair<int, int>> _rem_edge_set = new HashSet<Pair<int, int>>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public VariableGraph()
        {
        }

        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="data_file_name"> </param>
        public VariableGraph(string data_file_name) : base(data_file_name)
        {
        }

        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="graph"> </param>
        public VariableGraph(Graph graph) : base(graph)
        {
        }

        /// <summary>
        /// Set the set of vertices to be removed from the graph
        /// </summary>
        /// <param name="_rem_vertex_list"> </param>
        public virtual void set_rem_vertex_id_list(List<int> _rem_vertex_list)
        {
            //this._rem_vertex_id_set = _rem_vertex_list;
            foreach (var item in _rem_vertex_list)
            {
                _rem_vertex_id_set.Add(item);
            }

        }

        /// <summary>
        /// Set the set of edges to be removed from the graph
        /// </summary>
        /// <param name="_rem_edge_hashcode_set"> </param>
        public virtual void set_rem_edge_hashcode_set(List<Pair<int, int>> rem_edge_collection)
        {
            //_rem_edge_set = rem_edge_collection;
            foreach (var item in rem_edge_collection)
            {
                _rem_edge_set.Add(item);
            }
        }

        /// <summary>
        /// Add an edge to the set of removed edges
        /// </summary>
        /// <param name="edge"> </param>
        public virtual void remove_edge(Pair<int, int> edge)
        {
            _rem_edge_set.Add(edge);
        }

        /// <summary>
        /// Add a vertex to the set of removed vertices
        /// </summary>
        /// <param name="vertex_id"> </param>
        public virtual void remove_vertex(int? vertex_id)
        {
            _rem_vertex_id_set.Add(vertex_id.GetValueOrDefault());
        }

        public virtual void recover_removed_edges()
        {
            _rem_edge_set.Clear();
        }

        public virtual void recover_removed_edge(Pair<int, int> edge)
        {
            _rem_edge_set.Remove(edge);
        }

        public virtual void recover_removed_vertices()
        {
            _rem_vertex_id_set.Clear();
        }

        public virtual void recover_removed_vertex(int? vertex_id)
        {
            _rem_vertex_id_set.Remove(vertex_id.GetValueOrDefault());
        }

        /// <summary>
        /// Return the weight associated with the input edge.
        /// </summary>
        /// <param name="source"> </param>
        /// <param name="sink">
        /// @return </param>
        public override double get_edge_weight(BaseVertex source, BaseVertex sink)
        {
            int source_id = source.get_id();
            int sink_id = sink.get_id();

            if (_rem_vertex_id_set.Contains(source_id) || _rem_vertex_id_set.Contains(sink_id) || _rem_edge_set.Contains(new Pair<int, int>(source_id, sink_id)))
            {
                return Graph.DISCONNECTED;
            }
            return base.get_edge_weight(source, sink);
        }

        /// <summary>
        /// Return the weight associated with the input edge.
        /// </summary>
        /// <param name="source"> </param>
        /// <param name="sink">
        /// @return </param>
        public virtual double get_edge_weight_of_graph(BaseVertex source, BaseVertex sink)
        {
            return base.get_edge_weight(source, sink);
        }

        /// <summary>
        /// Return the set of fan-outs of the input vertex.
        /// </summary>
        /// <param name="vertex">
        /// @return </param>
        public override HashSet<BaseVertex> get_adjacent_vertices(BaseVertex vertex)
        {
            var ret_set = new HashSet<BaseVertex>();
            int starting_vertex_id = vertex.get_id();
            if (!_rem_vertex_id_set.Contains(starting_vertex_id))
            {
                HashSet<BaseVertex> adj_vertex_set = base.get_adjacent_vertices(vertex);
                foreach (BaseVertex cur_vertex in adj_vertex_set)
                {
                    int ending_vertex_id = cur_vertex.get_id();
                    if (_rem_vertex_id_set.Contains(ending_vertex_id) || _rem_edge_set.Contains(new Pair<int, int>(starting_vertex_id, ending_vertex_id)))
                    {
                        continue;
                    }

                    // 
                    ret_set.Add(cur_vertex);
                }
            }
            return ret_set;
        }

        /// <summary>
        /// Get the set of vertices preceding the input vertex.
        /// </summary>
        /// <param name="vertex">
        /// @return </param>
        public override  HashSet<BaseVertex> get_precedent_vertices(BaseVertex vertex)
        {
            HashSet<BaseVertex> ret_set = new HashSet<BaseVertex>();
            if (!_rem_vertex_id_set.Contains(vertex.get_id()))
            {
                int ending_vertex_id = vertex.get_id();
                HashSet<BaseVertex> pre_vertex_set = base.get_precedent_vertices(vertex);
                foreach (BaseVertex cur_vertex in pre_vertex_set)
                {
                    int starting_vertex_id = cur_vertex.get_id();
                    if (_rem_vertex_id_set.Contains(starting_vertex_id) || _rem_edge_set.Contains(new Pair<int, int>(starting_vertex_id, ending_vertex_id)))
                    {
                        continue;
                    }

                    //
                    ret_set.Add(cur_vertex);
                }
            }
            return ret_set;
        }

        /// <summary>
        /// Get the list of vertices in the graph, except those removed.
        /// @return
        /// </summary>
        public override List<BaseVertex> get_vertex_list()
        {
            List<BaseVertex> ret_list = new List<BaseVertex>();
            foreach (BaseVertex cur_vertex in base.get_vertex_list())
            {
                if (_rem_vertex_id_set.Contains(cur_vertex.get_id()))
                {
                    continue;
                }
                ret_list.Add(cur_vertex);
            }
            return ret_list;
        }

        /// <summary>
        /// Get the vertex corresponding to the input 'id', if exist. 
        /// </summary>
        /// <param name="id">
        /// @return </param>
        public override BaseVertex get_vertex(int id)
        {
            if (_rem_vertex_id_set.Contains(id))
            {
                return null;
            }
            else
            {
                return base.get_vertex(id);
            }
        }
        /// <param name="args"> </param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the class VariableGraph!");

            VariableGraph graph = new VariableGraph("data/test_50");
            graph.remove_vertex(13);
            graph.remove_vertex(12);
            graph.remove_vertex(10);
            graph.remove_vertex(23);
            graph.remove_vertex(47);
            graph.remove_vertex(49);
            graph.remove_vertex(3);
            graph.remove_edge(new Pair<int, int>(26, 41));
            DijkstraShortestPathAlg alg = new DijkstraShortestPathAlg(graph);
            Console.WriteLine(alg.get_shortest_path(graph.get_vertex(0), graph.get_vertex(20)));
        }

    }
}
