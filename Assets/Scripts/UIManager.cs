using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private StaffController staffController;
    [SerializeField] private TMP_Text PointText;
    [SerializeField] private TMP_Text TimeRemainingText;
    [SerializeField] private TMP_Text WinText;
    private float totalTime = 65f;
    private int minimumPointsToWin = 40;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        staffController.WritePoints += WritePoints;
        WritePoints(0);
        WinText.gameObject.SetActive(false);
        TimeRemainingText.gameObject.SetActive(true);
    }

    void LateUpdate()
    {
        TimeRemainingText.text = $"Time Remaining: {TimeToString(totalTime - gameManager.CurrentTime)}";
        if(totalTime - gameManager.CurrentTime <= 0)
        {
            StartCoroutine(EndOfGame());
        }
    }

    // Update is called once per frame
    void WritePoints(int points)
    {
        PointText.text = $"Your Points: {points}";
    }
    string TimeToString(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }
    void WinOrLose()
    {
        WinText.gameObject.SetActive(true);
        if(staffController.Score >= minimumPointsToWin)
        {
            WinText.text = "You Win!";
        }
        else
        {
            WinText.text = $"You Lose!\nYou need {minimumPointsToWin - staffController.Score} more points to win.";
        }
    }
    IEnumerator EndOfGame()
    {
        // Implement end of game logic here, such as showing a win/lose screen or restarting the game
            TimeRemainingText.gameObject.SetActive(false);
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Bird");
            foreach (GameObject obj in objs)
            {
                Destroy(obj);
            }
            WinOrLose();
            yield return new WaitForSeconds(5);
            WinText.gameObject.SetActive(false);
            SceneManager.LoadScene("MainMenu");
    }
}
