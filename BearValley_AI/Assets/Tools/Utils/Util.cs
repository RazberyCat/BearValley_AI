using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util 
{
    public static T GetOrAddComponent<T>(GameObject gameObject) where T : UnityEngine.Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
            component = gameObject.AddComponent<T>();
        return component;
    }

    // 하위 자식개체 오브젝트 찾기
    public static GameObject FindChild(GameObject ParentObject, string ChildName = null, bool searchChildren = false)
    {
        Transform transform = FindChild<Transform>(ParentObject, ChildName, searchChildren);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    // 하위 자식개체 타입 찾기
    public static T FindChild<T>(GameObject ParentObject, string ChildName = null, bool searchChildren = false) where T : UnityEngine.Object
    {
        if(ParentObject == null)
            return null;

        if(searchChildren == false) // 직속 자식을 찾는 경우
        {
            for (int i = 0; i < ParentObject.transform.childCount; i++)
            {
                Transform transform = ParentObject.transform.GetChild(i);
                if (string.IsNullOrEmpty(ChildName) || transform.name == ChildName)
                {
                    T component = transform.GetComponent<T>(); 
                    if (component != null)
                        return component;
                }
            }
        }
        else // 모든 하위 자식 중에 찾는 경우
        {
            
            foreach (T component in ParentObject.GetComponentsInChildren<T>()) 
            {
                if(string.IsNullOrEmpty(ChildName) || component.name == ChildName)
                    return component;
            }
        }

        return null;
    }





}
