using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MachineManager
{
    public GameObject hexagon;

    public override bool Interact(PlayerControl player)
    {
        // todo
        Instantiate(hexagon);
        return true;
    }
}
