using UnityEngine;
using UnityEngine.SceneManagement;

public class Acknowledgements : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ToMainMenu();   
        }
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
