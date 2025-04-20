using UnityEngine;

public class ResumeButton : BaseButton
{
    [SerializeField]
    private GameObject pauseMenuUI;
    protected override void OnClick()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
    }
}
