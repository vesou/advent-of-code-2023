namespace AdventOfCode2023.Day17.part2;

public class DijkstraAlgorithm2
{
    static int[] dx = { -1, 0, 1, 0 };
    static int[] dy = { 0, 1, 0, -1 };
    public const int Left  = 0;
    public const int Down  = 1;
    public const int Right  = 2;
    public const int Up  = 3;

    public static int Dijkstra(int[,] grid, int startX, int startY, int endX, int endY)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        int[, , ,] distance = new int[rows, cols, 4, 11];
        bool[, , ,] visited = new bool[rows, cols, 4, 11];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    for (int l = 0; l < 11; l++)
                    {
                        distance[i, j, k, l] = int.MaxValue;
                        visited[i, j, k, l] = false;
                    }
                }
            }
        }

        distance[startY, startX, Right, 0] = 0;


        PriorityQueue<Node> pq = new PriorityQueue<Node>();
        pq.Enqueue(new Node(startX, startY, Right, 0, 0));

        while (pq.Count > 0)
        {
            Node current = pq.Dequeue();

            if (visited[current.Y, current.X, current.Direction, current.StepsSinceLastTurn])
                continue;

            if (current.StepsSinceLastTurn > 10)
            {
                continue;
            }

            visited[current.Y, current.X, current.Direction, current.StepsSinceLastTurn] = true;

            for (int i = 0; i < 4; i++)
            {
                if(i == (current.Direction +2 )%4)
                    continue;
                int newX = current.X + dx[i];
                int newY = current.Y + dy[i];
                bool hasDirectionChanged = i != current.Direction;
                int stepsSinceLastTurn =  hasDirectionChanged ? 1 : current.StepsSinceLastTurn + 1;
                if(stepsSinceLastTurn > 10)
                    continue;
                if (hasDirectionChanged && current.StepsSinceLastTurn < 4)
                    continue;

                if (newX >= 0 && newX < cols && newY >= 0 && newY < rows && !visited[newY, newX, i, stepsSinceLastTurn])
                {
                    int newDistance = distance[current.Y, current.X, current.Direction, current.StepsSinceLastTurn] + grid[newY, newX];

                    if (newDistance < distance[newY, newX, i, stepsSinceLastTurn])
                    {
                        distance[newY, newX, i, stepsSinceLastTurn] = newDistance;
                        pq.Enqueue(new Node(newX, newY, i, newDistance, stepsSinceLastTurn));
                    }
                }
            }
        }

        int shortestDistance = int.MaxValue;
        for (int k = 0; k < 4; k++)
        {
            for (int i = 4; i < 11; i++)
            {
                shortestDistance = Math.Min(shortestDistance, distance[endY, endX, k, i]);
            }
        }

        return shortestDistance;
    }
}

class Node : IComparable<Node>
{
    public int X { get; }
    public int Y { get; }
    public int Direction { get; }
    public int StepsSinceLastTurn { get; }
    public int Cost { get; }

    public Node(int x, int y, int direction, int cost, int stepsSinceLastTurn)
    {
        X = x;
        Y = y;
        Direction = direction;
        Cost = cost;
        StepsSinceLastTurn = stepsSinceLastTurn;
    }

    public int CompareTo(Node other)
    {
        return Cost.CompareTo(other.Cost);
    }
}

class PriorityQueue<T> where T : IComparable<T>
{
    private readonly List<T> _heap = new();

    public int Count => _heap.Count;

    public void Enqueue(T item)
    {
        _heap.Add(item);
        int i = _heap.Count - 1;
        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (_heap[i].CompareTo(_heap[parent]) >= 0)
                break;

            Swap(i, parent);
            i = parent;
        }
    }

    public T Dequeue()
    {
        int lastIndex = _heap.Count - 1;
        T frontItem = _heap[0];
        _heap[0] = _heap[lastIndex];
        _heap.RemoveAt(lastIndex);

        lastIndex--;

        int parent = 0;
        while (true)
        {
            int leftChild = parent * 2 + 1;
            if (leftChild > lastIndex)
                break;

            int rightChild = leftChild + 1;
            int minChild = leftChild;

            if (rightChild <= lastIndex && _heap[rightChild].CompareTo(_heap[leftChild]) < 0)
                minChild = rightChild;

            if (_heap[parent].CompareTo(_heap[minChild]) <= 0)
                break;

            Swap(parent, minChild);
            parent = minChild;
        }

        return frontItem;
    }

    private void Swap(int i, int j)
    {
        (_heap[i], _heap[j]) = (_heap[j], _heap[i]);
    }
}

