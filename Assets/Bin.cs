using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MachineManager
{
    private Shape shape;
    private Coroutine runRoutine;
    [SerializeField] private Transform shapePosition;
    [SerializeField] private GameObject particle;



    private IEnumerator RunBin()
    {
        particle.SetActive(true);
        float time = 1f;
        float count = 0f;
        while (count < time)
        {
            yield return new WaitForSeconds(0.05f);
            shape.transform.Translate(new Vector3(0,-0.01f,0));
            count += 0.05f;
        }
        Destroy(shape.gameObject);
        particle.SetActive(false);
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

            runRoutine = StartCoroutine(RunBin());

            

            return true;
        }


        return false;
    }
}
