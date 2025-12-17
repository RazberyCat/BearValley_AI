using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler,  IEndDragHandler
{
    public Action OnClickHandler = null;
    public Action OnPointerDownHandler = null;
    public Action OnPressedHandler = null;
    public Action OnPointerUpHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnEndDragHandler = null;

    bool isPressed = false;
    const float PRESS_DURATION = 0.5f;
    float pressTime = 0f;

    void Update()
    {
        if (isPressed && Time.time - pressTime >= PRESS_DURATION)
        {
            OnPressedHandler?.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickHandler?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownHandler?.Invoke();

        isPressed = true;
        pressTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpHandler?.Invoke();
        isPressed = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragHandler?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragHandler?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragHandler?.Invoke(eventData);
    }

    public bool IsPressed()
    {
        return isPressed;
    }
}
