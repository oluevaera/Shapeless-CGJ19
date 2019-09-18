public interface IInteractable
{
    /// <summary>
    /// Called when the player interacts with the object 
    /// </summary>
    /// <param name="player"></param>
    /// <returns>Whether the interact is consumed</returns>
    bool Interact(PlayerControl player);
}