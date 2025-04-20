using UnityEngine;
using UnityEngine.SceneManagement;

public class WithPersonButton : BaseButton
{
    protected override void OnClick()
    {
        SceneManager.LoadScene("Connect4P_P");
    }
}
