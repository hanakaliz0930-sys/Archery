using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    public void StartButton()
    {
        SceneManager.LoadScene("Game");
    }
}
