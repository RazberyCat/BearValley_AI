using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    public static InputManager Instance { get; private set; }
    // 인풋 시스템
    public PlayerInputActions inputActions;
    public Action onPlayerInputAction = null;

    private string savePath => Path.Combine(Application.persistentDataPath, "input_rebinds.json");

    public void Init()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
        LoadRebinds();
    }

    public void OnUpdate()
    {
        onPlayerInputAction?.Invoke();
    }

    #region 기능
    // 키 반환
    public Vector2 GetMove() => inputActions.PlayerActions.Move.ReadValue<Vector2>();
    public bool GetJump() => inputActions.PlayerActions.Jump.WasPerformedThisFrame();
    public bool GetNormalAttack() => inputActions.PlayerActions.NormalAttack.WasPerformedThisFrame();
    public bool GetStrongAttack() => inputActions.PlayerActions.StrongAttack.WasPerformedThisFrame();
    public bool GetInteract() => inputActions.PlayerActions.Interact.WasPerformedThisFrame();
    public bool GetRun() => inputActions.PlayerActions.Sprint.IsPressed();

    // 저장 / 불러오기
    public void SaveRebinds()
    {
        var rebinds = inputActions.SaveBindingOverridesAsJson();
        File.WriteAllText(savePath, rebinds);
        Debug.Log("Input rebinds saved.");
    }

    public void LoadRebinds()
    {
        if (File.Exists(savePath))
        {
            var json = File.ReadAllText(savePath);
            inputActions.LoadBindingOverridesFromJson(json);
            Debug.Log("Input rebinds loaded.");
        }
    }

    // 런타임 리바인딩
    public void StartRebind(string actionName, int bindingIndex, System.Action onComplete = null)
    {
        var action = inputActions.asset.FindAction(actionName);
        if (action == null)
        {
            Debug.LogError($"Action {actionName} not found!");
            return;
        }

        var rebind = action.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(operation =>
            {
                operation.Dispose();
                SaveRebinds();
                onComplete?.Invoke();
            });

        rebind.Start();
    }
    //public void StartRebind(string actionName, System.Action onComplete)
    //{
    //    var action = inputActions.FindAction(actionName, true);
    //    action.Disable();

    //    action.PerformInteractiveRebinding()
    //        .WithControlsExcluding("Mouse")
    //        .OnComplete(callback =>
    //        {
    //            callback.Dispose();
    //            action.Enable();
    //            onComplete?.Invoke();
    //        })
    //        .Start();
    //}
    #endregion

}
