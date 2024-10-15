using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBtn : MonoBehaviour
{
    private static ColorBtn _selectable;
    [SerializeField]
    private Outline _outline;
    private ColorType _color;

    public void UpdateColor(ColorType color)
    {
        GetComponent<Image>().color = Colors.GetColorInColection(color);
        _color = color;
    }
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
                DetailPanel.SetColor(_color);
            }
        }
    }
    public void SwithOutline(bool turn)
    {
        _outline.enabled = turn;
    }
}
