using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Potion : MonoBehaviour {

    public abstract void Generate();

    public abstract string getName();

    public abstract void ApplyEffects(PlayerLogic player);

}