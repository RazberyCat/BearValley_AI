using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


public class Character : MonoBehaviour
{
    #region 기본 속성
    protected const float RUN_MOVE_SPEED = 7.5f; // 달리기 속도
    protected const float MOVE_MOVE_SPEED = 5f; // 이동 속도
    protected const float JUMP_HEIGHT = 2f; // 점프 높이
    #endregion

    #region 게임 오브젝트 속성
    protected GameObject _gameObject;
    public SpriteRenderer _sprite; // 스프라이트 (#임시 처리 public)
    public Animator _animator; // 애니메이터 (#임시 처리 public)

    protected string _prefabsName;
    protected string _spriteName;
    protected string _animatorName;

    private Vector3 moveDirection = Vector3.zero;

    // 스탯 (체력, 스태미나, 공격력, 공격속도, 이동속도)
    protected Stat _health = new Stat(); // 체력
    protected Stat _stamina = new Stat(); // 스태미나
    protected Stat _damage = new Stat(); // 공격력
    protected Stat _attakctSpeed = new Stat(); // 공격속도
    protected Stat _moveSpeed = new Stat();// 이동속도

    protected bool _isDead { get; set; }
    #endregion

    #region 상태 머신 속성
    public StateMachine FSM { get; protected set; }
    public Animator Animator => _animator;
    public Stat Health => _health;
    public Stat Stamina => _stamina;
    public Stat Damage => _damage;
    public Stat MoveSpeed => _moveSpeed;

    #endregion

    private void Start()
    {
        FSM = new StateMachine();
        Init();
    }

    protected virtual void Update()
    {
        FSM.Update();
    }

    #region 기능
    public void Init() // 초기화
    {
        if (FSM == null)
            FSM = new StateMachine();

        FSM.Actor = this;

        SetupStates();
        DataSetup();
        ResourcesSetup();
        SetInfo();
    }

    protected void SetupStates()
    {
        // 상태 추가
        var idle         = new IdleState();
        var jump         = new JumpState();
        var move         = new MoveState();
        var run          = new RunState();
        var interact     = new InteractState();
        var normalAttack = new NormalAttackState();
        var strongAttack = new StrongAttackState();
        var hit          = new HitState();
        var dead         = new DeadState();

        // 상태 추가
        FSM.AddState(idle);
        FSM.AddState(jump);
        FSM.AddState(move);
        FSM.AddState(run);
        FSM.AddState(interact);
        FSM.AddState(normalAttack);
        FSM.AddState(strongAttack);
        FSM.AddState(hit);
        FSM.AddState(dead);

        // 상태 업데이트 액션 설정
        idle.UpdateAction         = Idle;
        jump.UpdateAction         = Jump;
        move.UpdateAction         = Move;
        run.UpdateAction          = Run;
        interact.UpdateAction     = Interact;
        normalAttack.UpdateAction = NormalAttack;
        strongAttack.UpdateAction = StrongAttack;
        hit.UpdateAction          = Hit;
        dead.UpdateAction         = Dead;

        // 초기 상태 설정
        FSM.ChangeState<IdleState>();
    }

    public void SetMoveDirection(Vector3 dir) //이동 방향 설정
    {
        moveDirection = dir;
    }

    private void DataSetup()     // 데이터 초기화
    {
        // 체력 초기화
        _health.Init();
        _health.SetBaseValue(0); //#Todo 데이터(초기 체력)

        _stamina.Init();
        _damage.SetBaseValue(0); //#Todo 데이터(초기 공격력)

        _damage.Init();
        _attakctSpeed.Init();
        _attakctSpeed.SetBaseValue(0); //#Todo 데이터(초기 공격속도)

        _moveSpeed.Init();
        _moveSpeed.SetBaseValue(0); //#Todo 데이터(초기 이동속도)

        _isDead = false;
    }

    private void ResourcesSetup()
    {
        //_gameObject = Managers.Resource.Load<GameObject>(_prefabsName);
        //_sprite.sprite = Managers.Resource.Load<Sprite>(_spriteName);
        //_animator = Managers.Resource.Load<Animator>(_animatorName);
    }
    private void SetInfo()
    {
        // UI 정보 설정

    }
    #endregion


    #region 상태 머신 속성 (기본 상태 처리)
    private void Idle() // 대기
    {
        //Debug.Log("Character_Idle");
        Animator.Play("Cat_Idle", 0); // 대기 애니메이션 재생
        // 이동 상태 (2초 이동 후 대기, #Todo Move() 상태 처리 필요)
        // 상호작용 상태 (2초 상호작용 후 대기, #Todo Interact() 상태 처리 필요)
    }
    private void Move() // 이동
    {
        Animator.Play("Cat_Move", 0);
        transform.position += moveDirection * MOVE_MOVE_SPEED * Time.deltaTime;
        
        // 스프라이트 렌더러 방향 처리
        if (_sprite != null)
        {
            // 왼쪽으로 가면 뒤집기, 오른쪽으로 가면 뒤집기 해제
            _sprite.flipX = moveDirection.x < 0;
        }
        //Debug.Log("Character_Move");
        // 이동 상태 처리
        // if 이동 상태 처리
        // 대기 상태 처리
        // if 대기 상태 처리
        // 상호작용 상태 처리
        // 이동 상태 처리 종료
    }

    private void Jump() // 점프
    {
        Debug.Log("Character_Jump");
        // 점프 상태 처리
        // 점프 상태 처리 종료
    }
    private void Run() // 달리기
    {
        Animator.Play("Cat_Run", 0);
        transform.position += moveDirection * RUN_MOVE_SPEED * Time.deltaTime;
        
        // 스프라이트 렌더러 방향 처리
        if (_sprite != null)
        {
            // 왼쪽으로 가면 뒤집기, 오른쪽으로 가면 뒤집기 해제
            _sprite.flipX = moveDirection.x < 0;
        }
        
        //Debug.Log("Character_Run");
        // 스태미나 소모 처리 (#Todo 스태미나가 없으면 Move 상태로 전환)
    }
    private void Interact() // 상호작용
    {
        Debug.Log("Character_Interact");
        // 상호작용 상태 처리
        // 상호작용 상태 처리 종료
    }
    private void NormalAttack() // 일반 공격
    {
        Debug.Log("Character_NormalAttakct");
        // 일반 공격 상태 처리
        // 일반 공격 데미지 적용 (일반 공격 궗 업데이트, 최종 공격 Hit 상태 처리)
        // #Todo 데이터 처리 (일반 공격 데미지 적용 처리, 최종 공격 데미지 적용 처리)
    }

    private void StrongAttack() // 강력 공격
    {
        Debug.Log("Character_StrongAttakct");
        // 강력 공격 상태 처리
        // 강력 공격 데미지 적용
        // #Todo 데이터 처리 (강력 공격 데미지 적용 처리)
    }
    private void Hit() // 피격
    {
        Debug.Log("Character_Hit");
        // 피격 상태 처리
        // if 강력 공격 피격 처리
        // else 일반 공격 피격 처리
        // #Todo 데이터 처리 (피격 데미지 적용 처리)
    }
    private void Dead() // 사망
    {
        Debug.Log("Character_Dead");
        // 사망 상태 처리
        // 사망 상태 처리 종료
    }
    #endregion

}
