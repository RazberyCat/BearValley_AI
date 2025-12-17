using UnityEngine;

public class Managers : MonoBehaviour
{
    /// <summary>
    /// 매니저 선언
    /// 1. 매니저 맴버변수 선언 및 동적할당
    /// 2. 스택틱 변수 선언 및 싱글턴화
    /// 3. Init 구성
    /// </summary>

    static Managers _instance;
    static Managers Instance { get { Init(); return _instance; } }

    #region 매니저
    GameManager _game;
    EffectManager _effect;

    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResouceManager _resource = new ResouceManager(); 
    DataManager _data = new DataManager();
    SoundManager _sound = new SoundManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();
    GameSettingManager _setting = new GameSettingManager();

    public static GameManager Game { get { return Instance?._game; } }
    public static InputManager Input { get { return Instance?._input; } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static ResouceManager Resource { get { return Instance?._resource; } }
    public static DataManager Data { get { return Instance?._data; } }
    public static SoundManager Sound { get { return Instance?._sound; } }
    public static EffectManager Effect { get { return Instance?._effect; } }
    public static SceneManagerEx Scene { get { return Instance?._scene; } }
    public static UIManager UI { get { return Instance?._ui; } }
    public static GameSettingManager GameSetting { get { return Instance?._setting; } }
    #endregion

    #region 컨트롤러
    PlayerController _playerController;
    CameraController _cameraController;
    public static PlayerController PlayerController { get { return Instance?._playerController; } }
    public static CameraController CameraController { get { return Instance?._cameraController; } }
    #endregion

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Input.OnUpdate();
        Game.OnUpdate();
        _playerController.UpdateControl();
    }

    public static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            DontDestroyOnLoad(go);

            //MonoBehaviour 상속 개체의 instance 할당
            _instance = go.GetComponent<Managers>();
            _instance._game = go.AddComponent<GameManager>();
            _instance._effect = go.AddComponent<EffectManager>();

            GameObject controller = GameObject.Find("@Controller");
            DontDestroyOnLoad(controller);
            _instance._cameraController = controller.AddComponent<CameraController>();
            _instance._playerController = controller.AddComponent<PlayerController>();

        }
    }

    public static void Clear()
    {
        Scene.Clear();
        Sound.Clear();
        UI.Clear();

        Pool.Clear();  

        // Todo 나머지 매니저 클리어 추가
    }
}
