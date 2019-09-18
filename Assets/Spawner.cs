using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MachineManager
{
    private Shape shape;
    private Coroutine runRoutine;
    public GameObject shapePrefab;
    [SerializeField] private Transform shapePosition;

    [SerializeField] private Animator animator;

    void Start()
    {
        runRoutine = StartCoroutine(RunPlace());
    }

    private IEnumerator RunPlace()
    {

        animator.SetTrigger("Place");
        GameObject obj = Instantiate(shapePrefab, shapePosition.position, shapePosition.rotation, shapePosition);
        obj.GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds((1f / 30f) * (28f));
        obj.transform.SetParent(null);


    }

    public override bool Interact(PlayerControl player)
    {




        // Output the shape if interacted with
        if (shape != null && player.CarriedShape == null)
        {
            Debug.Log("Collected shape");
            player.CarriedShape = shape;
            shape.SetHolder(player.CarryPosition);
            shape = null;

            runRoutine = StartCoroutine(RunPlace());


            return true;
        }

        return false;
    }
}

