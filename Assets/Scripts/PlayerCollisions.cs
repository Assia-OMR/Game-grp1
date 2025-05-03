using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    public int Life = 3;
    public int apples=0;  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            TakeDamages(3);
        }

        if (collision.CompareTag("Apple"))
        {
            apples++;
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamages(int damage)
    {
        Life -= damage;
        if (Life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Ajoute une force vers le haut
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 150);
        // Désactive le collider pour tomber à travers les plateformes
        GetComponent<Collider2D>().isTrigger = true; 
        // Planifie le redémarrage du niveau après 1 seconde
        Invoke("RestartLevel", 1f);
    }

    public void RestartLevel()
    {
        // Correction de la faute de frappe "LoadScen" -> "LoadScene"
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start() 
    {
        // Vérifie si les composants existent
        if (GetComponent<Rigidbody2D>() == null)
        {
            Debug.LogError("Rigidbody2D manquant sur le joueur !");
        }
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError("Collider2D manquant sur le joueur !");
        }
    }

    void Update()
    {
        // Si le joueur tombe en dessous d'une certaine hauteur, redémarre immédiatement
        if (transform.position.y < -10f)
        {
            RestartLevel();
        }
    }
}