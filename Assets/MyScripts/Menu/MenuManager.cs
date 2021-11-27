using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public GameObject startingText;
    void Start()
    {
        gameOver = false;
        isGameStarted = false;
        gameOverPanel.SetActive(false);
        startingText.SetActive(true);
    }

    void Update()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }
}
