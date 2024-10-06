using TMPro;
using UnityEngine;

public class InteractionTextUI : View
{

    [SerializeField] private TextMeshProUGUI messageText;

    private void Awake()
    {
        _viewUIManager.RegisterView(this);
    }

    public override void Hide()
    {
        messageText.gameObject.SetActive(false);
    }

    public override void Show()
    {
        messageText.gameObject.SetActive(true);
    }

    public void SetText(string text)
    {
        messageText.text = text;
    }

}
