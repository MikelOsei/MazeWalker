using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditorInternal;
//using System.Numerics;
//using System.Numerics;

[RequireComponent(typeof(Rigidbody))]
public class PlayerLogic : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 0.001f;

    [SerializeField] Transform orientation;
    [SerializeField] GameObject ExplodeOnDestroy;

    private float verticalInput;
    private Vector3 moveDirection;
    //Camera camera;

    public void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
       // camera = Camera.current;

    }

    private void FixedUpdate() {
        Move();
    }

    private void Update() {
        getInput();
    }

    private void getInput() {
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    public void Move() {
        moveDirection = orientation.forward * verticalInput;
        transform.position += moveDirection * speed * (Time.deltaTime / 3);
    }

    public void OnCollisionEnter(Collision other) {
        var thing = other.collider;
        Debug.Log("Player has encountered: " + thing.tag);
        if (thing.CompareTag("Potion")) {
            Potion p = other.gameObject.GetComponent<Potion>();
            p.ApplyEffects(this);

            
            if (ExplodeOnDestroy) {
                ParticleSystem potionMagic = p.gameObject.GetComponent<ParticleSystem>();
                var magic = potionMagic.main;
                
                Debug.Log("magic color: " + magic.startColor);
                GameObject FX = Instantiate(ExplodeOnDestroy, p.gameObject.transform.position, Quaternion.identity);
                ParticleSystem ps = FX.GetComponent<ParticleSystem>();
                var explosion = ps.main;
                var ex = ps.main.startColor;

                ex = new ParticleSystem.MinMaxGradient(magic.startColor.gradient);
            }

            Destroy(p.gameObject);
        } else return;
    }

    
}
