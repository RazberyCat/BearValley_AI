using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.Linq;
using Object = UnityEngine.Object;

public class ResouceManager 
{
    #region 모든 리소스
    Dictionary<string, Object> _resources = new Dictionary<string, Object>();
    List<Object> _resourcesList = new List<Object>(); // 릴리즈를 위한 리스트
    #endregion

    #region 리소스 로드 & 릴리즈
    public T Load<T>(string key) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))
            return resource as T;

        return null;
    }
    public GameObject Instantiate(string key, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>(key);
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab : {key}");
            return null;
        }

        if (prefab.GetComponent<Poolabale>() != null)
            return Managers.Pool.Pop(prefab, parent).gameObject;

        GameObject gameObject = Object.Instantiate(prefab, parent);
        int index = gameObject.name.IndexOf("(Clone)");
        if(index > 0)
            gameObject.name = gameObject.name.Substring(0, index);

        return gameObject;
    }

    public void Destroy(GameObject gameObject)
    {
        if (gameObject == null)
            return;

        Poolabale poolabale = gameObject.GetComponent<Poolabale>();
        if(poolabale != null)
        {
            Managers.Pool.Push(poolabale);
            return;
        }

        Object.Destroy(gameObject);
    }

    public void ReleaseAll() // #Todo 릴리즈시 로드안됨
    {
        for (int i = 0; i < _resources.Count; i++)
        {
            Addressables.Release(_resourcesList[i]);
        }

        Debug.Log("어드레서브 릴리즈 완료");
    }

    #endregion

    #region 어드레서블

    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        var asyncOperation = Addressables.LoadAssetAsync<T>(key);
        asyncOperation.Completed += (op) =>
        {
            // 캐시 확인.
            if (_resources.TryGetValue(key, out Object resource))
            {
                callback?.Invoke(op.Result);
                return;
            }
            //if (typeof(T) == typeof(TextAsset))
            //{
            //    // T가 TextAsset인 경우
            //    TextAsset textAsset = op.Result as TextAsset;

            //    if (textAsset != null)
            //    {
            //        // TextAsset을 원하는 타입으로 변환하고 저장
            //        T data = JsonUtility.FromJson<T>(textAsset.text);
            //        _resources.Add(key, data);
            //        _resourcesList.Add(data);
            //        callback?.Invoke(data);
            //    }
            //    else
            //    {
            //        // 로드 실패 시 처리
            //    }
            //}
            //else
            //{
            //    // T가 TextAsset이 아닌 경우 (일반 리소스 로드)
            //    _resources.Add(key, op.Result);
            //    _resourcesList.Add(op.Result); // 릴리즈를 위해 리스트에 저장
            //    callback?.Invoke(op.Result);
            //}
            _resources.Add(key, op.Result);
            _resourcesList.Add(op.Result); // 릴리즈를 위해 리스트에 저장
            callback?.Invoke(op.Result);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        opHandle.Completed += (op) =>
        {
            int count = 0;

            int maxCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (obj) =>
                {
                    count++;
                    callback?.Invoke(result.PrimaryKey, count, maxCount);
                });
            }
        };
    }

    #endregion
}
