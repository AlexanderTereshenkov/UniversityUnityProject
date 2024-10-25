using Reflex.Attributes;
using UnityEngine;

public abstract class View : MonoBehaviour, IView
{
    [SerializeField] private ViewType viewType;
    protected ViewUIManager _viewUIManager;

    [Inject]
    private void Construct(ViewUIManager viewUIManager)
    {
        _viewUIManager = viewUIManager;
    }

    public abstract void Hide();
    public abstract void Show();
    public ViewType GetViewType() => viewType;
}
