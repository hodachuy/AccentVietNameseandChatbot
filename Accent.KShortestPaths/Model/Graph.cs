using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accent.KShortestPaths.Model.Abstracts;
using System.IO;
using System.Text.RegularExpressions;

namespace Accent.KShortestPaths.Model
{
    public class Graph : BaseGraph
    {
        public static readonly double DISCONNECTED = double.MaxValue;

        // index of fan-outs of one vertex
        protected internal Dictionary<int, HashSet<BaseVertex>> _fanout_vertices_index = new Dictionary<int, HashSet<BaseVertex>>();

        // index for fan-ins of one vertex
        protected internal Dictionary<int, HashSet<BaseVertex>> _fanin_vertices_index = new Dictionary<int, HashSet<BaseVertex>>();

        // index for edge weights in the graph
        protected internal Dictionary<Pair<int, int>, double> _vertex_pair_weight_index = new Dictionary<Pair<int, int>, double>();

        // index for vertices in the graph
        protected internal Dictionary<int, BaseVertex> _id_vertex_index = new Dictionary<int, BaseVertex>();

        // list of vertices in the graph 
        protected internal List<BaseVertex> _vertex_list = new List<BaseVertex>();

        // the number of vertices in the graph
        protected internal int _vertex_num = 0;

        // the number of arcs in the graph
        protected internal int _edge_num = 0;

