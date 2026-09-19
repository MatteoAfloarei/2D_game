using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpPower = 20f;
    public float moveSpeed = 8f;

    private int direction;

    public Transform groundCheck; // Transform to check if the player is grounded
    public LayerMask groundLayer; // LayerMask to specify which layers are considered ground
    public Animator anim; // Reference to the Animator component
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float reloadTime = 0.5f; // Time between shots
    private float nextFireTime; // Time when the player can fire again
    public AudioClip jumpAudio;
    public AudioClip shootAudio;

    // Update is called once per frame
    void Update()
    {  //movement
        if (Keyboard.current.dKey.isPressed)
        {
            direction = 1;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            direction = -1;
        }
        else
        {
            direction = 0;
        }
        if(direction != 0)
        {
            transform.localScale = new Vector3(direction, 1, 1);
        }
        if(Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            //jump and audio
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            AudioSource.PlayClipAtPoint(jumpAudio, transform.position);
        } 
        if(Keyboard.current.spaceKey.wasReleasedThisFrame && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        anim.SetBool("IsGround", IsGrounded());
        if(direction != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
            anim.SetFloat("Yvelocity", rb.linearVelocity.y);
        }
        //bullet
        if(Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.transform.localScale = new Vector3(transform.localScale.x, 1, 1); // Set the bullet's scale based on the player's direction
            nextFireTime = Time.time + reloadTime; // Update the next fire time

            //audio 
            AudioSource.PlayClipAtPoint(shootAudio, transform.position);
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }
    private bool IsGrounded()
    {
        // Check if the player is grounded by casting a small circle at the groundCheck position
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }
}
