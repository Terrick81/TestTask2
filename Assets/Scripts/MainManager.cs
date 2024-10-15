using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class MainManager : MonoBehaviour
{

    private static MainManager _mainManager;

    [SerializeField]
    private MainCamera _mainCamera;
    [SerializeField]
    private GameObject _objects;
    [SerializeField]
    private Material _fadeMaterial;
    [SerializeField]
    private Material _opaqueMaterial;
    [SerializeField]
    private Sprite[] _selectSprites;
    [SerializeField]
    private Sprite[] _hideSprites;

    public Detail[] _scriptOfObjects;

    private void Awake()
    {
        _mainManager = GetComponent<MainManager>();
        Detail.MyAwake(_fadeMaterial, _opaqueMaterial);
        Colors.Awake();
        Load();
        Block.MyAwake(_selectSprites, _hideSprites, _scriptOfObjects.Length);
    }

    public  void Load()
    {
        int countObjectsOnScene = _objects.transform.childCount;
        _scriptOfObjects = new Detail[countObjectsOnScene];

        List<Data> loadArray = JSONManager.Load();

        for (int i = 0; i < countObjectsOnScene; i++)
        {
            GameObject obj = _objects.transform.GetChild(i).gameObject;
            
            if (!obj.GetComponent<Detail>()) obj.AddComponent<Detail>();
            _scriptOfObjects[i] = obj.GetComponent<Detail>();

            string name = _scriptOfObjects[i].GetName();
            foreach (Data data in loadArray)
            {
                if (data.name == name)
                {
                    _scriptOfObjects[i].SetData(data);
                    loadArray.Remove(data);
                    break;
                }
            }
        }
    }
    public static Data[] Save()
    {
        List<Data> data = new List<Data> { };
        foreach (Detail d in _mainManager._scriptOfObjects)
        {
            data.Add(d.GetData());
        }
        return data.ToArray();
    }
    public static Detail[] GetScriptOfObjects()
    {
        return _mainManager._scriptOfObjects;
    }
}
