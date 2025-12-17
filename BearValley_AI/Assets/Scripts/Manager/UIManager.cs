using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Define;

public class UIManager
{
    int order = 10;

    Stack<UI_Popup> popupStack= new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public UI_Scene SceneUI { get { return _sceneUI; } }

    public void Init()
    {
        //Managers.UI.ShowSceneUI<UI_TitleScene>();
        //Managers.UI.ShowSceneUI<UI_GameScene>();
        CloseAllPopupUI();
    }

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject gameObject, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(gameObject);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true; // 소팅 오더 강제

        if (sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }

    }
    public T MakeSubitem<T>(Transform parent = null, string subitemName = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(subitemName)) //이름이 비어있으면 타입과 같은 이름으로
            subitemName = typeof(T).Name;

        GameObject gameObject = Managers.Resource.Instantiate(subitemName);

        if (parent != null)
            gameObject.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(gameObject);
    }

    public T ShowSceneUI<T>(string sceneName = null) where T : UI_Scene
    {
        if(string.IsNullOrEmpty(sceneName)) //이름이 비어있으면 타입과 같은 이름으로
            sceneName = typeof(T).Name;

        GameObject gameObject = Managers.Resource.Instantiate(sceneName);
        gameObject.transform.SetParent(Root.transform);
        T sceneUI = Util.GetOrAddComponent<T>(gameObject);
        _sceneUI = sceneUI;
        return sceneUI;
    }
    public T ShowPopupUI<T>(string popupName = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(popupName)) //이름이 비어있으면 타입과 같은 이름으로
            popupName = typeof(T).Name;

        GameObject gameObject = Managers.Resource.Instantiate(popupName);
        gameObject.transform.SetParent(Root.transform);
        T popup = Util.GetOrAddComponent<T>(gameObject);
        popupStack.Push(popup);
        return popup;

        // 개선사항
        // 이미 있는 팝업체크
        // 이미 있다면 소팅 오더만 변경하고 맨위로 출력

    }

    public void CloseSceneUI()
    {
        if (_sceneUI == null)
            return;

        UI_Popup popup = popupStack.Pop();
        popup.gameObject.SetActive(false);
        //Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        order--;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (popupStack.Count == 0)
            return;

        if(popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        UI_Popup popup = popupStack.Pop();
        _sceneUI.gameObject.SetActive(false);
        //Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        order--;
    }



    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();
    }


    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}
