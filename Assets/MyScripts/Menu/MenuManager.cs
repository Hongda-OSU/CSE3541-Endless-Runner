using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    void Start()
    {
        gameOver = false;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
        }
    }
}
