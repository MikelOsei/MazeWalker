using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public Transform orientation;
    public Transform player;
    float yRotation;
    // Start is called before the first frame update
    void Start() {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * sensX;
        yRotation += mouseX;

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        transform.position = player.position;
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
