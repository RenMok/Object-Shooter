using UnityEngine;
using UnityEngine.UI;

// Displays on screen how many nukes are available to the player, up to a max of 3
// Holds the count of nukes - any nuke functions should directly reference this count

public class NukeUI : MonoBehaviour
{
    public static NukeUI Singleton;
    [SerializeField] private Image nuke1, nuke2, nuke3;
    [SerializeField] private Image[] nukes = new Image[3];
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = new(Color.white.r, Color.white.g, Color.white.b, 0.45f);
    internal int availableNukes = 0;
    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(this);
            return;
        }

        Singleton = this;
    }
    private void Start()
    {
        if (nuke1 == null || nuke2 == null || nuke3 == null)
        {
            nukes = GetComponentsInChildren<Image>();
            nuke1 = nukes[0];
            nuke2 = nukes[1];
            nuke3 = nukes[2];
        }
        else
        {
            nukes[0] = nuke1;
            nukes[1] = nuke2;
            nukes[2] = nuke3;
        }

        // Set each UI nuke to unavailable state
        foreach (Image nuke in nukes)
        {
            nuke.color = inactiveColor;
        }
    }
    internal void AddNuke()
    {
        // Ensures a maximum of 3 nukes
        if (availableNukes >= 3)
        {
            availableNukes = 3;
            return;
        }
        // Increments nuke counter and activates the visual representation
        else
        {
            availableNukes++;
            nukes[availableNukes - 1].color = activeColor;
        }

    }
    internal void RemoveNuke()
    {
        // Prevents negative number of nukes
        if (availableNukes <= 0)
        {
            availableNukes = 0;
            return;
        }

        // Decrements nuke counter and deactivates associated visual
        else
        {
            nukes[availableNukes - 1].color = inactiveColor;
            availableNukes--;
        }
    }
}
