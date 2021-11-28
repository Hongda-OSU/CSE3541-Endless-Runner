using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject character;
    public GameObject gameOverPanel;
    public GameObject startingText;
    public static bool isGameStarted;
    public static bool gameOver;
    private Animator animator;
    void Start()
    {
        gameOver = false;
        isGameStarted = false;
        gameOverPanel.SetActive(false);
        startingText.SetActive(true);
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
        if (Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            animator.SetBool("IsDance", false);
            Destroy(startingText);
        }
    }
}
