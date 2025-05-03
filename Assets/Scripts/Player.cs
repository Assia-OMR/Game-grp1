using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpingPower;
    public LayerMask groundLayer;
    public Transform groundChek;
    public float fallThreshold = -10f; // Si le joueur tombe sous -10, il perd

    float horizontal; 
    SpriteRenderer sr;
    Animator animator;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        animator.SetBool("IsGrounded", IsGrounded());

        // Détection de chute
        if (transform.position.y < fallThreshold)
        {
            Debug.Log("Le joueur est tombé !");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Quitter avec Échap (fonctionne dans le build)
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Pour tester dans l’éditeur
#else
            Application.Quit();
#endif
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        sr.flipX = horizontal < 0;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundChek.position, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }
}
