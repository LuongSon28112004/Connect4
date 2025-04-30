using System;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 15f; // thời gian bắt đầu (giây)
    public TextMeshProUGUI countdownText;
    public bool isCounting = true;
    public int timerLast = 3;

    public event Action OnTimeUp;

    void Update()
    {
        if (isCounting && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateCountdownText();
        }
        else if (isCounting)
        {
            timeRemaining = 0;
            isCounting = false;
            countdownText.text = "0";
            OnTimeUp?.Invoke();
        }
    }

    void UpdateCountdownText()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        Debug.Log("Time remaining: " + seconds);
        if (seconds == timerLast)
        {
            SoundEffectMananger.Instance.PlaySound("countdown");
        }
        countdownText.text = seconds.ToString();
    }

    public void ResetTimer(float newTime)
    {
        timeRemaining = newTime;
        isCounting = true;
        UpdateCountdownText();
    }
}
