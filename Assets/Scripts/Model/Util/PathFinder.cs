using System.Collections.Generic;
using System.Linq;
using World;

namespace Util
{
    public class PathFinder
    {
        public static Pair<List<Node>, int> ShortestPath(WorldGraph g, Node source, Node target)
        {
            var nodeQueue = new Queue<Node>();
            var known = new HashSet<Node>();
            var distances = new Dictionary<Node, int>();
            var paths = new Dictionary<Node, List<Node>>();

            nodeQueue.Enqueue(source);
            distances[source] = 0;
            paths[source] = new List<Node> {source};

            while (nodeQueue.Count > 0)
            {
                var current = nodeQueue.Dequeue();
                known.Add(current);
                if (current == target)
                {
                    break;
                }

                var path = paths[current];
                var distance = distances[current];

                var next = g.GetNeighborsOf(current).Where(n => !known.Contains(n));
                foreach (var node in next)
                {
                    var newDistance = distance + 1;
                    var newPath = path.ToList();
                    newPath.Add(node);
                    if (!distances.ContainsKey(node) || newDistance < distances[node])
                    {
                        distances[node] = newDistance;
                        paths[node] = newPath;
                    }
                    nodeQueue.Enqueue(node);
                }
            }

            if (paths.ContainsKey(target))
            {
                return new Pair<List<Node>, int>(paths[target], distances[target]);
            }
            return null;
        }

        public static List<Node> FindPath(WorldGraph g, Node source, Node target)
        {
            var shortestPath = ShortestPath(g, source, target);
            if (shortestPath == null)
            {
                return null;
            }
            return shortestPath.left;
        }

        public static int FindDistance(WorldGraph g, Node source, Node target)
        {
            var shortestPath = ShortestPath(g, source, target);
            if (shortestPath == null)
            {
                return -1;
            }
            return shortestPath.right;
        }
    }
}
