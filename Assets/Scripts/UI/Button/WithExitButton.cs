using UnityEngine;

public class WithExitButton : BaseButton
{
    protected override void OnClick()
    {
        Application.Quit();
    }
}
