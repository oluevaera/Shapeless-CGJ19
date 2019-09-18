using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform shapePosition;
    
    private Shape shape;

    public bool Interact(PlayerControl player)
    {
        // Grab the shape from the player
        if (shape == null && player.CarriedShape != null)
        {
            shape = player.CarriedShape;
            shape.SetHolder(shapePosition);
            player.CarriedShape = null;

            return true;
        }
        // Output the shape if interacted with
        if (shape != null && player.CarriedShape == null)
        {
            player.CarriedShape = shape;
            shape.SetHolder(player.CarryPosition);
            shape = null;

            return true;
        }

        return false;
    }
}
