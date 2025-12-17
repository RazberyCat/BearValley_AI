
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameScene : BaseScene
{
    void Start()
    {
        Debug.Log("GameScene");
        Init();
    }

    protected override void Init()
    {
        SceneType = Define.Scene.GameScene;
        Managers.UI.ShowSceneUI<UI_GameScene>();
        Managers.Game.CreatePlayerCharacter();
    }


    public override void Clear()
    {

    }

}
