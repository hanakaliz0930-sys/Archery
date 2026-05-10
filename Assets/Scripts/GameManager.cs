using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int minimumPointRequired = 40;
    private float startTime = -1;
    public float CurrentTime {get; private set;} = 0;
    private bool isRunning = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning)
        {
            CurrentTime = Time.time - startTime;
        }
    }

    void GameOver()
    {
        isRunning = false;
    }
}
