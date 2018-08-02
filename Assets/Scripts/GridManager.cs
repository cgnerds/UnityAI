using System.Collections;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField]
    private int numberOfRows = 20;
    [SerializeField]
    public int numberOfColums = 20;
    [SerializeField]
    public float gridCellSize = 2;
    [SerializeField]
    public bool showGrid = true;
    [SerializeField]
    public bool showObstacleBlocks = true;

    private Vector3 origin = new Vector3();
    private GameObject[] obstacleList;
    private Node[,] nodes { get; set; }

    public Vector3 Origin { get { return origin; } }

    private void Awake()
    {
        obstacleList = GameObject.FindGameObjectsWithTag("Obstacle");
        InitializeNodes();
        CalculateObstacles();
    }

    private void InitializeNodes()
    {
        nodes = new Node[numberOfColums, numberOfRows];

        int index = 0;
        for(int i = 0; i < numberOfColums; i++)
        {
            for(int j = 0; j < numberOfRows; j++)
            {
                Vector3 cellPosition = GetGridCellCenter(index);
                Node node = new Node(cellPosition);
                nodes[i, j] = node;
                index++;
            }
        }
    }

    private void CalculateObstacles()
    {
        if(obstacleList != null && obstacleList.Length > 0)
        {
            foreach(GameObject data in obstacleList)
            {
                int indexCell = GetGridIndex(data.transform.position);
                int column = GetColumnOfIndex(indexCell);
                int row = GetRowOfIndex(indexCell);

                nodes[row, column].MarkAsObstacle();
            }
        }
    }

    private Vector3 GetGridCellCenter(int index)
    {
        Vector3 cellPosition = GetGridCellPositionAtIndex(index);
        cellPosition.x += (gridCellSize / 2.0f);
        cellPosition.z += (gridCellSize / 2.0f);

        return cellPosition;
    }

    private Vector3 GetGridCellPositionAtIndex(int index)
    {
        int row = GetRowOfIndex(index);
        int col = GetColumnOfIndex(index);
        float xPositionInGrid = col * gridCellSize;
        float zPositionInGrid = row * gridCellSize;

        return Origin + new Vector3(xPositionInGrid, 0.0f, zPositionInGrid);
    }

    public int GetGridIndex(Vector3 position)
    {
        if(!IsInBounds(position))
        {
            return -1;
        }

        position -= Origin;

        int column = (int)(position.x / gridCellSize);
        int row = (int)(position.z / gridCellSize);

        return (row * numberOfColums + column);
    }

    private int GetRowOfIndex(int index)
    {
        int row = index / numberOfColums;
        return row;
    }

    private int GetColumnOfIndex(int index)
    {
        int col = index % numberOfColums;
        return col;
    }

    private bool IsInBounds(Vector3 position)
    {
        float width = numberOfColums * gridCellSize;
        float height = numberOfRows * gridCellSize;

        return (position.x >= Origin.x && position.x <= Origin.x + width && position.z <= Origin.z + height && position.z >= Origin.z);
    }

    public void GetNeighbors(Node node, ArrayList neighbors)
    {
        Vector3 neighborPosition = node.position;
        int neighborIndex = GetGridIndex(neighborPosition);

        int row = GetRowOfIndex(neighborIndex);
        int column = GetColumnOfIndex(neighborIndex);

        // Bottom
        int leftNodeRow = row - 1;
        int leftNodeColumn = column;
        AssignNeighbor(leftNodeRow, leftNodeColumn, neighbors);

        // Top
        leftNodeRow = row + 1;
        leftNodeColumn = column;
        AssignNeighbor(leftNodeRow, leftNodeColumn, neighbors);

        // Right 
        leftNodeRow = row;
        leftNodeColumn = column + 1;
        AssignNeighbor(leftNodeRow, leftNodeColumn, neighbors);

        // Left
        leftNodeRow = row;
        leftNodeColumn = column - 1;
        AssignNeighbor(leftNodeRow, leftNodeColumn, neighbors);
    }

    // Check the neighbor. If it's not an obstacle, assign the neighbor.
    private void AssignNeighbor(int row, int column, ArrayList neighbors)
    {
        if(row != -1 && column != -1 && row < numberOfRows && column < numberOfColums)
        {
            Node nodeToAdd = nodes[row, column];
            if(!nodeToAdd.bObstacle)
            {
                neighbors.Add(nodeToAdd);
            }
        }
    }
}
