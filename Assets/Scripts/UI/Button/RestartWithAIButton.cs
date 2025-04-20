using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartWithAIButton : BaseButton
{
    protected override void OnClick()
    {
        SceneManager.LoadScene("Connect4P_AI");
    }
}
