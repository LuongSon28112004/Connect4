using UnityEngine;
using UnityEngine.SceneManagement;

public class WithAIButton : BaseButton
{
    [SerializeField] private GameObject levelUI;
    [SerializeField] private GameObject mainUI;
    protected override void OnClick()
    {
        mainUI.SetActive(false);
        levelUI.SetActive(true);
    }
}
