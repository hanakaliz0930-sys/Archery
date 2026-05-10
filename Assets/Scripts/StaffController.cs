using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class StaffController : MonoBehaviour
{
    [SerializeField] private BowController bowController;
    [SerializeField] private UIManager uiManager;
    public event Action<int> WritePoints;

    public AudioClip birdSound;
    public AudioClip arrowSound;
    public Transform shootPoint;
    public LineRenderer line;
    [SerializeField] private float distance = 500f;
    public GameObject prefab;
    [SerializeField] private int score = 0;
    public int Score => score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetMouseButtonUp(0))
        { 
            AudioManager.instance.PlayStaffSFX(arrowSound);
            Ray ray = new Ray(shootPoint.position, shootPoint.forward);
            RaycastHit hit;
            Vector3 endPoint;
            if (Physics.Raycast(ray, out hit, distance))
            {
                endPoint = hit.point;
                Debug.Log("Hit: " + hit.collider.name);
                
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
            else {

                    float mouseX = Input.GetAxis("Mouse X") * bowController.speed;
                    float mouseY = Input.GetAxis("Mouse Y") * bowController.speed;

                    Quaternion yaw = Quaternion.AngleAxis(mouseX, Vector3.up);
                    Quaternion pitch = Quaternion.AngleAxis(-mouseY, Vector3.right);

                    transform.rotation = yaw * transform.rotation * pitch;

            }
            
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