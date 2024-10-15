using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class DetailPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _colorPickerPanel;

    [SerializeField]
    private GameObject _colorSelectorPrefab;

    [SerializeField]
    private GameObject _blockPrefab;
    
    [SerializeField]
    private GameObject _content;

    [SerializeField]
    private Checkbox _allSelected;
    
    [SerializeField]
    private Checkbox _HideSelected;
    
    private ColorBtn[] _colorBtns;
    private OpacityBtb[] _opacityBtb;
    private Block[] _blocks;
    private Detail _target;

    private static DetailPanel _panelScript;

    private void Start()
    {
        _panelScript = GetComponent<DetailPanel>();
        Detail.AddListener(SetTarget);
        GameObject colorSelectorsGroup = GameObject.Find("Color Selectors");
        GameObject opacityBtnsGroup = GameObject.Find("HideObjSlider");

        _colorBtns = new ColorBtn[Colors.colorsCount];
        _opacityBtb = new OpacityBtb[Detail.MAX_LEVEL_OPACITY];

        for (int i = 0; i < Colors.colorsCount; i++)
        {
            GameObject c;
            c = Instantiate(_colorSelectorPrefab, colorSelectorsGroup.transform);
            _colorBtns[i] = c.GetComponent<ColorBtn>();
            _colorBtns[i].UpdateColor((ColorType)i);
        }

        Detail[] objects = MainManager.GetScriptOfObjects();
        _blocks = new Block[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject c;
            c = Instantiate(_blockPrefab, _content.transform);
            _blocks[i] = c.GetComponent<Block>();
            _blocks[i].UpdateBlock(objects[i]);
        }

        for (int i = 0; i < Detail.MAX_LEVEL_OPACITY; i++)
        {
            _opacityBtb[i] = opacityBtnsGroup.transform.GetChild(i).GetComponent<OpacityBtb>();
        }
        SetTarget(null);
    }
    private void SetTarget(GameObject target)
    {
        if (_target != null)
            _blocks[_target.GetSiblingIndex()].SwithOutline(false);
        if (target == null)
        {
            _target = null;
            OpacityBtb.RemoteSelectable();
        }
        else
        {
            _target = target.GetComponent<Detail>();
            _blocks[_target.GetSiblingIndex()].SwithOutline(true);
            int index = Detail.MAX_LEVEL_OPACITY - _target.GetLevelOpacity();
            _opacityBtb[index].Selectable(sendMassage: false);
        }
        
        SwithColorPickerPanel();
    }
    public static void SetColor(ColorType color)
    {
        _panelScript._target.SetColor(color);
    }
    public static void SetOpacity(int opacity)
    {
        if (_panelScript._target != null)
            _panelScript._target.SetOpacityLevel(opacity);
    }
    public void SwithAllCheckbox() 
    {
        bool currenBool = _allSelected.GetBool();

        foreach (Block b in _blocks)
        {
            b.SetSelect(currenBool);
        }
    }
    private void SwithColorPickerPanel()
    {
        if (_target != null)
        {
            _colorPickerPanel.SetActive(true);
            UpdateColorPickerPanel();
        }
        else
        {
            _colorPickerPanel.SetActive(false);
        }
    }
    public static void AllSelectedDone(bool b)
    {
        _panelScript._allSelected.SetBool(b);
    }
    public void AllSetHide()
    {
        bool currenBool = _HideSelected.GetBool();

        foreach (Block b in _blocks)
        {
            b.SetHide(currenBool);
        }
    }
    private void UpdateColorPickerPanel()
    {
        _colorBtns[(int)_target.GetColor()].Selectable(sendMassage: false);
    }

}
