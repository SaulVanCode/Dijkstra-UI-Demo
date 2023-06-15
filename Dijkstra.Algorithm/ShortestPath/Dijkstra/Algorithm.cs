namespace Dijkstra.Algorithm.ShortestPath.Dijkstra
{
    internal static class Algorithm
    {
        private const double INFINITE = double.MaxValue;

        internal static ShortestPathResult<T> GetShortestPath<T>(Graph<T> graph, T from, T to, CostDelegate<T> costFunction)
        {
            if (!(graph.Contains(from) && graph.Contains(to)))
            {
                throw new InvalidOperationException("Either source node or destination node are not present in graph");
            }

            var path = new Dictionary<T, T>();
            var costs = new Dictionary<T, double> { [from] = 0 };
            var q = new SortedSet<T>(new[] { from }, new NodeComparer<T>(costs));

            double CurrentCost(T key)
            {
                return costs.ContainsKey(key) ? costs[key] : INFINITE;
            }

            void Relax(T u, T v, CostDelegate<T> w)
            {
                var currentCost = CurrentCost(v);
                var possibleNewCost = CurrentCost(u) + w(u, v);
                if (currentCost > possibleNewCost)
                {
                    q.Remove(v);
                    costs[v] = possibleNewCost;
                    path[v] = u;
                    q.Add(v);
                }
            }

            do
            {
                T u = q.PopMin();

                if (Comparer<T>.Default.Compare(u, to) == 0)
                {
                    return new ShortestPathResult<T>(from, to, costs[u], path);
                }

                foreach (var v in graph[u])
                {
                    Relax(u, v, costFunction);
                }
            } while (q.Any());

            return new ShortestPathResult<T>(from, to);
        }
    }

    static internal class SortedSetExtensions
    {
        public static T PopMin<T>(this SortedSet<T> set)
        {
            T item = set.Min;
            set.Remove(item);
            return item;
        }
    }

    public delegate double CostDelegate<in T>(T from, T to);
}
