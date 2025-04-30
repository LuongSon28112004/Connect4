using System;
using UnityEngine;

public class GameCountDownController : MonoBehaviour
{
    public static GameCountDownController Instance { get; private set; }
    public GameObject Player1Timer { get => player1Timer; }
    public GameObject Player2Timer { get => player2Timer; }

    [SerializeField] private GameObject player1Timer;
    [SerializeField] private GameObject player2Timer;


    private void Start()
    {
        GameController.Instance.OnTurnChangePlayer += StartCountDown;
    }

    private void StartCountDown(int obj)
    {
        if (obj == 0)
        {
            Debug.Log("Start countdown for player 1");
            player1Timer.SetActive(true);
            CountDownTimerPlayer_1 countDownTimerPlayer_1 = player1Timer.GetComponent<CountDownTimerPlayer_1>();
            if(countDownTimerPlayer_1 != null)
            {
                Debug.Log("Resetting timer for player 1");
                countDownTimerPlayer_1.ResetTimer(15f); // Reset timer for player 1
            }
            player2Timer.SetActive(false);
        }
        else if (obj == 1)
        {
            Debug.Log("Start countdown for player 2");
            player1Timer.SetActive(false);
            player2Timer.SetActive(true);
            CountDownTimerPlayer_2 countDownTimerPlayer_2 = player2Timer.GetComponent<CountDownTimerPlayer_2>();
            if(countDownTimerPlayer_2 != null)
            {
                Debug.Log("Resetting timer for player 2");
                countDownTimerPlayer_2.ResetTimer(15f); // Reset timer for player 2
            }
        }
    }
}
