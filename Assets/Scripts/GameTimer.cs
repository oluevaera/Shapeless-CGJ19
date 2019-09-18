using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    float currentTimer;
    Text text;

    void Start()
    {
        currentTimer = 300;
        text = GetComponent<Text>();
        StartCoroutine("CountdownTime");
    }

    void Update()
    {
        text.text = "Time: " + currentTimer;
    }

    IEnumerator CountdownTime()
    {
        while (currentTimer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentTimer--;
        }
        // NOTE: Have game ending code called from here!
    }
}
