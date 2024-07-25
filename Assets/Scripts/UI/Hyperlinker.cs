using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hyperlinker : Button
{
    [SerializeField] private TextMeshProUGUI textbox;
    private string[] links;

    protected override void Start()
    {
        base.Start();
        links = FindLinks();

    }
    private string[] FindLinks()
    {
        return textbox.text.Split("\n");
    }
    // Callback for handling clicks.
    public override void OnPointerClick(PointerEventData eventData)
    {
        var lineNumber = TMP_TextUtilities.FindIntersectingLine(textbox, eventData.position, null);

        var url = links[lineNumber];

        Application.OpenURL(url);
    }
}
