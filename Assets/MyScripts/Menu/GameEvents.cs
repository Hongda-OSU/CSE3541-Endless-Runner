using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    [SerializeField] public GameObject timeCounter;
    private TMPro.TextMeshProUGUI TimerText;
    void Start()
    {
        TimerText = timeCounter.GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void Resume()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        MenuManager.isGamePaused = false;
        timeCounter.SetActive(true);
        int counter = 3;
        while (counter > 0)
        {
            TimerText.SetText(counter + "...");
            yield return new WaitForSecondsRealtime(1);
            counter--;
        }
        Time.timeScale = 1;
        timeCounter.SetActive(false);
        MenuManager.isPressed = false;
    }

    public void StartOver()
    {
        Time.timeScale = 1;
        AudioManager.instance.Stop("GameOver");
        AudioManager.instance.Stop("MainTheme");
        AudioManager.instance.Play("GameStart");

        SceneManager.LoadScene("Endless Runner");
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
