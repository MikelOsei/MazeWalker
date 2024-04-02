using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    
    public Transform camPos;

    public void Start() {}

    // Update is called once per frame
    void Update() {
        transform.position = camPos.position;
    }
}
