using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; // for randomness
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{

    [SerializeField]
    private MazeCell _mazeCell;

    [SerializeField]
    [Tooltip("Width of the maze to generate.")] public int mazeWidth;

    [SerializeField]
    [Tooltip("Length of the maze to generate.")] public int mazeDepth;

    public MazeCell[,] mazeGrid;

    [SerializeField]
    [Tooltip("Potion prefab(s) go here")] public GameObject[] Potions;

    private int maxPotions;

    public void Start() {
       mazeGrid = new MazeCell[mazeWidth, mazeDepth];
       maxPotions = mazeWidth * mazeDepth / 15;
       int numPotions = 0;


       System.Random random = new System.Random();
       int type = 0; // this will rotate the type of potion, so equal numbers of each are generated.
       // by order, there will be more speed or teleport than guide potions available most runs

       for (int i = 0; i < mazeWidth; i++) {
        for (int j = 0; j < mazeDepth; j++) {
            
            // random-offset to avoid walls flickering from overlaps
            var randomOffset = 0; // UnityEngine.Random.Range(0.002f, 0.006f);
            mazeGrid[i, j] = Instantiate(_mazeCell, new Vector3(i + randomOffset, 0, j + randomOffset), Quaternion.identity);
            // --------------------------------------------------------------------------------------------- //
            // places a potion in a cell at random, up to the max number of potions allowed in the maze
            if (numPotions < maxPotions && random.Next(15) == 3) {
                GameObject potionType = Potions[type];
                GameObject p = Instantiate(potionType, new Vector3(i, 0.5f, j), Quaternion.identity);
                p.gameObject.SetActive(true);
                numPotions++;
                type++;
                if (type >= Potions.Length) type = 0;
            }            
        }
       }

       GenerateMaze();
       mazeGrid[0, 0].ClearBackWall();
       mazeGrid[mazeWidth - 1, mazeDepth -1].ClearFrontWall();

       foreach (MazeCell cell in mazeGrid) {
        if (cell.isClosed()) cell.Close(); 
       }
    }


    //private void GenerateMaze() {
    private IEnumerator GenerateMaze() {
       var randX = new System.Random();
       var randY = new System.Random();

       var cells = new List<MazeCell>();
       int x = randX.Next(mazeWidth);
       int y = randY.Next(mazeDepth);
       cells.Add(mazeGrid[x, y]);
       var len = 0;

       // algo:
       // 1 - pick a random cell, add it to "stack" (list)
       // 2 - pick any unvisited neighbor of cell on top of stack, add it to path/stack
       // 3 - if cell has no unvisited neighbors, pop from stack, repeat from step 2.
       // 4 - else, continue
       // repeat until stack is empty

       while (len >= 0) {
            var currentCell = cells.ElementAt(len);
            currentCell.Visit();
            MazeCell nextCell = findUnvisitedNeighbor(currentCell);
            if (!nextCell) {
                cells.RemoveAt(len);
                len--;
                continue;
            }
            cells.Add(nextCell);
            len++;
            clearWalls(currentCell, nextCell);

            // adds each cell to the other's neighbor list, will come in handy for solving the maze.
            currentCell.neighbors.Add(nextCell);
            nextCell.neighbors.Add(currentCell);
            yield return WaitForSeconds(0.05f);
       }

    }


    private MazeCell findUnvisitedNeighbor(MazeCell current) {
        var unvisitedCells = getUnvisited(current);
        return unvisitedCells.OrderBy(_ => UnityEngine.Random.Range(1, 10)).FirstOrDefault();

    }
    
    public IEnumerable<MazeCell> getUnvisited(MazeCell current) {
        int x = (int)current.transform.position.x;
        int z = (int)current.transform.position.z;

        if (x + 1 < mazeWidth) {
            var rightCell = mazeGrid[x + 1, z];
            if (!rightCell.IsVisited) yield return rightCell;
        }

        if (x - 1 >= 0) {
            var leftCell = mazeGrid[x - 1, z];
            if (!leftCell.IsVisited) yield return leftCell;
        }

        if (z + 1 < mazeDepth) {
            var cellBelow = mazeGrid[x, z + 1];
            if (!cellBelow.IsVisited) yield return cellBelow;
        }

        if (z - 1 >= 0) {
            var cellAbove = mazeGrid[x, z - 1];
            if (!cellAbove.IsVisited) yield return cellAbove;
        }
    }

    private void clearWalls(MazeCell prev, MazeCell curr) {
        if (prev == null) {
            return;
        }
        
        float maxoffset = 0.01f;
        if (prev.transform.position.x + maxoffset < curr.transform.position.x) {
            // we went from right to left, so we clear right wall of prev, and left wall of "curr"ent cell
            prev.ClearRightWall();
            curr.ClearLeftWall();
            return;
        }

        if (prev.transform.position.x > curr.transform.position.x + maxoffset) {
            prev.ClearLeftWall();
            curr.ClearRightWall();
            return;
        }

        if (prev.transform.position.z < curr.transform.position.z) {
            prev.ClearFrontWall();
            curr.ClearBackWall();
            return;
        }

        if (prev.transform.position.z > curr.transform.position.z) {
            prev.ClearBackWall();
            curr.ClearFrontWall();
            return;
        }

    }

    Vector3 getStart() {
        return mazeGrid[mazeWidth - 1, mazeDepth -1].transform.position;
    }

    
}
