using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private RectTransform panelHighlight;
    [SerializeField] private Animator highlightAnimator;
    private void Update()
    {
        HighlightPanel();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Acknowledgements()
    {
        SceneManager.LoadScene(3);
    }
    public void AdjustSoundLevel(float level)
    {
        AudioListener.volume = level;
    }
    private void HighlightPanel()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(panelHighlight, Input.mousePosition))
        {
            highlightAnimator.SetBool("Highlighted", true);
        }
        else
        {
            highlightAnimator.SetBool("Highlighted", false);
        }

    }
}
