using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : BaseScene
{


    void Start()
    {
        Init();
       
    }


    protected override void Init()
    {
        base.Init();
        Debug.Log("StartScene");
        SceneType = Define.Scene.StartScene;

        Managers.Init();

        /// 어드레서블 라벨 기준으로 프리로드 라벨: "PreLoad"
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, maxCount) =>
        {
            Debug.Log($"캐시 확인 {count}/{maxCount} : {key}");

            if (count == maxCount)
            {
                
                Managers.Data.Init();
                Managers.Pool.Init();
                Managers.Sound.Init();
                Managers.Input.Init();

                Managers.Game.Init();
                Managers.GameSetting.Init();
                Managers.Effect.Init();
                Managers.UI.Init();

                Managers.CameraController.Init();
                Managers.PlayerController.Init();
                Debug.Log("초기화 완료");

                //Managers.Resource.ReleaseAll();

            }
        });

        Dictionary<int, Stats> dict = Managers.Data.TestDict;
    }


    public override void Clear()
    {
        Debug.Log("클리어");
    }

    public void GameStartButton() // 임시버튼
    {
        Managers.Scene.LoadScene(Define.Scene.GameScene);
    }

}
