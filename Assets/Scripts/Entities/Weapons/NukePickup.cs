public class NukePickup : PickUp
{
    protected override void PickMe(Player user)
    {
        // Nuke counter is held in a centralized location - the UI
        NukeUI.Singleton.AddNuke();
        Destroy(gameObject);
    }
}
