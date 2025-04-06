using UnityEngine;
using System.Collections;

public class Fruit : MonoBehaviour
{
    public GameObject sliced;
    public GameObject whole;
    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juicesEffect;
    
    // private Vector3 originalScale;
    private FruitSpawnerTraining fruitSpawnerTraining;
    private GameManagerTraining gameManagerTraining;
    private bool isBeingGripped = false;

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juicesEffect = GetComponentInChildren<ParticleSystem>();
        whole.SetActive(true);
        sliced.SetActive(true);
        // originalScale = transform.localScale; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gripper"))
        {
            Gripper gripper = other.GetComponent<Gripper>();
            if (gripper != null)
            {
                isBeingGripped = true;
                StartCoroutine(GripFruit(gripper));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gripper"))
        {
            isBeingGripped = false; // Stop shrinking when gripper exits
        }
    }

    private IEnumerator GripFruit(Gripper gripper)
    {
        fruitSpawnerTraining = FindObjectOfType<FruitSpawnerTraining>();

        while (transform.localScale.y > fruitSpawnerTraining.minScalePop && isBeingGripped)
        {
            if (!gripper.gripping) yield break; 

            if (gameObject.name.Contains("Watermelon") && Input.GetKey(KeyCode.Alpha1))
            {
                transform.localScale -= transform.localScale * gripper.shrinkRate * Time.deltaTime;

            } else if (gameObject.name.Contains("Apple") && Input.GetKey(KeyCode.Alpha2))
            {
                transform.localScale -= transform.localScale * gripper.shrinkRate * Time.deltaTime;

            } else if (gameObject.name.Contains("Orange") && Input.GetKey(KeyCode.Alpha3))
            {
                transform.localScale -= transform.localScale * gripper.shrinkRate * Time.deltaTime;

            } else if (gameObject.name.Contains("Lemon") && Input.GetKey(KeyCode.Alpha4))
            {
                transform.localScale -= transform.localScale * gripper.shrinkRate * Time.deltaTime;

            } else if (gameObject.name.Contains("Kiwi") && Input.GetKey(KeyCode.Alpha5))
            {
                transform.localScale -= transform.localScale * gripper.shrinkRate * Time.deltaTime;
            }
            
            yield return null;
        }

        if (transform.localScale.y <= fruitSpawnerTraining.minScalePop) {
            PopFruit();
        }
        
    }

    private void PopFruit()
    {
        whole.SetActive(false);
        
        fruitCollider.enabled = false;
        
        gameManagerTraining = FindObjectOfType<GameManagerTraining>();

        gameManagerTraining.IncreaseScore();

        juicesEffect.Play();

        Destroy(gameObject, 0.5f); // Destroy after animation
    }
}