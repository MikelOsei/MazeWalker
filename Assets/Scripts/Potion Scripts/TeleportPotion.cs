using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;
//using System.Numerics;



public class TeleportPotion : Potion {

    [SerializeField]
    private GameObject port_potion;
    [SerializeField]
    private GameObject magicCircle;

    public override void Generate() {
        port_potion.SetActive(true);
    }

    public override void ApplyEffects(PlayerLogic player) {
        MazeGenerator maze = FindFirstObjectByType<MazeGenerator>();
        System.Random random = new System.Random();
        int newX = random.Next(maze.mazeWidth);
        int newY = random.Next(maze.mazeDepth);
        Debug.Log(newX + ", " + newY);

        Vector3 newPos = new Vector3(newX, player.transform.position.y, newY);

        StartCoroutine(EffectLag(player.transform.position));
        player.transform.position = newPos;
        StartCoroutine(EffectLag(newPos));
    }

    private IEnumerator EffectLag(Vector3 position) {
        GameObject mc = Instantiate(magicCircle, new Vector3(position.x, 0.2f, position.z), Quaternion.identity);
        yield return new WaitForSeconds(4);
        Destroy(mc);
    }

    public override string getName() {
        return "teleport";
    }

}
