using UnityEngine;
using UnityEngine.SceneManagement;

public class HardButton : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("Hard Button Clicked");
        LevelAIControllder.Instance.LevelAI = 2;
        SceneManager.LoadScene("Connect4P_AI");
    }
}
