using UnityEngine;
using UnityEngine.UI;

public abstract class BaseToggleMusic : MonoBehaviour
{
    [SerializeField]
    protected Toggle toggle;

    protected virtual void Awake()
    {
        LoadToggle();
        AddOnValueChangedEvent();
    }

    private void LoadToggle()
    {
        toggle = GetComponent<Toggle>();
    }

    protected void AddOnValueChangedEvent()
    {
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener((value) => OnValueChanged(value));
    }

    public abstract void OnValueChanged(bool value);
}
