using UnityEngine;

public class Bomb : MonoBehaviour
{
    private ParticleSystem explodeEffect;
    
    private void Awake() 
    {
        explodeEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {

            explodeEffect.Play();

            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.Explode();
            } else {
                FindObjectOfType<GameManagerLeveled>().Explode();
            }
        }
    }
}
