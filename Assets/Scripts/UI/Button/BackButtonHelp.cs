using UnityEngine;

public class BackButtonHelp : BaseButton
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject HelpMenu;

    protected override void OnClick()
    {
        HelpMenu.SetActive(false);
        Menu.SetActive(true);
    }
}
