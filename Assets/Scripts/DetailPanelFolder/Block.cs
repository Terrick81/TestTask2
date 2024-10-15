using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField]
    private Image _selectCheckbox;

    [SerializeField]
    private Image _hideCheckbox;

    [SerializeField]
    private Outline _outline;

    [SerializeField]
    private TextMeshProUGUI _text;

    private bool _select = false;
    private Detail _obj;

    private static int MAX_SELECTED;
    private static int _countSelect = 0;
    private static Sprite[] _selectSprites;
    private static Sprite[] _hideSprites;

    public static void MyAwake(Sprite[] selectSprites, Sprite[] hideSprites, int max_selected)
    {
        _selectSprites = selectSprites;
        _hideSprites = hideSprites;
        MAX_SELECTED = max_selected;
    }
    public void SetSelect(bool b)
    {
        _select = b;
        if (_select) _countSelect++;
        else _countSelect--;
        CheckCountSelect();
        _selectCheckbox.sprite = _selectSprites[Convert.ToInt32(b)];
    }
    public void SetHide(bool b)
    {
        if (_select)
        {
            _obj.SetHide(b);
            _hideCheckbox.sprite = _hideSprites[Convert.ToInt32(b)];
        }
    }
    public void SwithSelect()
    {
        SetSelect(!_select);
    }
    public void SwithHide()
    {
        int index = _obj.SwithHide();
        _hideCheckbox.sprite = _hideSprites[index];
    }
    public void SwithOutline(bool turn)
    {
        if (turn)
            _outline.effectColor =
                Colors.GetColorInColection(ColorType.orange);
        else
            _outline.effectColor =
                Colors.GetColorInColection(ColorType.gray);
    }
    public void UpdateBlock(Detail d)
    {
        _obj = d;
        _text.text = _obj.GetName();
        _hideCheckbox.sprite = _hideSprites[_obj.GetHide()];
    }
    private void CheckCountSelect()
    {
        if (_countSelect == MAX_SELECTED)
        {
            DetailPanel.AllSelectedDone(true);
        }
        else
        {
            DetailPanel.AllSelectedDone(false);
        }
    }
}
