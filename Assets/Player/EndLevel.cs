using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public AudioClip CollectStar;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(CollectStar, transform.position);
            Destroy(gameObject);
        }
    }
}
