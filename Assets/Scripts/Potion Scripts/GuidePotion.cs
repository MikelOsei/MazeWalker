using UnityEngine;
using System.Collections;
using System;

public class GuidePotion : Potion {

    [SerializeField]
    private GameObject guide_potion; 

    public override void Generate() {
        guide_potion.SetActive(true);
    }

    public override void ApplyEffects(PlayerLogic player) {}

    public override string getName() {
        return "guide";
    }

    public void Update() {
        Debug.Log("Yippee!");
    }

}