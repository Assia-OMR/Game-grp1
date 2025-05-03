using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpingPower;
    public LayerMask groundLayer;
    public Transform groundChek;
    float horizontal; 
    SpriteRenderer sr;
    Animator animator;

    private void Start()
    {sr = GetComponent<SpriteRenderer>();
    animator=GetComponent<Animator>();}

    private void FixedUpdate(){
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        animator.SetBool("IsGrounded", IsGrounded());
    }

    public void Move(InputAction.CallbackContext context){
        horizontal = context.ReadValue<Vector2>().x;
        if (horizontal<0)
            sr.flipX=true;
        else
            sr.flipX = false;
    }

    public void Jump(InputAction.CallbackContext context){
        if (context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }
    }

    bool IsGrounded(){
    RaycastHit2D hit = Physics2D.Raycast(groundChek.position, Vector2.down, 0.1f, groundLayer);
    return hit.collider != null;
}



    
    
}
