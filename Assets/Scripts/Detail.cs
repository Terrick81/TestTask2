using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Detail : MonoBehaviour
{
    public const int MAX_LEVEL_OPACITY = 5;
    private const float DOUBLE_CLICK_TIME = 0.2f;

    private bool _hide = false;
    private int _levelOpacity = 5;
    private ColorType _color = ColorType.white;
    
    private Material _material;

    private static float _lastClickTime;
    private static Material _fadeMaterial;
    private static Material _opaqueMaterial;

    private static GameObject _target;
    private static UnityEvent<GameObject> _setTargetEvent = new UnityEvent<GameObject>();
    


    void Awake() 
    {
        _material = GetComponent<Renderer>().material;
    }

    public void SetData(Data data)
    {
        SetHide(data.hide);
        SetOpacityLevel(data.levelOpacity);
        SetColor(data.color);
    }
    public Data GetData()
    {
        Data data = new Data();
        data.name = GetName();
        data.hide = _hide;
        data.levelOpacity = _levelOpacity; 
        data.color = _color;
        return data;
    }
    public static void MyAwake(Material fadeMaterial, Material opaqueMaterial)
    {
        _fadeMaterial = fadeMaterial;
        _opaqueMaterial = opaqueMaterial;
    }
    private void OnMouseDown()
    {
        if (!IsMouseOverUI())
        {
            float timeSinceLastClick = Time.time - _lastClickTime;

            if (timeSinceLastClick <= DOUBLE_CLICK_TIME)
            {
                if (_target != gameObject)
                {
                    _target = gameObject;
                    _setTargetEvent.Invoke(_target);
                }
            }
            _lastClickTime = Time.time;
        }
    }
    public static void RemoveTarget()
    {
        _target = null;
        _setTargetEvent.Invoke(_target);
    }
    public static void AddListener(UnityAction<GameObject> action) 
    {
        _setTargetEvent.AddListener(action);
    }
    public static void RemoveListener(UnityAction<GameObject> action)
    {
        _setTargetEvent.RemoveListener(action);
    }
    private float OpacityLevelToAlpha()
    {
        return 1f / MAX_LEVEL_OPACITY * _levelOpacity;
    }
    public void SetOpacityLevel(int level)
    {
        if (level > MAX_LEVEL_OPACITY)
            level = MAX_LEVEL_OPACITY;
        if ( level < 1)
            level = 1;

        _levelOpacity = level;

        if (level == MAX_LEVEL_OPACITY)
        {
            _material.CopyPropertiesFromMaterial(_opaqueMaterial);
        }
        else
        {
            _material.CopyPropertiesFromMaterial(_fadeMaterial);
        }
        _material.color = Colors.GetColorInColection(_color, OpacityLevelToAlpha());
    }
    public void SetColor(ColorType color)
    {
        _color = color;
        _material.color = Colors.GetColorInColection(_color, OpacityLevelToAlpha());
    }
    public ColorType GetColor()
    {
        return _color;
    }
    public int GetLevelOpacity()
    {
        return _levelOpacity;
    }
    public string GetName()
    {
        return gameObject.name;
    }
    public int GetHide()
    {
        if (_hide) return 1;
        else return 0;
    }
    public int SwithHide()
    {
        SetHide(!_hide);
        return GetHide();
    }
    public void SetHide(bool hide)
    {
        _hide = hide;
        gameObject.SetActive(!_hide);
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    public int GetSiblingIndex()
    {
        return transform.GetSiblingIndex();
    }
}
