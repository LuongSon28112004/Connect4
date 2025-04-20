using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    [SerializeField] private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        this.addOnClickListener();
    }

    private void addOnClickListener()
    {
        button.onClick.AddListener(OnClick);
    }

    protected abstract void OnClick();
}
