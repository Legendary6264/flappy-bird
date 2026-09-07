using UnityEngine;


public class ScoreZone : MonoBehaviour
{
    private bool scored = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (scored) return;

        if (other.CompareTag("Player"))
        {
            scored = true;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(1);
            }
        }
    }
}