using UnityEngine;
using System;
//using System.Numerics;



public class TeleportPotion : Potion {

    [SerializeField]
    private GameObject port_potion;

    public override void Generate() {
        port_potion.SetActive(true);
    }

    public override void ApplyEffects(PlayerLogic player) {

    }

    /*public override void OnTriggerEnter(Collider other) {
        Debug.Log(getName() + " hit by " + other.tag);
        Destroy(this);
        throw new NotImplementedException();
    }*/

    public override string getName() {
        return "teleport";
    }

}
