using UnityEngine;

public class PauseButton : BaseButton
{
    [SerializeField]
    private GameObject pauseMenuUI;

    protected override void OnClick()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }
}
