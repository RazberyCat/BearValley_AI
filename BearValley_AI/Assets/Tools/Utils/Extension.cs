using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension 
{
    public static void BindEvent(this GameObject gameObject, Action clickAction = null, Action<PointerEventData> dragAction = null, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(gameObject, clickAction, dragAction, type);
    }

    public static void DragAndDropEvent(this GameObject gameObject)     // #Todo 아직 미완성 그냥 드래그해서 옮기는게 다임
    {
        //UI_Base.BindEvent(gameObject, null, (PointerEventData data) => {
        //    gameObject.transform.position = data.position;
        //}, Define.UIEvent.Drag);
    }
}
