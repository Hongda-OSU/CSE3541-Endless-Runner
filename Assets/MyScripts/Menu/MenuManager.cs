using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject character;
    public GameObject gameOverPanel;
    public GameObject startingText;
    public static bool isGameStarted;
    public static bool gameOver;
    private bool isClicked;
    private Animator animator;
    private TMPro.TextMeshProUGUI UIText;

    void Start()
    {
        gameOver = false;
        isGameStarted = false;
        gameOverPanel.SetActive(false);
        startingText.SetActive(true);
        UIText = startingText.GetComponent<TMPro.TextMeshProUGUI>(); // how to reference TextMeshPro object
        animator = character.GetComponent<Animator>();
        animator.SetBool("IsDance", true);
        animator.SetInteger("danceCount", Random.Range(1,5));
    }

    void Update()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0) && !isClicked)
        {
            isClicked = true;
            StartCoroutine(StartCountDown());
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
        Destroy(startingText);
    }
}

