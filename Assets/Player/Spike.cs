using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    public AudioClip DeathAudio;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(ResetScene());
        }
    }

    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
    
