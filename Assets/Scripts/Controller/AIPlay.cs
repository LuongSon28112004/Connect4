using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class AIPlay : MonoBehaviour
{
    [SerializeField]
    private GameController gCtrl;

    [SerializeField]
    private Transform[] Arrows;

    [SerializeField]
    private GameObject Person;

    [SerializeField]
    private GameObject AI;

    int count = 1;

    void Start()
    {
        gCtrl = GameObject.FindFirstObjectByType<GameController>();
    }

    void Awake()
    {
        GameObject gArrows = GameObject.FindGameObjectWithTag("arrow");
        Arrows = new Transform[7];
        int i = 0;
        foreach (Transform arrow in gArrows.transform)
        {
            Arrows[i] = arrow;
            ++i;
        }
    }

    private void disableClickButton()
    {
        foreach (Transform arrow in Arrows)
        {
            Button btn = arrow.GetComponent<Button>();
            if (btn != null)
            {
                btn.interactable = false;
            }
        }
    }

    private void enableClickButton()
    {
        foreach (Transform arrow in Arrows)
        {
            Button btn = arrow.GetComponent<Button>();
            if (btn != null)
            {
                btn.interactable = true;
            }
        }
    }

    void Update()
    {
        this.AISetTheBall();
        this.swapPlayer();
    }

    private void swapPlayer()
    {
        if (gCtrl.Turn == 1)
        {
            if (AI != null)
            {
                Transform whiteEdge = AI.transform.Find("EdgesWhite");
                Transform greenEdge = AI.transform.Find("EdgesGreen");

                if (whiteEdge != null)
                    whiteEdge.gameObject.SetActive(false);
                if (greenEdge != null)
                    greenEdge.gameObject.SetActive(true);
            }

            if (Person != null)
            {
                Transform whiteEdge = Person.transform.Find("EdgesWhite");
                Transform greenEdge = Person.transform.Find("EdgesGreen");

                if (whiteEdge != null)
                    whiteEdge.gameObject.SetActive(true);
                if (greenEdge != null)
                    greenEdge.gameObject.SetActive(false);
            }
        }
        else
        {
            if (AI != null)
            {
                Transform whiteEdge = AI.transform.Find("EdgesWhite");
                Transform greenEdge = AI.transform.Find("EdgesGreen");

                if (whiteEdge != null)
                    whiteEdge.gameObject.SetActive(true);
                if (greenEdge != null)
                    greenEdge.gameObject.SetActive(false);
            }

            if (Person != null)
            {
                Transform whiteEdge = Person.transform.Find("EdgesWhite");
                Transform greenEdge = Person.transform.Find("EdgesGreen");

                if (whiteEdge != null)
                    whiteEdge.gameObject.SetActive(false);
                if (greenEdge != null)
                    greenEdge.gameObject.SetActive(true);
            }
        }
    }

    private void AISetTheBall()
    {
        if (gCtrl.Turn == 0)
            return;
        if (count == 1)
        {
            this.disableClickButton();
            count = 0;
            Invoke(nameof(AIClickButton), 1f);
        }
    }

    private void AIClickButton()
    {
        int randomValue;
        if (LevelAIControllder.Instance.LevelAI == 0)
        {
            randomValue = MiniMaxAIEasy.Instance.GetBestMove();
        }
        else if (LevelAIControllder.Instance.LevelAI == 1)
        {
            Debug.Log("LevelAIControllder.Instance.LevelAI == 1");
            randomValue = MiniMaxAIMedium.Instance.GetBestMove();
        }
        else 
        {
            Debug.Log("LevelAIControllder.Instance.LevelAI == 2");
            randomValue = MiniMaxAIHard.Instance.GetBestMove();
        }
        Button btn = Arrows[randomValue].GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.Invoke();
        }
        count = 1;
        this.enableClickButton();
    }
}
