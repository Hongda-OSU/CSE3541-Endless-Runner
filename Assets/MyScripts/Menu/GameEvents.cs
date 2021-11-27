using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartOver()
    {
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
