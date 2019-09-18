using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MachineManager : MonoBehaviour, IInteractable
{
    public abstract bool Interact(PlayerControl player);


}
