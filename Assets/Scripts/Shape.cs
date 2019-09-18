using System;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour, IInteractable
{
    // Not ideal but will work for now
    public List<Mesh> ShapeMeshes = new List<Mesh>();
    
    public int Edges = 6;
    public Color Colour;
    public string ColourName = "white";
    public bool primaryColour = false;



    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Collider collider;
    private Rigidbody rigidbody;
    
    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();


        colourDict.Add("white", Color.white);
        colourDict.Add("red", Color.red);
        colourDict.Add("yellow", new Color(1, 1, 0));
        colourDict.Add("blue", Color.blue);
        colourDict.Add("green", Color.green);
        colourDict.Add("orange", new Color(1, 0.5f, 0));
        colourDict.Add("purple", new Color(1, 0, 1));
        
        UpdateShape();
    }

    /// <summary>
    /// Updates the mesh based on the current edges and colour
    /// </summary>
    public void UpdateShape()
    {
        var index = 6 - Edges;
        meshFilter.mesh = ShapeMeshes[index];
        meshRenderer.material.color = Colour;
        checkColour();
    }

    public int Reduce() // Procedure to lower the efges of the object by 1.
    {
        Edges -= 1;

        if (Edges <= 2)
        {
            return 0; // command to destroy the shape
        }

        UpdateShape();
        return Edges;
    }

    public void colourChange(Color newColor) // Procedure to change the current colour into a new one.
    {
        Colour = newColor;
        UpdateShape();
    }
    public void checkColour()
    {
        if(Colour == colourDict["red"])
        {
            ColourName = "red";
            primaryColour = true;
        }
        else if(Colour == colourDict["white"])
        {
            ColourName = "white";
            primaryColour = false;
        }
        else if(Colour == colourDict["yellow"])
        {
            ColourName = "yellow";
            primaryColour = true;
        }
        else if(Colour == colourDict["blue"])
        {
            ColourName = "blue";
            primaryColour = true;
        }
        else if(Colour == colourDict["purple"])
        {
            ColourName = "purple";
            primaryColour = false;
        }
        else if(Colour == colourDict["orange"])
        {
            ColourName = "orange";
            primaryColour = false;
        }
        else if(Colour == colourDict["green"])
        {
            ColourName = "green";
            primaryColour = false;
        }
    }
    public void SetColourByName(string name)
    {
        switch (name)
        {
            case "white":
                Colour = colourDict["white"];
                break;
            case "red":
                Colour = colourDict["red"];
                break;
            case "yellow":
                Colour = colourDict["yellow"];
                break;
            case "blue":
                Colour = colourDict["blue"];
                break;
            case "green":
                Colour = colourDict["green"];
                break;
            case "orange":
                Colour = colourDict["orange"];
                break;
            case "purple":
                Colour = colourDict["purple"];
                break;
            default:
                break;
        }
        UpdateShape();
    }


    public bool Interact(PlayerControl player)
    {
        if (player.CarriedShape == null)
        {
            SetHolder(player.CarryPosition);
            player.CarriedShape = this;
        }
        else
        {
            transform.SetParent(null);
            collider.enabled = true;
            rigidbody.isKinematic = false;
            player.CarriedShape = null;
        }
        return true;
    }

    public void SetHolder(Transform parent)
    {
        transform.position = parent.position;
        transform.rotation = Quaternion.identity;
        transform.SetParent(parent.transform, true);
        collider.enabled = false;
        rigidbody.isKinematic = true;
    }


    public Dictionary<string, Color> colourDict = new Dictionary<string, Color>();

}
