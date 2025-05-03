using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    public int Life = 1;
    public int coeurs = 0;
    bool isDead = false;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return; // Ne rien faire si déjà mort

        if (collision.CompareTag("Spike"))
        {
            Die(); // Au lieu de TakeDamage(1)
        }
        if (collision.CompareTag("Coeur"))
        {
            coeurs++;
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("EndLevel")){
            print("Bravo!");
        }

    }


    private void TakeDamage(int damage)
    {
        Life -= damage;
        
        if (Life <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Die(){
        isDead=true;
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * 200);
        GetComponent<Collider2D>().isTrigger=true;
        Invoke("RestartLevel",1);
        isDead=true;
    }
    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}