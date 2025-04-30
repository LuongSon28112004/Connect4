using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartWithPersonButton : BaseButton
{
    protected override void OnClick()
    {
        Time.timeScale = 1f; // Reset time scale to normal
        SceneManager.LoadScene("Connect4P_P");
    }
}
