using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelAIControllder : MonoBehaviour
{
    public static LevelAIControllder Instance { get; private set; }
    [SerializeField] int levelAI = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public int LevelAI
    {
        get { return levelAI; }
        set { levelAI = value; }
    }
}
