using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<MenuWindow> _menuWindows = new List<MenuWindow>();
    
    public void OpenWindow(MenuWindow window)
    {
        window.ShowWindow();
    }
    public void CloseWindow(MenuWindow window)
    {
        window.HideWindow();
    }
    
    public MenuWindow GetWindow(string windowName)
    {
        return _menuWindows.Find(x => x.WindowName == windowName);
    }
}
