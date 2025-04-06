using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private bool slicing;
    private Collider bladeCollider;
    private Camera mainCamera;
    private TrailRenderer bladeTrail;

    public Vector3 direction { get; private set; }
    public float sliceForce = 10f;
    public float minSliceVelocity = 0.01f;
    public float minComboVelocity = 0.05f;

    private HashSet<Fruits> slicedFruits = new HashSet<Fruits>();

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            StartSlicing();
        } 
        else if (Input.GetMouseButtonUp(1)) 
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void OnEnable()
    {
        StopSlicing();        
    }


    private void OnDisable()
    {
        StopSlicing();        
    }

    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();

        slicedFruits.Clear();
    }
    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;

        CheckComboBonus();
    }

    private void ContinueSlicing() 
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        if (velocity < minComboVelocity) 
        {
            CheckComboBonus();
            slicedFruits.Clear();
        }

        transform.position = newPosition;
    }

    public void RegisterSlice(Fruits fruit)
    {
        if (!slicedFruits.Contains(fruit))
        {
            slicedFruits.Add(fruit);
        }
    }

    private void CheckComboBonus()
    {
        if (slicedFruits.Count >= 3) 
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AddComboBonus(slicedFruits.Count);
            }
        }
    }
}
