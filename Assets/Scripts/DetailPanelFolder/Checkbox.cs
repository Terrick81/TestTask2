using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Checkbox : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _sprites;
    private Image _image;
    private bool _bool = false;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    public void Swith()
    {
        _bool = !_bool;
        UpdateSprite();
    }
    public void SetBool(bool bl)
    {
        if (_bool != bl)
        {
            _bool = bl;
            UpdateSprite();
        }  
    }
    public bool GetBool()
    {
        return _bool;
    }
    private void UpdateSprite()
    {
        _image.sprite = _sprites[Convert.ToInt32(_bool)];
    }
}
