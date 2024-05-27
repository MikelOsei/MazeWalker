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
        if (pos.y < -100) return;
        Quaternion rotation = transform.rotation;
        pos.y = hover * Mathf.Cos(Time.time * hoverSpeed) + 0.5f;
        rotation.y = Mathf.Cos(Time.time * (hoverSpeed - 0.2f));
        transform.position = pos;
        transform.rotation = rotation;
    }
 
}
