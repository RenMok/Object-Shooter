using System.Collections;
using UnityEngine;

public class IntroAnimationController : MonoBehaviour
{
    [SerializeField] private Animator[] animators;
    [SerializeField] private float offsetTime;
    private void Awake()
    {
        foreach (Animator animator in animators)
        {
            animator.enabled = false;
        }
    }
    private void Start()
    {
        StartCoroutine(OffsetActivation());

    }
    private IEnumerator OffsetActivation()
    {
        foreach (Animator animator in animators)
        {
            yield return new WaitForSecondsRealtime(offsetTime);
            animator.enabled = true;
        }
    }
}
