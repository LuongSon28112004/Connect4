using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyButton : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("Easy Button Clicked");
        LevelAIControllder.Instance.LevelAI = 0;
        SceneManager.LoadScene("Connect4P_AI");
    }
}
