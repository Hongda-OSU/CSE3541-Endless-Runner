using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public static int numberOfCoins;
    public Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        numberOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "Score: " + numberOfCoins;
    }
}
