using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject GameWinScreen;
    public Button defaultPauseButton;
    public Button winDefaultButton;
    public Text winText;

    public bool paused = false;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        defaultPauseButton.Select();
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void Restart()
    {
        Unpause();
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Win(PlayerNumber player)
    {
        Time.timeScale = 0f;
        GameWinScreen.SetActive(true);
        winText.text = "Player " + (player.Player == PlayerNumber.Number.P1 ? "1" : "2") + " Wins!";
        winDefaultButton.Select();
    }

    public void Lose(PlayerNumber player)
    {
        Time.timeScale = 0f;
        GameWinScreen.SetActive(true);
        winText.text = "Player " + (player.Player == PlayerNumber.Number.P2 ? "1" : "2") + " Wins!";
        winDefaultButton.Select();
    }
}
