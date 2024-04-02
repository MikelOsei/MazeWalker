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
    private int mazeWidth;

    [SerializeField]
    private int mazeDepth;

    private MazeCell[,] mazeGrid;
    //private Potion[] potions;

    [SerializeField]
    public GameObject[] Potions;
    //private Type[] PotionPrefabs = { SpeedPotion, typeof(TeleportPotion), typeof(GuidePotion) };

    //private Potion potion;

    int maxPotions;

    // Start is called before the first frame update
    public void Start() {
       mazeGrid = new MazeCell[mazeWidth, mazeDepth];
       maxPotions = mazeWidth * mazeDepth / 10;
       int numPotions = 0;


       System.Random random = new System.Random();

       for (int i = 0; i < mazeWidth; i++) {
        for (int j = 0; j < mazeDepth; j++) {
            // DO NOT TOUCH OR YOU WILL BE FIRED - to avoid wall flickering from overlaps
            var randomOffset = 0; // UnityEngine.Random.Range(0.002f, 0.006f);
            mazeGrid[i, j] = Instantiate(_mazeCell, new Vector3(i + randomOffset, 0, j + randomOffset), Quaternion.identity);
            // --------------------------------------------------------------------------------------------- //
            // places a potion in a cell at random, up to the max number of potions allowed in the maze
            if (numPotions < maxPotions && random.Next(11) == 3) {
                GameObject potionType = Potions[random.Next(Potions.Length)];
                GameObject p = Instantiate(potionType, new Vector3(i, 0.5f, j), Quaternion.identity);
                p.gameObject.SetActive(true);
                numPotions++;
                Debug.Log("Spawned at: " + i + " " + j);
            }            
        }
       }

       Debug.Log(numPotions);

       GenerateMaze();
       mazeGrid[0, 0].ClearBackWall();
       mazeGrid[mazeWidth - 1, mazeDepth -1].ClearFrontWall();

       foreach (MazeCell cell in mazeGrid) {
        if (cell.isClosed()) cell.Close(); 
       } 

       
    }


    private void GenerateMaze() {
       var randX = new System.Random();
       var randY = new System.Random();

       var cells = new List<MazeCell>();
       int x = randX.Next(mazeWidth);
       int y = randY.Next(mazeDepth);
       Debug.Log(x + " and " + y);
       cells.Add(mazeGrid[x, y]);
       var len = 0;

       // algo:
       // 1 - pick a random cell, add it to stack
       // 2 - pick any unvisited neighbor of cell on top of stack, add it to path/stack
       // 3 - if cell has no unvisited neighbors, pop from stack, repeat from step 2.
       // 4 - else, continue
       // repeat until stack is empty

       while (len >= 0) {
            //yield return new WaitForSeconds(2);
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
       }

    }


    private MazeCell findUnvisitedNeighbor(MazeCell current) {
        var unvisitedCells = getUnvisited(current);
        return unvisitedCells.OrderBy(_ => UnityEngine.Random.Range(1, 10)).FirstOrDefault();

    }
    
    private IEnumerable<MazeCell> getUnvisited(MazeCell current) {
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
            // we went from right to left, so we clear right wall of prev, and left wall of curr
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
