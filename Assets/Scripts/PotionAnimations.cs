using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PotionAnimations : MonoBehaviour
{
    public float hover = 0.1f;
    public float hoverSpeed = 1.1f;
    // Start is called before the first frame update

    ParticleSystem ps;
    void Start() {
        ps = GetComponent<ParticleSystem>();
        var main = ps.main;
    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = transform.position;
        Quaternion rotation = transform.rotation;
        pos.y = hover * Mathf.Cos(Time.time * hoverSpeed) + 0.55f;
        rotation.y = Mathf.Cos(Time.time * (hoverSpeed - 0.2f));
        transform.position = pos;
        transform.rotation = rotation;
    }

    /*public void OnTriggerEnter(Collider other) {
        Debug.Log("Potion hit by " + other.tag);
        Destroy(potion);
        //throw new NotImplementedException();
    }*/
}
