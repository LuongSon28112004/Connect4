using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiniMaxAIHard : MonoBehaviour
{
    public static MiniMaxAIHard Instance { get; private set; }
    private const int ROW_COUNT = 6;
    private const int COLUMN_COUNT = 7;
    private const int EMPTY = 0;
    private const int AI_PLAYER = 2;
    private const int HUMAN_PLAYER = 1;

    public int[,] board;
    private int depth = 7; // tăng độ sâu hơn bản Medium

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        board = new int[ROW_COUNT, COLUMN_COUNT];
        for (int i = 0; i < ROW_COUNT; i++)
        {
            for (int j = 0; j < COLUMN_COUNT; j++)
            {
                board[i, j] = 0;
            }
        }
    }

    public int GetBestMove()
    {
        int bestScore = int.MinValue;
        int bestColumn = 3; // bắt đầu từ cột trung tâm

        foreach (int col in GetColumnPriorityOrder())
        {
            if (IsValidMove(board, col))
            {
                int row = GetNextOpenRow(board, col);
                board[row, col] = AI_PLAYER;

                int score = Minimax(board, depth - 1, false, int.MinValue, int.MaxValue);

                board[row, col] = EMPTY;

                if (score > bestScore)
                {
                    bestScore = score;
                    bestColumn = col;
                }
            }
        }

        return bestColumn;
    }

    private int Minimax(int[,] board, int depth, bool isMaximizing, int alpha, int beta)
    {
        if (depth == 0 || IsTerminalNode(board))
            return EvaluateBoard(board);

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            foreach (int col in GetColumnPriorityOrder())
            {
                if (IsValidMove(board, col))
                {
                    int row = GetNextOpenRow(board, col);
                    board[row, col] = AI_PLAYER;
                    int eval = Minimax(board, depth - 1, false, alpha, beta);
                    board[row, col] = EMPTY;
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                        break;
                }
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (int col in GetColumnPriorityOrder())
            {
                if (IsValidMove(board, col))
                {
                    int row = GetNextOpenRow(board, col);
                    board[row, col] = HUMAN_PLAYER;
                    int eval = Minimax(board, depth - 1, true, alpha, beta);
                    board[row, col] = EMPTY;
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break;
                }
            }
            return minEval;
        }
    }

    private int EvaluateBoard(int[,] board)
    {
        int score = 0;

        // Ưu tiên giữa bàn cờ
        for (int row = 0; row < ROW_COUNT; row++)
            if (board[row, COLUMN_COUNT / 2] == AI_PLAYER) score += 6;

        score += EvaluateLines(board, AI_PLAYER);
        score -= EvaluateLines(board, HUMAN_PLAYER);

        return score;
    }

    private int EvaluateLines(int[,] board, int player)
    {
         int score = 0;

        // Horizontal
        for (int row = 0; row < ROW_COUNT; row++)
        {
            for (int col = 0; col < COLUMN_COUNT - 3; col++)
            {
                var window = new int[] {
                    board[row, col], board[row, col + 1],
                    board[row, col + 2], board[row, col + 3]
                };
                score += EvaluateWindow(window, player);
            }
        }

        // Vertical
        for (int col = 0; col < COLUMN_COUNT; col++)
        {
            for (int row = 0; row < ROW_COUNT - 3; row++)
            {
                var window = new int[] {
                    board[row, col], board[row + 1, col],
                    board[row + 2, col], board[row + 3, col]
                };
                score += EvaluateWindow(window, player);
            }
        }

        // Positive diagonal
        for (int row = 0; row < ROW_COUNT - 3; row++)
        {
            for (int col = 0; col < COLUMN_COUNT - 3; col++)
            {
                var window = new int[] {
                    board[row, col], board[row + 1, col + 1],
                    board[row + 2, col + 2], board[row + 3, col + 3]
                };
                score += EvaluateWindow(window, player);
            }
        }

        // Negative diagonal
        for (int row = 3; row < ROW_COUNT; row++)
        {
            for (int col = 0; col < COLUMN_COUNT - 3; col++)
            {
                var window = new int[] {
                    board[row, col], board[row - 1, col + 1],
                    board[row - 2, col + 2], board[row - 3, col + 3]
                };
                score += EvaluateWindow(window, player);
            }
        }

        return score;
    }

    private int EvaluateWindow(int[] window, int player)
    {
        int score = 0;
        int oppPlayer = (player == AI_PLAYER) ? HUMAN_PLAYER : AI_PLAYER;

        int countPlayer = window.Count(p => p == player);
        int countOpp = window.Count(p => p == oppPlayer);
        int countEmpty = window.Count(p => p == EMPTY);

        if (countPlayer == 4)
            score += 100000;
        else if (countPlayer == 3 && countEmpty == 1)
            score += 100;
        else if (countPlayer == 2 && countEmpty == 2)
            score += 10;

        if (countOpp == 3 && countEmpty == 1)
            score -= 120;

        return score;
    }

    private List<int> GetColumnPriorityOrder()
    {
        // Ưu tiên duyệt từ giữa ra hai bên
        return new List<int> { 3, 2, 4, 1, 5, 0, 6 };
    }

   private bool IsWinningMove(int[,] board, int player)
    {
        // Horizontal
        for (int row = 0; row < ROW_COUNT; row++)
        {
            for (int col = 0; col < COLUMN_COUNT - 3; col++)
            {
                if (Enumerable.Range(0, 4).All(i => board[row, col + i] == player))
                    return true;
            }
        }

        // Vertical
        for (int col = 0; col < COLUMN_COUNT; col++)
        {
            for (int row = 0; row < ROW_COUNT - 3; row++)
            {
                if (Enumerable.Range(0, 4).All(i => board[row + i, col] == player))
                    return true;
            }
        }

        // Positive diagonal
        for (int row = 0; row < ROW_COUNT - 3; row++)
        {
            for (int col = 0; col < COLUMN_COUNT - 3; col++)
            {
                if (Enumerable.Range(0, 4).All(i => board[row + i, col + i] == player))
                    return true;
            }
        }

        // Negative diagonal
        for (int row = 3; row < ROW_COUNT; row++)
        {
            for (int col = 0; col < COLUMN_COUNT - 3; col++)
            {
                if (Enumerable.Range(0, 4).All(i => board[row - i, col + i] == player))
                    return true;
            }
        }

        return false;
    }

    private bool IsValidMove(int[,] board, int col)
    {
        return board[0, col] == EMPTY;
    }

    private int GetNextOpenRow(int[,] board, int col)
    {
        for (int row = ROW_COUNT - 1; row >= 0; row--)
        {
            if (board[row, col] == EMPTY)
                return row;
        }
        return -1; // should not happen if move is valid
    }

    private bool IsTerminalNode(int[,] board)
    {
        return IsWinningMove(board, AI_PLAYER) || IsWinningMove(board, HUMAN_PLAYER) || GetValidLocations(board).Count == 0;
    }

    private List<int> GetValidLocations(int[,] board)
    {
        var validLocations = new List<int>();
        for (int col = 0; col < COLUMN_COUNT; col++)
        {
            if (IsValidMove(board, col))
                validLocations.Add(col);
        }
        return validLocations;
    }
}
