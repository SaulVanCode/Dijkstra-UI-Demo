namespace Dijkstra.Algorithm.ShortestPath
{
    public class ShortestPathResult<T>
    {
        private readonly IEnumerable<T> _path;
        private readonly T _from;
        private readonly T _to;
        private readonly double _distance;

        internal ShortestPathResult(T from, T to) : this(from, to, double.MaxValue, null) { }

        internal ShortestPathResult(T from, T to, double distance, IDictionary<T, T> pathDict)
        {
            _from = from;
            _to = to;
            _distance = distance;
            _path = GetBackwardsPath(pathDict).Reverse().ToList();
        }

        public bool FoundPath => _path != null;

        public double Distance => _distance;

        public T FromNode => _from;

        public T ToNode => _to;

        public IReadOnlyList<T> Path => (IReadOnlyList<T>)_path;

        private IEnumerable<T> GetBackwardsPath(IDictionary<T, T> pathDict)
        {
            if (pathDict == null)
            {
                yield break;
            }

            T result = ToNode;

            do
            {
                yield return result;
                result = pathDict[result];
            } while (Comparer<T>.Default.Compare(result, FromNode) != 0);

            yield return FromNode;
        }
    }
}