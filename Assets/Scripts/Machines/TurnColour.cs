using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

[RequireComponent(typeof(Painter))]
public class TurnColour : MachineManager    // Machine that turns the shape into Red.
{
    float countdownValue;
    [SerializeField, Tooltip("How long each cycle takes to colour")] private float timeToColour = 5f;
    [SerializeField, Tooltip("Time between each slice cycle")] private float cooldown = 2f;
    [SerializeField, Tooltip("Block Colour in RGB")] private Color rGB;
    [SerializeField, Tooltip("Block Colour name")] private string rGBName;
    [SerializeField] private Transform shapePosition;
    
    [Header("UI")] 
    [SerializeField] private GameObject ui;
    [SerializeField] private ProgressBar progressBar;
    
    bool isShapeOtherPrimaryColour = false;
    string otherPrimaryName;

    private Coroutine runCoroutine;
    private Shape shape;
    Color colorOriginal;
    string lastColor;
    Painter painter;

    private void Start()
    {
        painter = gameObject.GetComponent<Painter>();
        countdownValue = 0;
    }

    private void Update()
    {
        if (countdownValue > .01f)
        {
            if (countdownValue < timeToColour)
            {
                progressBar.Fill.GetComponent<Image>().color = Color.green;
                progressBar.Progress = countdownValue / timeToColour;
            }
            else
            {
                progressBar.Fill.GetComponent<Image>().color = Color.grey;
                progressBar.Progress = 1f - ((countdownValue - timeToColour) / cooldown);
            }
        }
    }

    IEnumerator ColourObj()
    {
        lastColor = shape.ColourName;
        painter.machineState = 1;
        colorOriginal = shape.Colour;
        
        ui.SetActive(true);
        
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            countdownValue += 0.01f;

            if(countdownValue <= timeToColour)
            {
                if(shape.primaryColour && rGB != shape.Colour )
                {
                    if(otherPrimaryName == null)
                    {
                        otherPrimaryName = shape.ColourName;
                    }
                    shape.colourChange(Color.Lerp(colorOriginal,Color.Lerp( rGB,shape.colourDict[otherPrimaryName],0.5f), (countdownValue / timeToColour)));
                }
                else
                {
                    shape.colourChange(Color.Lerp(colorOriginal, rGB, (countdownValue / timeToColour)));
                }
               
            }



            if (countdownValue >= timeToColour)
            {
                if (otherPrimaryName != null)
                {
                    shape.colourChange(shape.colourDict[ AddColours(otherPrimaryName, rGBName)]);
                }
                else
                {
                    shape.colourChange(rGB);
                }
               
                shape.checkColour();
                lastColor = shape.ColourName;
                painter.machineState = 2;
            }
            if (countdownValue >= timeToColour+cooldown)
            {
                painter.machineState = 1;
                otherPrimaryName = null;
                countdownValue = 0;
            }

        }
    }

    public override bool Interact(PlayerControl player)
    {
        Debug.Log("CallMachine " + gameObject.name);
        // Grab the shape from the player
        if (shape == null && player.CarriedShape != null)
        {
            shape = player.CarriedShape;
            shape.SetHolder(shapePosition);
            player.CarriedShape = null;


            otherPrimaryName = null;
            countdownValue = 0;
            runCoroutine = StartCoroutine(ColourObj());

            return true;
        }

        if (shape != null && player.CarriedShape == null)
        {
            player.CarriedShape = shape;
            shape.SetHolder(player.CarryPosition);
            shape.SetColourByName(lastColor);

            StopCoroutine(runCoroutine);
            ui.SetActive(false);
            shape = null;

            otherPrimaryName = null;
            painter.machineState = 0;
            countdownValue = 0;

            return true;
        }


        return false;
    }
    string AddColours(string nameOne, string nameTwo)
    {
        if (nameOne == "blue" )
        {
            if (nameTwo == "red")
            {
                return "purple";
            }
            if ( nameTwo == "yellow")
            {
                return "green";
            }

        }
        if (nameOne == "red" )
        {
            if ( nameTwo == "blue")
            {
                return "purple";
            }
            if ( nameTwo == "yellow")
            {
                return "orange";
            }

        }
        if (nameOne == "yellow" )
        {
            if ( nameTwo == "red")
            {
                return "orange";
            }
            if ( nameTwo == "blue")
            {
                return "green";
            }

        }
        return nameTwo;

    }
}
