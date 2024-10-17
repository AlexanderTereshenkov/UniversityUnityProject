using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapInventoryPage : View
{

    [SerializeField] GameObject mapInventoryPage;
    [SerializeField] private TextMeshProUGUI filtersText;
    [SerializeField] private Image objectIcon;
    [SerializeField] private TextMeshProUGUI objectText;
    [SerializeField] private Button deleteButton;

    [Header("Default options")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private string defaultText;

    private Player _player;

    public GameObject InventoryPage
    {
        get
        {
            return mapInventoryPage;
        }
    }

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _viewUIManager.RegisterView(this);
        deleteButton.onClick.AddListener(() =>
        {
            _player.GetInventory().DeleteFromInventory();
        }
        );
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

    public void SetInteractibleObject(IPickable pickable)
    {
        if(pickable == null)
        {
            objectText.text = defaultText;
            objectIcon.sprite =  defaultSprite;
            return;
        }
        objectText.text = pickable.GetName() == null ? defaultText : pickable.GetName();
        objectIcon.sprite = pickable.GetIcon() == null ? defaultSprite : pickable.GetIcon();
    }

}
