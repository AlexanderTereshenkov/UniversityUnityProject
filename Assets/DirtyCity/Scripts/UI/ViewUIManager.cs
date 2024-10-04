using System.Collections.Generic;
using UnityEngine;

public class ViewUIManager : MonoBehaviour
{
    private Dictionary<IView, View> uiPages = new Dictionary<IView, View>();

    public static ViewUIManager ViewManagerInstance { get; private set; }

    private void Awake()
    {
        ViewManagerInstance = this;
    }

    public void RegisterView<T>(T page) where T : IView
    {
        if (!uiPages.ContainsKey(page))
        {
           // uiPages.Add(page.GetType(), page);
        }
    }
}
