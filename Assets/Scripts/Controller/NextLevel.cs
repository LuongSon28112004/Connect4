using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public void personWithPerson()
    {
        SceneManager.LoadScene("Connect4P_P");
    }

    public void personWithBotAI()
    {
        SceneManager.LoadScene("Connect4P_AI");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
