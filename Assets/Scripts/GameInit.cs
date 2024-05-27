using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    Camera mainCamera;
    public Camera camTwo;
    
    public Vector3 startPos;

    // Start is called before the first frame update
    IEnumerator Start() {
        mainCamera = Camera.main;
        mainCamera.enabled = true;
        camTwo.transform.position = mainCamera.transform.position;
        camTwo.enabled = false;

        yield return new WaitForSeconds(5);

        mainCamera.enabled = false;
        camTwo.enabled = true;
        camTwo.transform.position = Vector3.Lerp(camTwo.transform.position, startPos, 5);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Press V to switch camera-view

        if (Input.GetKeyDown(KeyCode.V)) {
            if (mainCamera.enabled) {
                mainCamera.enabled = false;
                camTwo.enabled = true;
            } else if (camTwo.enabled) {
                camTwo.enabled = false;
                mainCamera.enabled = true;
            }
        }
        
    }
}
