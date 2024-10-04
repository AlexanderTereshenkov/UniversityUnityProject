using System.Collections.Generic;
using UnityEngine;

public class ViewUIManager : MonoBehaviour
{
    private Dictionary<string, IView> _uiPages = new Dictionary<string, IView>();

    public void RegisterView<T>(T page) where T : IView
    {
        var key = page.GetType().Name;
        if (!_uiPages.ContainsKey(key))
        {
           _uiPages.Add(key, page);
        }
    }

    public T GetView<T>() where T : IView
    {
        var key = typeof(T).Name;
        if (!_uiPages.ContainsKey(key))
        {
            Debug.LogError("View not registered");
            return default;
        }
        return (T) _uiPages[key];
    }


}
