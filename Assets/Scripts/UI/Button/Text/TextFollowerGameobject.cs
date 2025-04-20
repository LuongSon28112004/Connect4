using UnityEngine;
using TMPro;

public class TextFollowerGameobject : MonoBehaviour
{
    public Transform gameobjects;

    void Update()
    {
        if (gameobjects != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(gameobjects.position);
            transform.position = screenPos;
        }
    }
}