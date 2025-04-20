using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseExitButton : BaseButton
{
    [SerializeField]
    private GameObject pauseMenuUI;
    protected override void OnClick()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
