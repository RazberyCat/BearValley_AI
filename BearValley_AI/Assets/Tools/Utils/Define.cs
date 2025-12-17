using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{

    public enum Scene
    {
        Unknown,
        StartScene,
        GameScene,

    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount, // 최대 개수 파악용
    }

    public enum UIEvent
    {
        Click,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
        Preseed,
    }

    public enum InputCommand // #Todo 인풋매니저에서 인풋시스템으로 변경(삭제 필요)
    {
        None,
        MoveLeft,
        MoveRight,
        Jump,
        StrongAttack,
        NormalAttack,
        Interact,
    }

}
