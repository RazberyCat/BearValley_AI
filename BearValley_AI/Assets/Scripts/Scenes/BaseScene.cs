using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{

    Define.Scene sceneType = Define.Scene.Unknown;

    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    void Start()
    {

        Init();
    }

    protected virtual void Init()
    {
        Debug.Log("BaseScene");
        EventSystem obj = Object.FindFirstObjectByType<EventSystem>();
        if (obj == null)
            Managers.Resource.Instantiate("EventSystem").name = "@EventSystem";
    }


    public abstract void Clear();

}
