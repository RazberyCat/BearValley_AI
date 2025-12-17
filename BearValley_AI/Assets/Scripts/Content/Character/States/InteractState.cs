using UnityEngine;

public class InteractState : State
{

    public override void Enter()
    {
        Debug.Log("Interact Start");
    }

    public override void Update()
    {
        // 상호작용 처리
    }

    public override void Exit()
    {
        Debug.Log("Interact End");
    }
}
