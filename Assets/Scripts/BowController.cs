using UnityEngine;

public class BowController : MonoBehaviour
{
    public float speed = 4;
    Rigidbody rb;
    public float force = 5000f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(0 , 0, Input.GetAxis("Mouse X") * speed);


    }
}