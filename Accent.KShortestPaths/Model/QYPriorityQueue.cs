using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accent.KShortestPaths.Model.Abstracts;

namespace Accent.KShortestPaths.Model
{
    /// <summary>
    /// @author HuyHo
    /// @version $Revision: 673 $
    /// @latest $Id: QYPriorityQueue.java 673 2009-02-05 08:19:18Z qyan $
    /// </summary>
    public class QYPriorityQueue<E> where E : BaseElementWithWeight
    {
        internal List<E> _element_weight_pair_list = new List<E>();
        internal int _limit_size = -1;
        internal bool _is_incremental = false;

        /// <summary>
        /// Default constructor. 
        /// </summary>
        public QYPriorityQueue()
        {
        }

        /// <summary>
        /// Constructor. </summary>
        /// <param name="limit_size"> </param>
        public QYPriorityQueue(int limit_size, bool is_incremental)
        {
            _limit_size = limit_size;
            _is_incremental = is_incremental;
        }

        public override string ToString()
        {
            return _element_weight_pair_list.ToString();
        }

        /// <summary>
        /// Binary search is exploited to find the right position 
        /// of the new element. </summary>
        /// <param name="weight"> </param>
        /// <returns> the position of the new element </returns>
        private int _bin_locate_pos(double weight, bool is_incremental)
        {
            int mid = 0;
            int low = 0;
            int high = _element_weight_pair_list.Count - 1;
            //
            while (low <= high)
            {
                mid = (low + high) / 2;
                if (_element_weight_pair_list.ElementAt(mid).get_weight() == weight)
                {
                    return mid + 1;
                }

                if (is_incremental)
                {
                    if (_element_weight_pair_list.ElementAt(mid).get_weight() < weight)
                    {
                        high = mid - 1;
                    }
                    else
                    {
                        low = mid + 1;
                    }
                }
                else
                {
                    if (_element_weight_pair_list.ElementAt(mid).get_weight() > weight)
                    {
                        high = mid - 1;
                    }
                    else
                    {
                        low = mid + 1;
                    }
                }
            }
            return low;
        }

        /// <summary>
        /// Add a new element in the queue. </summary>
        /// <param name="element"> </param>
        public virtual void add(E element)
        {

            //_element_weight_pair_list.ElementAt(_bin_locate_pos(element.get_weight(), _is_incremental)).A(element);
            int index = _bin_locate_pos(element.get_weight(), _is_incremental);
            
            _element_weight_pair_list.Add(element);
            _element_weight_pair_list[index] = element;

            if (_limit_size > 0 && _element_weight_pair_list.Count > _limit_size)
            {
                int size_of_results = _element_weight_pair_list.Count;
                _element_weight_pair_list.RemoveAt(size_of_results - 1);
            }
        }

        /// <summary>
        /// It only reflects the size of the current results.
        /// @return
        /// </summary>
        public virtual int size()
        {
            return _element_weight_pair_list.Count;
        }

        /// <summary>
        /// Get the i th element. </summary>
        /// <param name="i">
        /// @return </param>
        public virtual E get(int i)
        {
            if (i >= _element_weight_pair_list.Count)
            {
                Console.Error.WriteLine("The result :" + i + " doesn't exist!!!");
            }
            return _element_weight_pair_list[i];
        }

        /// <summary>
        /// Get the first element, and then remove it from the queue. 
        /// @return
        /// </summary>
        public virtual E poll()
        {
            E ret = _element_weight_pair_list[0];
            _element_weight_pair_list.RemoveAt(0);
            return ret;
        }

        /// <summary>
        /// Check if it's empty.
        /// @return
        /// </summary>
        public virtual bool Empty
        {
            get
            {
                return _element_weight_pair_list.Count == 0 ? true : false ;
            }
        }

    }
}
