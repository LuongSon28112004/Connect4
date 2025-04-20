using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSlider : MonoBehaviour
{
    [SerializeField]
    protected Slider slider;

    protected virtual void Awake()
    {
        LoadSlider();
        AddOnValueChangeEvent();
    }

    private void LoadSlider()
    {
        slider = GetComponent<Slider>();
    }

    protected void AddOnValueChangeEvent()
    {
        slider.onValueChanged.AddListener((value) => OnValueChanged(value));
    }

    public abstract void OnValueChanged(float value);
}
