
using UnityEngine;

public class BackButton : BaseButton
{
    [SerializeField] private GameObject levelUI;
    [SerializeField] private GameObject mainUI;
    protected override void OnClick()
    {
        mainUI.SetActive(true);
        levelUI.SetActive(false);
    }
}
