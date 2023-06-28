using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BFS(너비 우선 탐색) 알고리즘을 통해 목적지를 향한 경로 찾기 스크립트
public class PathFinding : MonoBehaviour
{
    [SerializeField] Vector2Int mStartPos;
    public Vector2Int startPos
    {
        get
        {
            return mStartPos;
        }
    }
    [SerializeField] Vector2Int mEndPos;
    public Vector2Int endPos
    {
        get
        {
            return mEndPos;
        }
    }

    GridManager gridManager;
    Node startNode;
    Node destinationNode;
    Node currentSearchNode;


    Vector2Int[] direction = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> visited = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.grid;
            startNode = grid[startPos];
            destinationNode = grid[endPos];

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath() // 시작지점으로부터 출발 -> 경로탐색
    {
        return GetNewPath(startPos);
    }

    public List<Node> GetNewPath(Vector2Int coordinates) // coordinates로부터 출발 -> 경로 탐색
    {
        gridManager.ResetNodes();
        BFS(coordinates);
        return BuildPath();
    }


    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        for (int i = 0; i < direction.Length; i++)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction[i];

            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (!visited.ContainsKey(neighbors[i].coordinates) && neighbors[i].isWalkable)
            {
                neighbors[i].connectedTo = currentSearchNode;
                visited.Add(neighbors[i].coordinates, neighbors[i]);
                frontier.Enqueue(neighbors[i]);

            }
        }
    }

    void BFS(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;
        frontier.Clear();
        visited.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        visited.Add(coordinates, grid[coordinates]);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();

            if (currentSearchNode.coordinates == endPos)
            {
                isRunning = false;
            }

        }
    }

    List<Node> BuildPath()
    { // 목적지 -> 출발점으로 역추적하는 함수
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPathed = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPathed = true;
        }

        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }

        }

        return false;
    }

    public void NotifiyReceiver()
    {
        BroadcastMessage("RecalculatePath", false,SendMessageOptions.DontRequireReceiver);
    }

}
