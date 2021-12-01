using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public static int numberOfCoins;
    [SerializeField] public GameObject scoreText;
    private TMPro.TextMeshProUGUI ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText = scoreText.GetComponent<TMPro.TextMeshProUGUI>();
        numberOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.SetText("Score: " + numberOfCoins);
    }
}
