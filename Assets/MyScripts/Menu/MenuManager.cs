using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public GameObject startingText;
    [SerializeField] public GameObject gamePauseMenu;
    [SerializeField] public GameObject mileText;
    public static bool isGameStarted, isGamePaused, isPressed;
    public static bool gameOver;
    private bool isClicked;
    private Animator animator;
    private TMPro.TextMeshProUGUI UIText, MileText;

    void Start()
    {
        gameOver = false;
        isGameStarted = false;
        isGamePaused = false;
        isPressed = false;
        isClicked = false;
        gameOverPanel.SetActive(false);
        gamePauseMenu.SetActive(false);
        mileText.SetActive(false);
        startingText.SetActive(true);
        UIText = startingText.GetComponent<TMPro.TextMeshProUGUI>(); // how to reference TextMeshPro object
        MileText = mileText.GetComponent<TMPro.TextMeshProUGUI>();
        animator = character.GetComponent<Animator>();
        animator.SetBool("IsDance", true);
        animator.SetInteger("danceCount", Random.Range(1,6));
    }

    void Update()
    {
        if (gameOver)
        {
            mileText.SetActive(false);
            gameOverPanel.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0) && !isClicked)
        {
            isClicked = true;
            StartCoroutine(StartCountDown());
        }
        if (isGameStarted && !gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !isPressed)
            {
                Time.timeScale = 0;
                isPressed = true;
                isGamePaused = true;
                gamePauseMenu.SetActive(true);
            }
        }
        if(!isGamePaused)
        {
            gamePauseMenu.SetActive(false);
        }

        if (mileText)
        {
            int mile = (int) character.transform.position.z;
            MileText.SetText(mile.ToString() + " MILES");
        }
    }

    IEnumerator StartCountDown()
    {
        int counter = 3;
        while (counter > 0)
        {
            UIText.SetText(counter.ToString()); // how to modify the text in TextMeshPro object
            yield return new WaitForSeconds(1);
            counter--;
        }
        isGameStarted = true;
        animator.SetBool("IsDance", false);
        mileText.SetActive(true);
        Destroy(startingText);
    }

}

