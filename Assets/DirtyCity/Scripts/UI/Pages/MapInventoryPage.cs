using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapInventoryPage : View
{

    [SerializeField] GameObject mapInventoryPage;
    [SerializeField] private TextMeshProUGUI filtersText;
    [SerializeField] private Image objectIcon;
    [SerializeField] private TextMeshProUGUI objectText;

    [Header("Default options")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private string defaultText;

    public GameObject InventoryPage
    {
        get
        {
            return mapInventoryPage;
        }
    }

    private void Awake()
    {
        _viewUIManager.RegisterView(this);
    }

    public override void Hide()
    {
        mapInventoryPage.SetActive(false);
        _viewUIManager.ShowView(ViewType.Gameplay);
    }

    public override void Show()
    {
        _viewUIManager.HideAllViews();
        mapInventoryPage.SetActive(true);
    }

    public void SetFiltersAmount(int amount)
    {
        filtersText.text = amount.ToString();
    }

    public void SetInteractibleObject(IInteractible interactible)
    {
        if(interactible == null)
        {
            objectText.text = defaultText;
            objectIcon.sprite =  defaultSprite;
            return;
        }
        objectText.text = interactible.GetName() == null ? defaultText : interactible.GetName();
        objectIcon.sprite = interactible.GetIcon() == null ? defaultSprite : interactible.GetIcon();
    }

}
