using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PoolManager : MonoBehaviour
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolabale> _poolStack = new Stack<Poolabale>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
                Push(Create());
        }

        Poolabale Create()
        {
            GameObject gameObject = Object.Instantiate<GameObject>(Original);
            gameObject.name = Original.name;
            return gameObject.GetOrAddComponent<Poolabale>();
        }

        public void Push(Poolabale poolabale)
        {
            if (poolabale == null)
                return;

            poolabale.transform.parent = Root;
            poolabale.gameObject.SetActive(false);
            poolabale.IsUsing = false;

            _poolStack.Push(poolabale);
        }

        public Poolabale Pop(Transform parent)
        {
            Poolabale poolabale;

            if (_poolStack.Count > 0)
                poolabale = _poolStack.Pop();
            else
                poolabale = Create();

            poolabale.gameObject.SetActive(true);
            poolabale.transform.parent = parent;
            poolabale.IsUsing = true;

            return poolabale;
        }
    }

    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;

    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    public void Push(Poolabale poolabale)
    {
        string name = poolabale.gameObject.name;
        if(_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolabale.gameObject);
            return;
        }
        _pool[name].Push(poolabale);
    }

    public Poolabale Pop(GameObject original, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);
        
        return _pool[original.name].Pop(parent) ;
    }


    public GameObject GetOrignal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;

        return _pool[name].Original;
    }

    public void Clear() // 필요할때 사용
    {
        foreach (Transform child in _root) // 산하의 모든 자식접근
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
