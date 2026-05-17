using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UIElements;

public class BirdController : MonoBehaviour
{
    public int value = 1;
    private int speed = 1;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if(transform.position.x >= 14.5f)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    
}
