
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour
{
    public static BuffUI Singleton;
    [SerializeField] private Image buffImage;
    [SerializeField] private Vector3 buffOffset;
    private Coroutine buffCoroutine = null;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else if (Singleton != null && Singleton != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        buffImage.gameObject.SetActive(false);
    }

    // Enables a UI element displaying the current time remaining on a buff
    private IEnumerator StartBuff(float endTime, Player recipient)
    {
        if (!buffImage.gameObject.activeSelf)
        {
            buffImage.gameObject.SetActive(true);
        }
        // UI icon starts full
        buffImage.fillAmount = 1;
        float timer = 0;

        while (timer < endTime)
        {
            if (recipient == null)
            {
                buffImage.gameObject.SetActive(false);
                yield break;
            }
            // Update on-screen placement
            buffImage.transform.position = recipient.GetBuffPlacement();

            // Icon wipes away clockwise over duration of buff
            buffImage.fillAmount -= 1 / endTime * Time.deltaTime;
            timer += Time.deltaTime;

            // Allows the coroutine to continue looping into the next frame
            yield return null;
        }
        // Reset once buff is depleted
        buffImage.fillAmount = 0;
        buffImage.gameObject.SetActive(false);
        buffCoroutine = null;

    }
    // Called from pickup object
    // Starting the coroutine from the UI prevents it from stopping when the pickup object is destroyed
    public void ActivateBuffUI(float endTime, Player recipient)
    {
        // Restart buff if there is already one in progress
        if (buffCoroutine != null)
        {
            StopCoroutine(buffCoroutine);
        }
        buffCoroutine = StartCoroutine(StartBuff(endTime, recipient));
    }

}
