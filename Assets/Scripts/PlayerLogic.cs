using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditorInternal;

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
                GameObject FX = Instantiate(ExplodeOnDestroy, p.gameObject.transform.position, Quaternion.identity);
                p.gameObject.transform.position = new Vector3(0, -1000, 0);
            }

            if (p.getName() != "speed") {
                StartCoroutine(Delay(p.getName(), p.gameObject));
            } else Destroy(p.gameObject);
            
        } else return;
    }

    private IEnumerator Delay(string name, GameObject p) {
        if (name == "guide") {
            yield return new WaitForSeconds(21);
            Destroy(p);
            Debug.Log("Potion Destroyed from coroutine");
        } else if (name == "teleport") {
            yield return new WaitForSeconds(3);
            Destroy(p);
        } else Debug.Log($"Passed: {name}, not destroyed");
    }

    
}
