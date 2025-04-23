using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class MiniMaxAIEasy : MonoBehaviour
{
    public static MiniMaxAIEasy Instance { get; private set; }
    private const int ROWS = 6;
    private const int COLS = 7;
    private const int MAX_DEPTH = 5;
    private const int AI_PLAYER = 2;
    private const int HUMAN_PLAYER = 1;
    private const int EMPTY = 0;
    public int[,] boardMiniMax;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        boardMiniMax = new int[ROWS, COLS];
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                boardMiniMax[i, j] = 0;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Chỉ in khi nhấn phím Space
        {
            StartCoroutine(PrintBoard());
        }
    }

    private IEnumerator PrintBoard()
    {
        // In ma trận
        for (int i = 0; i < ROWS; i++)
        {
            string rowText = "";
            for (int j = 0; j < COLS; j++)
            {
                rowText += boardMiniMax[i, j] + " ";
            }
            Debug.Log(rowText);
            yield return null; // Tạm dừng trong 1 frame để Unity có thời gian in ra màn hình
        }

        // Tiến hành công việc khác sau khi in xong
        Debug.Log("Done printing the board!");
    }

    public int GetBestMove()
    {
        int bestMove = -1;
        int bestScore = int.MinValue;

        for (int col = 0; col < COLS; col++)
        {
            if (IsValidMove(boardMiniMax, col))
            {
                int row = GetNextOpenRow(boardMiniMax, col);
                boardMiniMax[row, col] = AI_PLAYER;
                int score = Minimax(boardMiniMax, MAX_DEPTH, false); // Không dùng alpha, beta
                boardMiniMax[row, col] = EMPTY;

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = col;
                }
            }
        }
        return bestMove;
    }

    private int Minimax(int[,] board, int depth, bool isMaximizing)
    {
        if (depth == 0 || IsTerminalNode(board))
            return EvaluateBoard(board);

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            for (int col = 0; col < COLS; col++)
            {
                if (IsValidMove(board, col))
                {
                    int row = GetNextOpenRow(board, col);
                    board[row, col] = AI_PLAYER;
                    int eval = Minimax(board, depth - 1, false);
                    board[row, col] = EMPTY;
                    maxEval = Math.Max(maxEval, eval);
                }
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            for (int col = 0; col < COLS; col++)
            {
                if (IsValidMove(board, col))
                {
                    int row = GetNextOpenRow(board, col);
                    board[row, col] = HUMAN_PLAYER;
                    int eval = Minimax(board, depth - 1, true);
                    board[row, col] = EMPTY;
                    minEval = Math.Min(minEval, eval);
                }
            }
            return minEval;
        }
    }

    private int EvaluateBoard(int[,] board)
    {
        // Viết hàm đánh giá điểm của bàn cờ, càng gần thắng thì điểm càng cao.
        return new System.Random().Next(-100, 100); // Placeholder, viết lại theo logic đánh giá của bạn
    }

    private bool IsTerminalNode(int[,] board)
    {
        // Kiểm tra có ai thắng hoặc bàn cờ đầy chưa.
        return false; // Placeholder, viết lại logic kiểm tra điều kiện thắng thua
    }

    private bool IsValidMove(int[,] board, int col)
    {
        return board[0, col] == EMPTY;
    }

    private int GetNextOpenRow(int[,] board, int col)
    {
        for (int row = ROWS - 1; row >= 0; row--)
        {
            if (board[row, col] == EMPTY)
                return row;
        }
        return -1;
    }
}
