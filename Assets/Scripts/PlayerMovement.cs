using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private bool IsGrounded = false;
    PlayerInput input;
    InputAction action;
    Vector2 moveDir = Vector2.zero;
    public float Speed;
    private bool canDash;
    public float dashForce;
    private bool canMove = true;
    [SerializeField] float jumpForce;
    private Animator animator;
    private Vector2 moveInput;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;
    public GameObject attackArea;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        action = input.actions.FindAction("Movement");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * Speed * Time.deltaTime);
    

}

    public void MovePlayer(InputAction.CallbackContext context)
    {
        
            if (canMove == true)
            {
                moveInput = context.ReadValue<Vector2>();
                moveDirection = moveInput.normalized;
                canDash = true;
                animator.Play("Running");

            if (moveDirection.x < 0) // Moving left
            {
                spriteRenderer.flipX = true;
                Vector3 scale = attackArea.transform.localScale;
                scale.x = -1;
                attackArea.transform.localScale = scale;
            }
            else if (moveDirection.x > 0) // Moving right
            {
                spriteRenderer.flipX = false;
                Vector3 scale = attackArea.transform.localScale;
                scale.x = 1;
                attackArea.transform.localScale = scale;
            }

            if (context.canceled)
            {
                animator.Play("Idle");
            }
        }

 }
           
   
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (IsGrounded == true)
            {
                Debug.Log("Jump");
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }
    public void Dash(InputAction.CallbackContext context)
    {

        if (context.performed && canDash == true)
        {
            Debug.Log("Dash");
            Vector2 direction = action.ReadValue<Vector2>();
            Vector3 dashVector = new Vector3(direction.x, 0, 0) * dashForce;
            rb2d.AddForce(dashVector, (ForceMode2D)ForceMode.Impulse);
            canMove = false;
            canDash = false;
            StartCoroutine(WaitForDash());

        }

    }
    IEnumerator WaitForDash()
    {
        yield return new WaitForSeconds(0.4f);
        rb2d.velocity = Vector3.zero;
        canMove = true;
        canDash = true;
    }

}
