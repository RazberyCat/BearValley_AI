using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.Rendering.DebugUI;
using TextAsset = UnityEngine.TextAsset;



public interface ILoader<key, value>
{
    Dictionary<key, value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Stats> TestDict { get; private set; } = new Dictionary<int, Stats>();
    // 추가


    public void Init()
    {
        //#Todo  TestData.Json 어드레서블에서 연결 필요
        //TestDict = LoadJson<StatData, int, Stats>("TestData").MakeDict();
        // 추가
    }


    Loader LoadJson<Loader, Key, Value>(string name) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>(name);
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

}
