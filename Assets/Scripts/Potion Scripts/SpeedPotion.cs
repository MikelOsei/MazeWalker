using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;

public class SpeedPotion : Potion {

    [SerializeField]
    private GameObject speed_potion;

    public override void Generate() {
        speed_potion.SetActive(true);
    }

    public override void ApplyEffects(PlayerLogic player) {
        player.speed *= 2;
        int maxSpeed = 4;
        if (player.speed > maxSpeed) player.speed = maxSpeed;  
        //speed_potion.SetActive(false);
    }

   /* public override void OnTriggerEnter(Collider other) {
       // Debug.Log(getName() + " hit by " + other.tag);
        if (other.CompareTag("Player")) {
            GameObject p = other.gameObject;
            PlayerLogic player = p.GetComponent<PlayerLogic>();
            player.speed = 2;
        }
    }*/

    public override string getName() {
        return "speed";
    }

}