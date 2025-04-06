using UnityEngine;
using UnityEngine.UI;

public class Gripper : MonoBehaviour
{
    public bool gripping;
    private Collider gripCollider;
    private Camera mainCamera;

    public Image handOpenImg;
    public Image handClosedImg;
    public Image IndexGripImg;
    public Image MiddleGripImg;
    public Image RingGripImg;
    public Image PinkyGripImg;

    public Vector3 direction { get; private set; }
    public float shrinkRate = 0.5f;
    private float currentHoldTime = 0f;

    private void Awake()
    {
        mainCamera = Camera.main;
        gripCollider = GetComponent<Collider>();
        handOpenImg.gameObject.SetActive(true);
        handClosedImg.gameObject.SetActive(false);
        IndexGripImg.gameObject.SetActive(false);
        MiddleGripImg.gameObject.SetActive(false);
        RingGripImg.gameObject.SetActive(false);
        PinkyGripImg.gameObject.SetActive(false);

    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        handOpenImg.transform.position = mousePosition;
        handClosedImg.transform.position = mousePosition;
        IndexGripImg.transform.position = mousePosition;
        MiddleGripImg.transform.position = mousePosition;
        RingGripImg.transform.position = mousePosition;
        PinkyGripImg.transform.position = mousePosition;

        // if (Input.GetMouseButtonDown(1)) 
        // {
        //     StartGripping();
        // } 
        // else if (Input.GetMouseButtonUp(1)) 
        // {
        //     StopGripping();
        // }
        if (Input.GetKeyDown(KeyCode.Alpha1) || 
            Input.GetKeyDown(KeyCode.Alpha2) || 
            Input.GetKeyDown(KeyCode.Alpha3) || 
            Input.GetKeyDown(KeyCode.Alpha4) || 
            Input.GetKeyDown(KeyCode.Alpha5)) 
        {
            StartGripping();
        } 
        else if (Input.GetKeyUp(KeyCode.Alpha1) || 
            Input.GetKeyUp(KeyCode.Alpha2) || 
            Input.GetKeyUp(KeyCode.Alpha3) || 
            Input.GetKeyUp(KeyCode.Alpha4) || 
            Input.GetKeyUp(KeyCode.Alpha5)) 
        {
            StopGripping();
        }
        else if (gripping)
        {
            ContinueGripping();
        }
    }

    private void StartGripping()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        transform.position = newPosition;

        handOpenImg.gameObject.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            handClosedImg.gameObject.SetActive(true);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            IndexGripImg.gameObject.SetActive(true);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            MiddleGripImg.gameObject.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) 
        {
            RingGripImg.gameObject.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) 
        {
            PinkyGripImg.gameObject.SetActive(true);
        }

        gripping = true;
        gripCollider.enabled = true;
        currentHoldTime = 0f;
    }

    private void StopGripping()
    {
        handOpenImg.gameObject.SetActive(true);
        handClosedImg.gameObject.SetActive(false);
        IndexGripImg.gameObject.SetActive(false);
        MiddleGripImg.gameObject.SetActive(false);
        RingGripImg.gameObject.SetActive(false);
        PinkyGripImg.gameObject.SetActive(false);

        gripping = false;
        gripCollider.enabled = false;
    }

    private void ContinueGripping()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        transform.position = newPosition;

        if (gripping)
        {
            currentHoldTime += Time.deltaTime;
        }
    }
}