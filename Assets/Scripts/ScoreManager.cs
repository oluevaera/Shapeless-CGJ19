using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;    // Player score
    Text text;                  // Reference to Text component

    void Start()
    {
        text = GetComponent<Text>();
        score = 0;
    }

    void Update()
    {
        text.text = "Score: " + score;
    }

    public void IncreaseScore(int number)
    {
        score = score + number;
    }

    public void DecreaseScore(int number)
    {
        score = score - number;
        if (score < 0) { score = 0; }
    }
}
