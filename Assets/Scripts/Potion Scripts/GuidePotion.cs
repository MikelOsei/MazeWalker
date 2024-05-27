using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class GuidePotion : Potion {

    [SerializeField]
    private GameObject guide_potion; 

    [SerializeField] 
    private GameObject _guideWisps;
    
    MazeGenerator maze;
    MazeCell[,] mazeGrid;
    private List<MazeCell> path;
    List<GameObject> wisps = new List<GameObject>();

    public override void Generate() {
        guide_potion.SetActive(true);
    }

    public override void ApplyEffects(PlayerLogic player) {

        maze = FindFirstObjectByType<MazeGenerator>();
        mazeGrid = maze.mazeGrid;
        
        int posX = (int) player.transform.position.x;
        int posY = (int) player.transform.position.z;
        MazeCell start = mazeGrid[posX, posY];

        path = new List<MazeCell>();
        path.Add(start);
        start.inPath = true;

        if (!FindPath()) Debug.Log("Uh-oh");

        DisplayPath();

    }

    private bool FindPath() {
        
        MazeCell current = path[path.Count - 1];
        if (current == mazeGrid[maze.mazeWidth - 1, maze.mazeDepth - 1]) return true;       
        Debug.Log("Not at end, checking for paths...");

        foreach (MazeCell neighbor in current.neighbors) {
            if (neighbor.inPath) continue;
            // otherwise: 
            path.Add(neighbor);
            neighbor.inPath = true;
            if (FindPath()) return true;
            else { 
                neighbor.inPath = false;
                path.RemoveAt(path.Count - 1);
            }
        }

        return false;

    }

    private void DisplayPath() {

        StartCoroutine(AnimatePath());

    }

    private IEnumerator AnimatePath() {

        foreach (MazeCell cell in path) {
           Vector3 pos = cell.transform.position;
           cell.ActivateWisps();
           cell.inPath = false;
        }

        yield return new WaitForSeconds(15);
        
        foreach (MazeCell cell in path) {
            cell.HideWisps();
        }
        
    }

    public override string getName() {
        return "guide";
    }

}