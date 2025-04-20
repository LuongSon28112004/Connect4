using UnityEngine;

public class HelpButton : BaseButton
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject HelpMenu;

    protected override void OnClick()
    {
        HelpMenu.SetActive(true);
        Menu.SetActive(false);
    }
}

