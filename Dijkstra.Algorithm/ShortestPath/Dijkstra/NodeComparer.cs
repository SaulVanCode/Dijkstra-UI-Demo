namespace Dijkstra.Algorithm.ShortestPath.Dijkstra
{
    internal class NodeComparer<T> : IComparer<T>
    {
        private readonly IDictionary<T, double> _costs;

        public NodeComparer(IDictionary<T, double> costs)
        {
            _costs = costs;
        }

        public int Compare(T x, T y)
        {
            double xCost = _costs.ContainsKey(x) ? _costs[x] : double.MaxValue;
            double yCost = _costs.ContainsKey(y) ? _costs[y] : double.MaxValue;

            if (xCost.CompareTo(yCost) == 0)
            {
                return Comparer<T>.Default.Compare(x, y);
            }

            return xCost.CompareTo(yCost);
        }
    }
}
