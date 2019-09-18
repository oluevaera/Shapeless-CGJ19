using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taker : MachineManager
{
    private Shape shape;
    private Coroutine runRoutine;
    [SerializeField] private Transform shapePosition;
    [SerializeField] private Animator animator;



    private IEnumerator RunTake()
    {

        animator.SetTrigger("Take");


        yield return new WaitForSeconds((1f/30f)*(28f));


        Destroy(shape.gameObject);

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

            runRoutine = StartCoroutine(RunTake());



            return true;
        }

       


        return false;
    }
}
