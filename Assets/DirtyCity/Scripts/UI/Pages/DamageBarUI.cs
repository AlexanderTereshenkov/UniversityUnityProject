using UnityEngine;
using UnityEngine.UI;

public class DamageBarUI : View
{

    [SerializeField] private Image image;

    private void Awake()
    {
        _viewUIManager.RegisterView(this);
    }

    public override void Hide()
    {
        image.gameObject.SetActive(false);
    }

    public override void Show()
    {
        image.gameObject.SetActive(true);
    }

    public void FillBar(float maxValue, float value)
    {
        float percent = value / maxValue;
        image.fillAmount = percent;
    }

}
