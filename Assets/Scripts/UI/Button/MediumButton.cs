
using UnityEngine;
using UnityEngine.SceneManagement;

public class MediumButton : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("Medium Button Clicked");
        LevelAIControllder.Instance.LevelAI = 1;
        SceneManager.LoadScene("Connect4P_AI");
    }
}
