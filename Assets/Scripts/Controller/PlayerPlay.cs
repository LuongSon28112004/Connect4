using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
     [SerializeField]
    private GameController gCtrl;

    [SerializeField]
    private GameObject Person_1;
    [SerializeField]
    private GameObject Person_2;

    void Start()
    {
        gCtrl = GameObject.FindFirstObjectByType<GameController>();
    }

    void Update()
    {
        this.swapPlayer();
    }


    private void swapPlayer()
    {
        if (gCtrl.Turn == 0)
        {
            if(Person_1 != null)
            {
                Transform whiteEdge = Person_1.transform.Find("EdgesWhite");
                Transform greenEdge = Person_1.transform.Find("EdgesGreen");

                if (whiteEdge != null)
                    whiteEdge.gameObject.SetActive(false);
                if (greenEdge != null)
                    greenEdge.gameObject.SetActive(true);
            }

            if (Person_2 != null)
            {
                Transform whiteEdge = Person_2.transform.Find("EdgesWhite");
                Transform greenEdge = Person_2.transform.Find("EdgesGreen");

                if (whiteEdge != null)
                    whiteEdge.gameObject.SetActive(true);
                if (greenEdge != null)
                    greenEdge.gameObject.SetActive(false);
            }
        }
        else
        {
            if(Person_1 != null)
            {
                Transform whiteEdge = Person_1.transform.Find("EdgesWhite");
                Transform greenEdge = Person_1.transform.Find("EdgesGreen");

                if (whiteEdge != null)
                    whiteEdge.gameObject.SetActive(true);
                if (greenEdge != null)
                    greenEdge.gameObject.SetActive(false);
            }

            if (Person_2 != null)
            {
                Transform whiteEdge = Person_2.transform.Find("EdgesWhite");
                Transform greenEdge = Person_2.transform.Find("EdgesGreen");

                if (whiteEdge != null)
                    whiteEdge.gameObject.SetActive(false);
                if (greenEdge != null)
                    greenEdge.gameObject.SetActive(true);
            }
        }
    }
}