        /// <summary>
        /// Constructor 1 </summary>
        /// <param name="data_file_name"> </param>
        //JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
        //ORIGINAL LINE: public Graph(final String data_file_name)
        public Graph(string data_file_name)
        {
            import_from_file(data_file_name);
        }

        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="graph"> </param>
        //JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
        //ORIGINAL LINE: public Graph(final Graph graph_)
        public Graph(Graph graph_)
        {
            _vertex_num = graph_._vertex_num;
            _edge_num = graph_._edge_num;
            //((List<BaseVertex>)_vertex_list).AddRange(graph_._vertex_list);
            foreach (var item in graph_._vertex_list)
            {
                ((List<BaseVertex>)_vertex_list).Add(item);
            }
            
            //JAVA TO C# CONVERTER TODO TASK: There is no .NET Dictionary equivalent to the Java 'putAll' method:               
            //_id_vertex_index =  graph_._id_vertex_index;
            foreach (var keyValuePair in _id_vertex_index)
            {
                _id_vertex_index[keyValuePair.Key] = keyValuePair.Value;
                //_id_vertex_index.Add(keyValuePair.Key, keyValuePair.Value);
            }
            //JAVA TO C# CONVERTER TODO TASK: There is no .NET Dictionary equivalent to the Java 'putAll' method:
            //_fanin_vertices_index = graph_._fanin_vertices_index;
            foreach (var keyValuePair in graph_._fanin_vertices_index)
            {
                _fanin_vertices_index[keyValuePair.Key] = keyValuePair.Value;
                //_fanin_vertices_index.Add(keyValuePair.Key, keyValuePair.Value);
            }
            //JAVA TO C# CONVERTER TODO TASK: There is no .NET Dictionary equivalent to the Java 'putAll' method:
            //_fanout_vertices_index = graph_._fanout_vertices_index;
            foreach (var keyValuePair in graph_._fanout_vertices_index)
            {
                _fanout_vertices_index[keyValuePair.Key] = keyValuePair.Value;
                //_fanout_vertices_index.Add(keyValuePair.Key, keyValuePair.Value);
            }
            //JAVA TO C# CONVERTER TODO TASK: There is no .NET Dictionary equivalent to the Java 'putAll' method:
            //_vertex_pair_weight_index = graph_._vertex_pair_weight_index;
            foreach (var keyValuePair in graph_._vertex_pair_weight_index)
            {
                _vertex_pair_weight_index[keyValuePair.Key] = keyValuePair.Value;
                //_vertex_pair_weight_index.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        /// <summary>
        /// Default constructor 
        /// </summary>
        public Graph()
        {
        }

        /// <summary>
        /// Clear members of the graph.
        /// </summary>
        public virtual void clear()
        {
            Vertex.reset();
            _vertex_num = 0;
            _edge_num = 0;
            _vertex_list.Clear();
            _id_vertex_index.Clear();
            _fanin_vertices_index.Clear();
            _fanout_vertices_index.Clear();
            _vertex_pair_weight_index.Clear();
        }

        /// <summary>
        /// There is a requirement for the input graph. 
        /// The ids of vertices must be consecutive. 
        /// </summary>
        /// <param name="data_file_name"> </param>
        //JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
        //ORIGINAL LINE: public void import_from_file(final String data_file_name)
        public virtual void import_from_file(string data_file_name)
        {
            // 0. Clear the variables 
            clear();

            try
            {
                // 1. read the file and put the content in the buffer
                //StreamReader input = new StreamReader(data_file_name);
                //FileStream fos = new FileStream(data_file_name, FileMode.Create, FileAccess.Write);
                //var bufRead = new StreamWriter(fos, Encoding.UTF8);
                //StreamReader bufRead = new StreamReader(input);

                StreamReader bufRead = File.OpenText(data_file_name);              
               
                bool is_first_line = true;
                string line; // String that holds current file line

                // 2. Read first line
                line = bufRead.ReadLine();
                while (!string.ReferenceEquals(line, null))
                {
                    // 2.1 skip the empty line
                    if (line.Trim().Equals(""))
                    {
                        line = bufRead.ReadLine();
                        continue;
                    }

                    // 2.2 generate nodes and edges for the graph
                    if (is_first_line)
                    {
                        //2.2.1 obtain the number of nodes in the graph 

                        is_first_line = false;
                        initGraph(int.Parse(line.Trim()));

                    }
                    else
                    {
                        //2.2.2 find a new edge and put it in the graph  
                        string[] str_list = Regex.Split(line.Trim(), @"\\s"); 

                        int start_vertex_id = int.Parse(str_list[0]);
                        int end_vertex_id = int.Parse(str_list[1]);
                        double weight = double.Parse(str_list[2]);
                        add_edge(start_vertex_id, end_vertex_id, weight);
                    }
                    //
                    line = bufRead.ReadLine();
                }
                bufRead.Close();

            }
            catch (IOException e)
            {
                // If another exception is generated, print a stack trace
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }

        public virtual void initGraph(int vertexNum)
        {
            clear();
            _vertex_num = vertexNum;
            for (int i = 0; i < _vertex_num; ++i)
            {
                BaseVertex vertex = new Vertex();
                _vertex_list.Add(vertex);
                //_id_vertex_index.Add(vertex.get_id(), vertex);
                _id_vertex_index[vertex.get_id()] = vertex;
            }
        }
        /// <summary>
        /// Note that this may not be used externally, because some other members in the class
        /// should be updated at the same time. 
        /// </summary>
        /// <param name="start_vertex_id"> </param>
        /// <param name="end_vertex_id"> </param>
        /// <param name="weight"> </param>
        public virtual void add_edge(int start_vertex_id, int end_vertex_id, double weight)
        {
            // actually, we should make sure all vertices ids must be correct. 
            if (!_id_vertex_index.ContainsKey(start_vertex_id) || !_id_vertex_index.ContainsKey(end_vertex_id) || start_vertex_id == end_vertex_id)
            {
                throw new System.ArgumentException("The edge from " + start_vertex_id + " to " + end_vertex_id + " does not exist in the graph.");
            }

            // update the adjacent-list of the graph
            HashSet<BaseVertex> fanout_vertex_set = new HashSet<BaseVertex>();
            if (_fanout_vertices_index.ContainsKey(start_vertex_id))
            {
                fanout_vertex_set = _fanout_vertices_index[start_vertex_id];
            }
            fanout_vertex_set.Add(_id_vertex_index[end_vertex_id]);
            _fanout_vertices_index[start_vertex_id] = fanout_vertex_set;
            //_fanout_vertices_index.Add(start_vertex_id, fanout_vertex_set);

            //
            HashSet<BaseVertex> fanin_vertex_set = new HashSet<BaseVertex>();
            if (_fanin_vertices_index.ContainsKey(end_vertex_id))
            {
                fanin_vertex_set = _fanin_vertices_index[end_vertex_id];
            }
            fanin_vertex_set.Add(_id_vertex_index[start_vertex_id]);
            //_fanin_vertices_index.Add(end_vertex_id, fanin_vertex_set);
            _fanin_vertices_index[end_vertex_id] = fanin_vertex_set;

            // store the new edge 
            _vertex_pair_weight_index[new Pair<int, int>(start_vertex_id, end_vertex_id)] = weight;
            //_vertex_pair_weight_index.Add(new Pair<int, int>(start_vertex_id, end_vertex_id),weight);

           ++ _edge_num;
        }

        //JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
        //ORIGINAL LINE: public void export_to_file(final String file_name)
        public virtual void export_to_file(string file_name)
        {
            //1. prepare the text to export
            StringBuilder sb = new StringBuilder();
            sb.Append(_vertex_num + "\n\n");
            foreach (Pair<int, int> cur_edge_pair in _vertex_pair_weight_index.Keys)
            {
                int starting_pt_id = cur_edge_pair.first();
                int ending_pt_id = cur_edge_pair.second();
                double weight = _vertex_pair_weight_index[cur_edge_pair];
                sb.Append(starting_pt_id + "	" + ending_pt_id + "	" + weight + "\n");
            }
            //2. open the file and put the data into the file. 
            StreamWriter output = null;
            try
            {
                // use buffering
                // FileWriter always assumes default encoding is OK!
                //output = new StreamWriter(new File(file_name));
                //output.write(sb.ToString());
                FileStream fos = new FileStream(file_name, FileMode.Create, FileAccess.Write);
                output = new StreamWriter(fos, Encoding.UTF8);
                output.Write(sb.ToString());
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            finally
            {
                // flush and close both "output" and its underlying FileWriter
                try
                {
                    if (output != null)
                    {
                        output.Close();
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }
        }

        /* (non-Javadoc)
         * @see edu.asu.emit.qyan.alg.model.abstracts.BaseGraph#get_adjacent_vertices(edu.asu.emit.qyan.alg.model.abstracts.BaseVertex)
         */
        public virtual HashSet<BaseVertex> get_adjacent_vertices(BaseVertex vertex)
        {
            return _fanout_vertices_index.ContainsKey(vertex.get_id()) ? _fanout_vertices_index[vertex.get_id()] : new HashSet<BaseVertex>();
        }

        /* (non-Javadoc)
         * @see edu.asu.emit.qyan.alg.model.abstracts.BaseGraph#get_precedent_vertices(edu.asu.emit.qyan.alg.model.abstracts.BaseVertex)
         */
        public virtual HashSet<BaseVertex> get_precedent_vertices(BaseVertex vertex)
        {
            return _fanin_vertices_index.ContainsKey(vertex.get_id()) ? _fanin_vertices_index[vertex.get_id()] : new HashSet<BaseVertex>();
        }

        /* (non-Javadoc)
         * @see edu.asu.emit.qyan.alg.model.abstracts.BaseGraph#get_edge_weight(edu.asu.emit.qyan.alg.model.abstracts.BaseVertex, edu.asu.emit.qyan.alg.model.abstracts.BaseVertex)
         */
        public virtual double get_edge_weight(BaseVertex source, BaseVertex sink)
        {
            return _vertex_pair_weight_index.ContainsKey(new Pair<int, int>(source.get_id(), sink.get_id())) ? _vertex_pair_weight_index[new Pair<int, int>(source.get_id(), sink.get_id())] : DISCONNECTED;
        }

        /// <summary>
        /// Set the number of vertices in the graph </summary>
        /// <param name="num"> </param>
        public virtual void set_vertex_num(int num)
        {
            _vertex_num = num;
        }

        /// <summary>
        /// Return the vertex list in the graph.
        /// </summary>
        public virtual List<BaseVertex> get_vertex_list()
        {
            return _vertex_list;
        }

        /// <summary>
        /// Get the vertex with the input id.
        /// </summary>
        /// <param name="id">
        /// @return </param>
        public virtual BaseVertex get_vertex(int id)
        {
            return _id_vertex_index[id];
        }
    }
}
