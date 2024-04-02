using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAudio : MonoBehaviour
{
    AudioSource footsteps;
    public Transform myOrientation;
    public float minDistance = 0.001f;
    private Vector3 previousPosition;
    private Quaternion previousRotation;

    // Start is called before the first frame update
    void Start() {
        // Store the initial position of the object
        previousPosition = transform.position;
        previousRotation = myOrientation.rotation;
        footsteps = GetComponent<AudioSource>();
        footsteps.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance the object has moved since the last frame
        float moveDistance = Vector3.Distance(transform.position, previousPosition);
        float turnDistance = Quaternion.Angle(myOrientation.rotation, previousRotation);

        // If the player has moved or turned more than the minimum distance (0)
        if (moveDistance > 0 || turnDistance != 0) {
            // Play the sound if not currently playing
            if (!footsteps.isPlaying) footsteps.Play();

            // default sound settings
            footsteps.spatialBlend = 0;
            footsteps.reverbZoneMix = 1;
            footsteps.pitch = 1;
            footsteps.volume = 1;

            // distorts sound for a little variety when turning only :)
            if (turnDistance != 0 && moveDistance == 0) {
                footsteps.spatialBlend = 0.8f;
                //footsteps.reverbZoneMix = 0.5f;
                footsteps.pitch = 1.3f;
                footsteps.volume = 0.5f;
            }
            // Stores the current position & orientation of the object for the next frame
            previousPosition = transform.position;
            previousRotation = myOrientation.rotation;
        } else footsteps.Stop(); 
        // else, stop if player has not moved
    }
}
