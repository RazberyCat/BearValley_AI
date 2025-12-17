using UnityEngine;

public class DeadState : State
{


    public override void Enter()
    {
        Debug.Log("Dead Start");
    }

    public override void Update()
    {
        // Á×À½ Ã³¸®
    }

    public override void Exit()
    {
        Debug.Log("Dead End");
    }
}
