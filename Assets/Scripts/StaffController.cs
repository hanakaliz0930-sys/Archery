using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Leap;

public class StaffController : MonoBehaviour
{
    [SerializeField] private BowController bowController;
    [SerializeField] private UIManager uiManager;
    public event Action<int> WritePoints;

    public float pullThreshold = 0.01f;
    private Vector3 pullStartPosition;
    private bool wasGrabbing;
    private bool hasShot;
    public AudioClip birdSound;
    public AudioClip arrowSound;
    public Transform shootPoint;
    public LineRenderer line;
    [SerializeField] private float distance = 300f;
    public GameObject prefab;
    [SerializeField] private int score = 0;
    public int Score => score;
    public HandInput HandInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentLeft = HandInput.leftHandPosition;
        if(HandInput.isGrabbingLeft == true && wasGrabbing == false)
        {
            pullStartPosition = currentLeft;
            hasShot = false;
        }
        float pullZ = pullStartPosition.z - currentLeft.z;
            //if(Input.GetMouseButtonUp(0))
            if(HandInput.isGrabbingLeft && hasShot == false && pullZ > pullThreshold)
        { 
            hasShot = true;
            AudioManager.instance.PlayStaffSFX(arrowSound);
            Ray ray = new Ray(shootPoint.position, shootPoint.forward);
            RaycastHit hit;
            Vector3 endPoint;
            if (Physics.Raycast(ray, out hit, distance))
            {
                endPoint = hit.point;
                
                var bird = hit.collider.GetComponent<BirdController>();
                if(bird != null)
                {
                    AudioManager.instance.PlayBirdSFX(birdSound);
                    GainPoints(bird.value);
                    Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                endPoint = shootPoint.position + shootPoint.forward * distance;
            }
            DrawLine(shootPoint.position, endPoint);
        }

                    /*float mouseX = Input.GetAxis("Mouse X") * bowController.speed;
                    float mouseY = Input.GetAxis("Mouse Y") * bowController.speed;

                    Quaternion yaw = Quaternion.AngleAxis(mouseX, Vector3.up);
                    Quaternion pitch = Quaternion.AngleAxis(-mouseY, Vector3.right);

                    transform.rotation = yaw * transform.rotation * pitch;*/

                    /*Vector3 current = HandInput.rightHandPosition;
                    Vector3 delta = current - lastHandPosition;
                    if(!initialized)
                    {
                        lastHandPosition = current;
                        initialized = true;
                    }
                    yaw += delta.x * bowController.speed;
                    pitch -= delta.y * bowController.speed;
                    pitch = Mathf.Clamp(pitch, -90, 90);
                    Quaternion targetRotation = Quaternion.Euler(pitch, 0, yaw) * Quaternion.Euler(0, 0, 90);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f *Time.deltaTime);*/
                    
                    var targetRotation = HandInput.rightHandRotation * Quaternion.Euler(0, 90, 90);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, bowController.speed * Time.deltaTime);

                     wasGrabbing = HandInput.isGrabbingLeft;
    }
    void DrawLine(Vector3 start, Vector3 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }
    public void GainPoints(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
        WritePoints?.Invoke(score);
    }
}