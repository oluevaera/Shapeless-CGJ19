using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{

    public GameObject paintingPart;
    public GameObject paintingDonePart;

    AudioSource paintCast;
    public AudioClip painterCast;
    public AudioClip painterDone;
    private bool painterIsRunning = false;  //To stop the clip for playing on every frame in the update function
    private bool painterIsDone = false;

    public int machineState = 0;


    // Start is called before the first frame update
    void Start()
    {
        paintCast = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (machineState)
        {
            case 0:
                paintingDonePart.SetActive(false);
                paintingPart.SetActive(false);                         
                break;
            case 1:
                paintingDonePart.SetActive(false);
                paintingPart.SetActive(true);
                if (painterIsRunning == false)
                {
                    paintCast.clip = painterCast;
                    paintCast.Play();
                    painterIsRunning = true;
                }
                painterIsDone = false;
                break;
            case 2:
                paintingDonePart.SetActive(true);
                paintingPart.SetActive(false);
                if (painterIsDone == false)
                {
                    paintCast.clip = painterDone;
                    paintCast.Play();
                    painterIsDone = true;
                }
                painterIsRunning = false;

                break;
            default:
                break;
        }
    }
}
