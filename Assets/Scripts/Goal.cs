using UnityEngine;

// Sirf Level 4 ke liye: portal ko chhuo = level jeet gaye (GameManager mai Target Score = 1).
public class Goal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) GameManager.Instance.AddScore(1);
    }
}
