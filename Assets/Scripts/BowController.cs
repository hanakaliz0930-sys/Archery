using UnityEngine;

public class BowController : MonoBehaviour
{
    public HandInput HandInput;
    public float speed = 4;
    Rigidbody rb;
    public float force = 5000f;
    [SerializeField] private Transform staffTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = HandInput.rightHandRotation * Quaternion.Euler(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.Rotate(0 , 0, Input.GetAxis("Mouse X") * speed);
        var targetRotation = HandInput.rightHandRotation * Quaternion.Euler(0, 0, 90);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}