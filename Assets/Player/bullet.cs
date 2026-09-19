using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public LayerMask groundLayer;
    public AudioClip DeathAudio;
    public float speed = 10f;
    public float timer = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, timer);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * transform.localScale.x * Time.deltaTime;
        if(IsGrounded() == true)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(DeathAudio, transform.position);
        }
    }
    private bool IsGrounded()
    {
        // Check if the bullet is colliding with the ground layer
        return Physics2D.OverlapCircle(transform.position, 0.3f, groundLayer);
    }
}
