using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public AudioClip DeathAudio;
    public Rigidbody2D rb;
    public float speed = 4f;

    public Transform WallCheck;
    public Transform LedgeCheck;

    public LayerMask groundLayer;
    [Range(-1,1)]
    public int direction = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(direction == 0)
        {
            direction = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsWalled() == true || IsLedged() == true)
        {
            direction *= -1;
        }
        transform.localScale = new Vector3(direction, 1, 1);
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

   private bool IsWalled()
    {
        return Physics2D.OverlapCircle(WallCheck.position, 0.2f, groundLayer);
    }

    private bool IsLedged()
    {
        return Physics2D.OverlapCircle(LedgeCheck.position, 0.2f, groundLayer);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(DeathAudio, transform.position);
        }
    }
}
