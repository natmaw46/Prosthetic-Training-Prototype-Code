using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fruits : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juicesEffect;


    private int hitFloor;

    private void Awake() 
    {
        fruitCollider = GetComponent<Collider>();
        fruitRigidbody = GetComponent<Rigidbody>();
        juicesEffect = GetComponentInChildren<ParticleSystem>();
        hitFloor = 0;
    }

    public void Slice(Vector3 direction, Vector3 position, float force, bool isGameOver) 
    {
        if (!isGameOver) 
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            GameManagerLeveled gameManagerLeveled = FindObjectOfType<GameManagerLeveled>();
            GameManagerTraining gameManagerTraining = FindObjectOfType<GameManagerTraining>();
            if (gameManager != null)
            {
                gameManager.IncreaseScore();
            } else if (gameManagerLeveled != null) {
                
                gameManagerLeveled.PlaySliceSound();
            } else if (gameManagerTraining != null) {
                
                gameManagerTraining.IncreaseScore();
            }
        }

        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider.enabled = false;
        juicesEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices) 
        {
            slice.linearVelocity = fruitRigidbody.linearVelocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {

            Blade blade = other.GetComponent<Blade>(); 
            Slice(blade.direction, blade.transform.position, blade.sliceForce, false);

            blade.RegisterSlice(this);
        }

        if (other.CompareTag("Floor") && whole.activeSelf == true) {

            hitFloor += 1;

            if (hitFloor == 2) {
                GameManager gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    gameManager.MinusLife(transform.position);
                } else {
                    FindObjectOfType<GameManagerLeveled>().MinusLife(transform.position);
                }
            }
        }
    }
}
