using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpacityBtb : MonoBehaviour
{
    private static OpacityBtb _selectable;

    [SerializeField]
    private Image _outline;
    [SerializeField]
    private int levelOpacity;
    public void Selectable(bool sendMassage = true) 
    {
        if (_selectable != this)
        {
            if (_selectable != null)
            {
                _selectable.SwithOutline(false);
            }
            _selectable = this;
            SwithOutline(true);
            if (sendMassage)
            {
                DetailPanel.SetOpacity(levelOpacity);
            }
        }
    }
    public static void RemoteSelectable()
    {
        if (_selectable != null)
        {
            _selectable.SwithOutline(false);
            _selectable = null;
        }
    }
    public void SwithOutline(bool turn)
    {
        if (turn) 
        {
            _outline.color = Colors.GetColorInColection(ColorType.orange, 1);
        }
        else
        {
            _outline.color = Colors.GetColorInColection(ColorType.orange, 0);
        }
    }
}
