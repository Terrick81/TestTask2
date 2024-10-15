using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class JSONManager : MonoBehaviour
{
    const int AUTOSAVE_DELAY = 30;

    private void Start() => StartCoroutine(Autosave());
    public static List<Data> Load()
    {
        Debug.Log("load");
        Data[] result =
            FromJson<Data>(
               File.ReadAllText(Application.streamingAssetsPath + "/objectsSetting.json")
               );

        if (result == null) return new List<Data>();
        return result.ToList();
    }
    public static void Save()
    {
        Debug.Log("save");
        File.WriteAllText(Application.streamingAssetsPath + "/objectsSetting.json",
            ToJson(MainManager.Save()));
    }
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper;
        if (json != "")
        {
            wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }
        return new Wrapper<T>().Items;

    }
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, true);
    }
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
    IEnumerator Autosave()
    {
        while (true)
        {
            yield return new WaitForSeconds(AUTOSAVE_DELAY);
            Save();
        }

    }
}

[Serializable]
public class Data
{
    public string name;
    public bool hide = false;
    public int levelOpacity = 5;
    public ColorType color;
}


