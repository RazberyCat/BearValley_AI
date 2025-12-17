using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using static Define;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : ControllerBase
{
    public void Init()
    {
        Managers.Input.onPlayerInputAction -= UpdateControl;
        Managers.Input.onPlayerInputAction += UpdateControl;
    }


    public override void UpdateControl()
    {
        if (actor == null || actor.FSM == null)
            return;

        // #Todo 인풋시스템 동작 추가
        var move = Managers.Input.GetMove();
        var jump = Managers.Input.GetJump();
        var NormalAttack = Managers.Input.GetNormalAttack();
        var StrongAttack = Managers.Input.GetStrongAttack();
        var Interact = Managers.Input.GetInteract();

        var cur = actor.FSM.CurrentStateType;

        // 이동 입력 유지 (move.x의 크기가 약간 남아있으면 Move 유지)
        if (Mathf.Abs(move.x) > 0.1f)
        {
            actor.SetMoveDirection(move.x > 0 ? Vector3.right : Vector3.left);
            if (cur != typeof(MoveState))
                actor.FSM.ChangeState<MoveState>();
            return;
        }
        else if (jump)
        {
            actor.FSM.ChangeState<JumpState>();
        }
        else if (NormalAttack)
        {
            actor.FSM.ChangeState<NormalAttackState>();
        }
        else if (StrongAttack)
        {
            actor.FSM.ChangeState<StrongAttackState>();
        }
        else if (Interact)
        {
            actor.FSM.ChangeState<InteractState>();
        }
        else if(cur != typeof(IdleState))
            actor.FSM.ChangeState<IdleState>();
    }

}
