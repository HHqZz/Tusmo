using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Game over ou collision logic
            Debug.Log("Collision!");
            SoundManager.Instance?.PlayCollisionSound();
            UIManager.Instance?.ShowGameOver();
        }
    }
}