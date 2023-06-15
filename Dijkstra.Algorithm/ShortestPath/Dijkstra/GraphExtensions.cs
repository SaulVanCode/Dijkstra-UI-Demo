namespace Dijkstra.Algorithm.ShortestPath.Dijkstra
{
    public static class GraphExtensions
    {
        public static ShortestPathResult<T> GetShortestPath<T>(this Graph<T> graph, T from, T to, CostDelegate<T> costFunction)
        {
            return Algorithm.GetShortestPath(graph, from, to, costFunction);
        }
    }
}
