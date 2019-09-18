using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slicer : MachineManager
{
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Done = Animator.StringToHash("Done");

    [SerializeField, Tooltip("How long each cycle takes to slice")] private float timeToSlice = 5f;
    [SerializeField, Tooltip("Time between each slice cycle")] private float cooldown = 2f;
    [SerializeField] private Transform shapePosition;

    [Header("UI")] 
    [SerializeField] private GameObject ui;
    [SerializeField] private ProgressBar progressBar;

    private Animator animator;
    private float startTime = 0f;
    private bool running;
    private Shape shape;
    private Coroutine runRoutine;
    AudioSource slicerCast;
    public AudioClip SlicerCast;
    public AudioClip SlicerDone;
    private bool slicerIsRunning = false;  //To stop the clip for playing on every frame in the update function
    private bool slicerIsDone = false;

    private void Start()
    {
        slicerCast = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (running)
        {
            var diff = Time.time - startTime;
            if (diff < timeToSlice)
            {
                slicerIsDone = false;
                progressBar.Fill.GetComponent<Image>().color = Color.green;
                progressBar.Progress = diff / timeToSlice;
                if (slicerIsRunning == false)
                {
                    slicerCast.clip = SlicerCast;
                    slicerCast.Play();
                    slicerIsRunning = true;
                }

            }
            else
            {
                progressBar.Fill.GetComponent<Image>().color = Color.grey;
                diff -= timeToSlice;
                progressBar.Progress = 1f - (diff / cooldown);
                if (slicerIsDone == false)
                {
                    slicerCast.clip = SlicerDone;
                    slicerCast.Play();
                    slicerIsDone = true;
                }
                slicerIsRunning = false;
            }
        }
    }

    private IEnumerator RunSlicer()
    {
        running = true;
        startTime = Time.time;
        ui.SetActive(true);
        animator.SetTrigger(Run);
        
        yield return new WaitForSeconds(timeToSlice);
        
        animator.SetTrigger(Done);

        // Update shape with new slice and begin cooldown
        if (shape.Reduce() <= 2)
        {
            // todo destroy shape
            running = false;
            ui.SetActive(false);
            yield break;
        }
        
        yield return new WaitForSeconds(cooldown);
        runRoutine = StartCoroutine(RunSlicer());
    }

    public override bool Interact(PlayerControl player)
    {
        // Grab the shape from the player
        if (shape == null && player.CarriedShape != null)
        {
            Debug.Log("Shape inserted");
            shape = player.CarriedShape;
            shape.SetHolder(shapePosition);
            player.CarriedShape = null;
            
            // Start slicing
            runRoutine = StartCoroutine(RunSlicer());

            return true;
        }
        // Output the shape if interacted with
        if (shape != null && player.CarriedShape == null)
        {
            Debug.Log("Collected shape");
            player.CarriedShape = shape;
            shape.SetHolder(player.CarryPosition);
            shape = null;
            
            StopCoroutine(runRoutine);
            running = false;
            ui.SetActive(false);
            slicerIsRunning = false;
            slicerCast.Stop();
            if (Time.time - startTime < timeToSlice)
            {
                animator.SetTrigger(Done);
            }

            return true;
        }

        return false;
    }
}
