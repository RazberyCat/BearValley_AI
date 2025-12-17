using UnityEngine;
using static Define;

public abstract class ControllerBase : MonoBehaviour
{
    protected Character actor;

    public void SetCharacter(Character target) // 캐릭터 생성시 설정해야함
    {
        actor = target;
    }

    // 매 프레임 어떤 명령을 보낼지
    public abstract void UpdateControl();
}
