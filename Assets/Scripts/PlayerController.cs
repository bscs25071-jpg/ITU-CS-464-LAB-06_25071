using System.Collections;
using UnityEngine;

// LECTURE 07 - bilkul same. Lecture 06 ka controller + BoostSpeed.
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveForce = 10f;
    Rigidbody rb; Vector3 input;
    float speedMultiplier = 1f;
    Coroutine boost;

    void Awake() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        float h = Input.GetAxis("Horizontal"), v = Input.GetAxis("Vertical");
        input = Vector3.ClampMagnitude(new Vector3(h, 0f, v), 1f);   // diagonal tez na ho
    }

    void FixedUpdate() => rb.AddForce(input * moveForce * speedMultiplier);

    public void BoostSpeed(float multiplier, float seconds)
    {
        if (boost != null) StopCoroutine(boost);                      // doosri pickup = timer restart
        boost = StartCoroutine(Boost(multiplier, seconds));
    }

    IEnumerator Boost(float multiplier, float seconds)
    {
        speedMultiplier = multiplier;
        yield return new WaitForSeconds(seconds);                     // timer PLAYER pe chalta hai
        speedMultiplier = 1f; boost = null;
    }
}
