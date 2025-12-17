using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TitleScene : UI_Scene
{

    enum Images
    {
        Background_Image,
        Test_Image,
    }
    enum Buttons
    {
        Start_Button,
    }

    enum Texts
    {
        Start_Text,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        //BindImage(typeof(Images));
        
        GetButton((int)Buttons.Start_Button).gameObject.BindEvent(OnClickButton);
        //GetButton((int)Buttons.Start_Button).gameObject.DragAndDropEvent();
        //GetImage((int)Images.Test_Image).color= Color.red;
        //GetImage((int)Images.Test_Image).gameObject.DragAndDropEvent();
        //GetImage((int)Images.Test_Image).gameObject.BindEvent(null, (PointerEventData data) => { HandleDragEvent(data); }, Define.UIEvent.Drag);
        //GetImage((int)Images.Test_Image).gameObject.BindEvent(null, HandleDragEvent, Define.UIEvent.EndDrag);

        //GameObject ImageObject = GetImage((int)Images.Test_Image).gameObject;
        //AddUIEvent(ImageObject, null, (PointerEventData data) => { HandleDragEvent(data,ImageObject); }, type: Define.UIEvent.Drag);
        GetText((int)Texts.Start_Text).text = "Start_Text";
    }

    public void OnClickButton()
    {
        Managers.Scene.LoadScene(Define.Scene.GameScene);
    }

    private void HandleDragEvent(PointerEventData data)
    {
        //GetText((int)Texts.Start_Text).text = "dddddddddddddddd";
        //ImageObject.transform.position = data.position;
    }
}
