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
    }

    public override string getName() {
        return "speed";
    }

}