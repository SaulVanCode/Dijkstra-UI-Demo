using System.Collections;
using System.Collections.Immutable;

namespace Dijkstra.Algorithm
{
    public class Graph<T> : ICollection<T>
    {
        private readonly IDictionary<T, HashSet<T>> _nodes = new Dictionary<T, HashSet<T>>();
        private int _edgeCount = 0;
        public int EdgeCount => _edgeCount;
        public int Count => _nodes.Count;
        public bool IsReadOnly => true;
        public IReadOnlyDictionary<T, ImmutableHashSet<T>> AdjacencyList => _nodes
            .ToDictionary(pair => pair.Key, pair => pair.Value.ToImmutableHashSet());

        public Graph() { }

        public Graph(List<T> nodes, List<(T, T)> edges)
        {
            CreateNodes(nodes);
            CreateEdges(edges);
        }

        private void CreateNodes(List<T> nodes) => nodes.ForEach(n => Add(n));

        private void CreateEdges(List<(T from, T to)> edges) => edges.ForEach(e => Connect(e.from, e.to));

        public bool Connect(T from, T to)
        {
            if (!(_nodes.ContainsKey(from) && _nodes.ContainsKey(to)))
            {
                throw new InvalidOperationException($"Source {from} or destination {to} not found");
            }

            _nodes[from].Add(to);
            _edgeCount++;
            return true;
        }

        public HashSet<T> this[T node] => _nodes.ContainsKey(node) ? _nodes[node] : null;

        public void Add(T item)
        {
            if (_nodes.ContainsKey(item))
            {
                throw new InvalidOperationException($"Duplicated node {item}");
            }

            _nodes.Add(item, new HashSet<T>());
        }

        public void Clear() => _nodes.Clear();

        public bool Contains(T item) => _nodes.ContainsKey(item);

        public void CopyTo(T[] array, int arrayIndex) => _nodes.Keys.CopyTo(array, arrayIndex);

        public bool Remove(T item)
        {
            _nodes.Keys.ToList().ForEach(n =>
            {
                if (_nodes[n].Contains(n))
                {
                    _nodes[n].Remove(item);
                    _edgeCount--;
                }
            });

            return _nodes.Remove(item);
        }

        public IEnumerator GetEnumerator() => _nodes.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => _nodes.Keys.GetEnumerator();

        public static T operator +(Graph<T> graph, T node)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            graph.Add(node);
            return node;
        }

        public static bool operator +(Graph<T> graph, (T from, T to) edge)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            return graph.Connect(edge.from, edge.to);
        }
    }
}
