using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    #region Bind
    protected void BindObject(Type type) { Bind<GameObject>(type); }
    protected void BindImage(Type type) { Bind<Image>(type); }
    protected void BindText(Type type) { Bind<TMP_Text>(type); }
    protected void BindButton(Type type) { Bind<Button>(type); }
    protected void BindToggle(Type type) { Bind<Toggle>(type); }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type); // Enum을 스트링으로 변환
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; // Enum 개수만큼 공간 할당
        _objects.Add(typeof(T), objects); // 저장

        for (int i = 0; i < names.Length; i++) // 자식 중 오브젝트 찾기
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");

        }
    }
    #endregion

    #region Get
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Toggle GetToggle(int idx) { return Get<Toggle>(idx); }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[index] as T; // T 타입으로 캐스팅 후 리턴 
    }
    #endregion

    #region Event
    public static void BindEvent(GameObject gameObject, Action clickAction = null, Action<PointerEventData> dragAction = null, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(gameObject);

        switch (type)
        {
            case Define.UIEvent.Click:
                SetHandler(ref evt.OnClickHandler, clickAction);
                break;
            case Define.UIEvent.PointerDown:
                SetHandler(ref evt.OnPointerDownHandler, clickAction);
                break;
            case Define.UIEvent.PointerUp:
                SetHandler(ref evt.OnPointerUpHandler, clickAction);
                break;
            case Define.UIEvent.BeginDrag:
                SetHandler(ref evt.OnBeginDragHandler, dragAction);
                break;
            case Define.UIEvent.Drag:
                SetHandler(ref evt.OnDragHandler, dragAction);
                break;
            case Define.UIEvent.EndDrag:
                SetHandler(ref evt.OnEndDragHandler, dragAction);
                break;
            case Define.UIEvent.Preseed:
                SetHandler(ref evt.OnPressedHandler, clickAction);
                break;
        }

        #region 백업
        //switch (type)
        //{
        //    case Define.UIEvent.Click:
        //        if (evt.OnClickHandler != null)
        //        {
        //            evt.OnClickHandler -= clickAction;
        //            evt.OnClickHandler += clickAction;
        //        }
        //        break;
        //    case Define.UIEvent.PointerDown:
        //        if (evt.OnPointerDownHandler != null)
        //        {
        //            evt.OnPointerDownHandler -= clickAction;
        //            evt.OnPointerDownHandler += clickAction;
        //        }
        //        break;
        //    case Define.UIEvent.PointerUp:
        //        if (evt.OnPointerUpHandler != null)
        //        {
        //            evt.OnPointerUpHandler -= clickAction;
        //            evt.OnPointerUpHandler += clickAction;
        //        }
        //        break;
        //    case Define.UIEvent.BeginDrag:
        //        if (evt.OnBeginDragHandler != null)
        //        {
        //            evt.OnBeginDragHandler -= dragAction;
        //            evt.OnBeginDragHandler += dragAction;
        //        }
        //        break;
        //    case Define.UIEvent.Drag:
        //        if (evt.OnDragHandler != null)
        //        {
        //            evt.OnDragHandler -= dragAction;
        //            evt.OnDragHandler += dragAction;
        //        }
        //        break;
        //    case Define.UIEvent.EndDrag:
        //        if (evt.OnEndDragHandler != null)
        //        {
        //            evt.OnEndDragHandler -= dragAction;
        //            evt.OnEndDragHandler += dragAction;
        //        }
        //        break;
        //    case Define.UIEvent.Preseed:
        //        if (evt.OnPressedHandler != null)
        //        {
        //            evt.OnPressedHandler -= clickAction;
        //            evt.OnPressedHandler += clickAction;
        //        }
        //        break;
        //}
        #endregion
    }

    private static void SetHandler(ref Action existingHandler, Action newHandler)
    {
        if (existingHandler != null)
            existingHandler -= newHandler;

        existingHandler += newHandler;
    }

    private static void SetHandler(ref Action<PointerEventData> existingHandler, Action<PointerEventData> newHandler)
    {
        if (existingHandler != null)
            existingHandler -= newHandler;

        existingHandler += newHandler;
    }

    #endregion

}
