using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDetector : MonoBehaviour
{
    public float fallThreshold = -10f; // Si le joueur passe en dessous de -10, il "meurt"

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Debug.Log("Le joueur est tombé !");
            // Recharge la scène actuelle
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
