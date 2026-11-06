using UnityEngine;

// Lecture 06 wali Coin.cs agar tumhare paas hai to WOHI use karo. Ye sirf backup hai.
public class Coin : MonoBehaviour
{
    void Update() => transform.Rotate(0f, 120f * Time.deltaTime, 0f, Space.World);   // ghumta hai

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.AddScore(1);
        Destroy(gameObject);
    }
}
