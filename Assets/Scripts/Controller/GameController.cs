using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Game Settings")]
    public Transform[] dots;
    public GameObject[] player;
    public GameObject gameOverPanel,
        playerOneWinsText,
        playerTwoWinsText,
        playerTurnImage,
        // undoButton,
        tieText;
    public int[] tokensCount;
    bool gameOver;
    GameObject playerToken;
    int turn; //lastColumn

    [SerializeField]
    private GameCountDownController gameCountDownController;

    public int Turn
    {
        get { return turn; }
    }

    public event Action<int> OnTurnChangePlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        this.LoadDots();
        this.resetToken();
    }

    private void LoadDots()
    {
        dots = new Transform[42];
        GameObject dotPrefabs = GameObject.Find("dots");
        int i = 0;
        foreach (Transform item in dotPrefabs.transform)
        {
            dots[i] = item;
            i++;
        }
    }

    public void resetToken()
    {
        tokensCount = new int[7];
        for (int i = 0; i < 7; i++)
        {
            tokensCount[i] = 0;
        }
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        tieText.SetActive(false);
        playerOneWinsText.SetActive(false);
        playerTwoWinsText.SetActive(false);
        gameOver = false;

        turn = 0;
        OnTurnChangePlayer?.Invoke(turn);
        playerTurnImage.SetActive(false);

        addCountDownEvent();
    }

    private void addCountDownEvent()
    {
        if (SceneManager.GetSceneByName("Connect4P_P").isLoaded)
        {
            CountDownTimerPlayer_1 countDownTimerPlayer_1 = gameCountDownController.Player1Timer.GetComponent<CountDownTimerPlayer_1>();
            CountDownTimerPlayer_2 countDownTimerPlayer_2 = gameCountDownController.Player2Timer.GetComponent<CountDownTimerPlayer_2>();
            countDownTimerPlayer_1.OnTimeUp += () => gameOverFunction(1 - turn);
            countDownTimerPlayer_2.OnTimeUp += () => gameOverFunction(1 - turn);
        }
    }

    void addAnimation(int linesLeft)
    {
        Animation anim = playerToken.GetComponent<Animation>();

        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        clip.ClearCurves();

        float yValue = (float)(linesLeft * 1.9);
        float speed = (float)(linesLeft * 0.15F);

        AnimationCurve curve = AnimationCurve.Linear(0.0F, yValue, speed, 0);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
    }

    bool vertical(int postitionOfToken, int turn)
    {
        if (postitionOfToken < 21)
            return false;

        for (int i = 1; i <= 3; i++)
        {
            int postitionOfNextToken = postitionOfToken - i * 7;
            if (dots[postitionOfNextToken].childCount == 0)
                return false;
            if (string.Compare(dots[postitionOfNextToken].GetChild(0).tag, turn.ToString()) != 0)
                return false;
        }
        return true;
    }

    int horizontalSearch(int column, int postitionOfToken, int turn, bool direction)
    {
        if (
            dots[postitionOfToken].childCount > 0
            && string.Compare(dots[postitionOfToken].GetChild(0).tag, turn.ToString()) == 0
        )
        {
            if (direction)
            {
                if (column > 0)
                    return 1 + horizontalSearch(column - 1, postitionOfToken - 1, turn, true);
                return 1;
            }
            else
            {
                if (column < 6)
                    return 1 + horizontalSearch(column + 1, postitionOfToken + 1, turn, false);
                return 1;
            }
        }
        return 0;
    }

    bool horizontal(int column, int postitionOfToken, int turn)
    {
        int sum = 0;

        sum += horizontalSearch(column, postitionOfToken, turn, false);
        if (column > 0)
            sum += horizontalSearch(column - 1, postitionOfToken - 1, turn, true);

        if (sum >= 4)
            return true;
        return false;
    }

    int mainDiagonalSearch(int column, int postitionOfToken, int turn, bool direction)
    {
        if (
            dots[postitionOfToken].childCount > 0
            && string.Compare(dots[postitionOfToken].GetChild(0).tag, turn.ToString()) == 0
        )
        {
            if (direction)
            {
                if (column > 0 && (postitionOfToken + 6) >= 0 && (postitionOfToken + 6) <= 41)
                    return 1 + mainDiagonalSearch(column - 1, postitionOfToken + 6, turn, true);
                return 1;
            }
            else
            {
                if (column < 6 && (postitionOfToken - 6) >= 0 && (postitionOfToken - 6) <= 41)
                    return 1 + mainDiagonalSearch(column + 1, postitionOfToken - 6, turn, false);
                return 1;
            }
        }
        return 0;
    }

    bool mainDiagonal(int column, int postitionOfToken, int turn)
    {
        int sum = 1;

        if (column > 0 && (postitionOfToken + 6) >= 0 && (postitionOfToken + 6) <= 41)
            sum += mainDiagonalSearch(column - 1, postitionOfToken + 6, turn, true);
        if (column < 6 && (postitionOfToken - 6) >= 0 && (postitionOfToken - 6) <= 41)
            sum += mainDiagonalSearch(column + 1, postitionOfToken - 6, turn, false);

        if (sum >= 4)
            return true;
        return false;
    }

    int secondaryDiagonalSearch(int column, int postitionOfToken, int turn, bool direction)
    {
        if (
            dots[postitionOfToken].childCount > 0
            && string.Compare(dots[postitionOfToken].GetChild(0).tag, turn.ToString()) == 0
        )
        {
            if (direction)
            {
                if (column < 6 && (postitionOfToken + 8) >= 0 && (postitionOfToken + 8) <= 41)
                    return 1
                        + secondaryDiagonalSearch(column + 1, postitionOfToken + 8, turn, true);
                return 1;
            }
            else
            {
                if (column > 0 && (postitionOfToken - 8) >= 0 && (postitionOfToken - 8) <= 41)
                    return 1
                        + secondaryDiagonalSearch(column - 1, postitionOfToken - 8, turn, false);
                return 1;
            }
        }
        return 0;
    }

    bool secondaryDiagonal(int column, int postitionOfToken, int turn)
    {
        int sum = 1;

        if (column < 6 && (postitionOfToken + 8) >= 0 && (postitionOfToken + 8) <= 41)
            sum += secondaryDiagonalSearch(column + 1, postitionOfToken + 8, turn, true);
        if (column > 0 && (postitionOfToken - 8) >= 0 && (postitionOfToken - 8) <= 41)
            sum += secondaryDiagonalSearch(column - 1, postitionOfToken - 8, turn, false);

        if (sum >= 4)
            return true;
        return false;
    }

    bool checkIfGameOver(int column, int postitionOfToken, int turn)
    {
        if (vertical(postitionOfToken, turn))
        {
            return true;
        }
        if (horizontal(column, postitionOfToken, turn))
        {
            return true;
        }
        if (mainDiagonal(column, postitionOfToken, turn))
        {
            return true;
        }
        if (secondaryDiagonal(column, postitionOfToken, turn))
        {
            return true;
        }

        return false;
    }

    void gameOverFunction(int turnTemp)
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.SetAsLastSibling();
        if (turnTemp == -1)
            tieText.SetActive(true);
        else if (turnTemp == 0)
            playerOneWinsText.SetActive(true);
        else
            playerTwoWinsText.SetActive(true);
    }

    void gameOverFunction()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.SetAsLastSibling();
        if (turn == -1)
            tieText.SetActive(true);
        else if (turn == 0)
            playerOneWinsText.SetActive(true);
        else
            playerTwoWinsText.SetActive(true);
    }

    bool tie()
    {
        for (int i = 0; i < 7; i++)
            if (tokensCount[i] < 6)
                return false;
        return true;
    }

    void changePlayer()
    {
        turn = 1 - turn;
        OnTurnChangePlayer?.Invoke(turn);
        playerTurnImage.SetActive(!playerTurnImage.activeInHierarchy);
    }

    public void arrow(int column)
    {
        if (tokensCount[column] < 6 && !gameOver)
        {
            int postitionOfToken = column + 7 * tokensCount[column];
            if (LevelAIControllder.Instance.LevelAI == 0)
            {
                if (MiniMaxAIEasy.Instance != null)
                {
                    if (turn == 0)
                        MiniMaxAIEasy.Instance.boardMiniMax[5 - tokensCount[column], column] = 1;
                    else if (turn == 1)
                        MiniMaxAIEasy.Instance.boardMiniMax[5 - tokensCount[column], column] = 2;
                }
            }
            else if (LevelAIControllder.Instance.LevelAI == 1)
            {
                Debug.Log("AI");
                if (MiniMaxAIMedium.Instance != null)
                {
                    if (turn == 0)
                        MiniMaxAIMedium.Instance.board[5 - tokensCount[column], column] = 1;
                    else if (turn == 1)
                        MiniMaxAIMedium.Instance.board[5 - tokensCount[column], column] = 2;
                }
            }

            playerToken = Instantiate(player[turn], dots[postitionOfToken]);
            playerToken.SetActive(true);
            SoundEffectMananger.Instance.PlaySound("turn");

            addAnimation(6 - tokensCount[column]);

            if (checkIfGameOver(column, postitionOfToken, turn))
                gameOverFunction();
            else
            {
                tokensCount[column]++;

                if (tie())
                {
                    Debug.Log("tie");
                    turn = -1;
                    gameOverFunction();
                }
                else
                {
                    changePlayer();
                }
            }
        }
    }

    // public void undo()
    // {
    //     if (lastColumn > -1 && !gameOver)
    //     {
    //         Destroy(playerToken);
    //         tokensCount[lastColumn]--;
    //         lastColumn = -1;
    //         changePlayer();
    //     }
    // }
}
