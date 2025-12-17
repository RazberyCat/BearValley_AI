using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameScene : UI_Scene
{

    enum Images
    {
        //Background_Image,
        Test_Image,
    }
    enum Buttons
    {
        //Start_Button,
    }

    enum Texts
    {
        //Start_Text,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        //BindButton(typeof(Buttons));
        //BindText(typeof(Texts));
        //BindImage(typeof(Images));

        //GetButton((int)Buttons.Start_Button).gameObject.BindEvent(OnClickButton);
        //GetImage((int)Images.Test_Image).color= Color.red;
        //GameObject ImageObject = GetImage((int)Images.Test_Image).gameObject;
        //GetText((int)Texts.Start_Text).text = "Start_Text";
    }
}
