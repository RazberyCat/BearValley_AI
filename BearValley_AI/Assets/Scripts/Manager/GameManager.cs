using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Action = System.Action;


public class GameManager : MonoBehaviour
{
    // 캐릭터
    public Character player;
    [SerializeField] GameObject _playerCharacter;

    public void OnUpdate()
    {

    }
    public void Init()
    {
        ResourcesLoad();
        SetInfo();

    }
    public void SetInfo()
    {


    }

    #region 기능
    public void GameReStart()
    {
        Managers.CameraController.Init();
    }

    public void AllClear()
    {
        player = null;
        _playerCharacter = null;

    }


    void ResourcesLoad() 
    {
        _playerCharacter = Managers.Resource.Load<GameObject>("Character"); 
        player = _playerCharacter.GetComponent<Character>();
    }

    public void CreatePlayerCharacter()
    {
        GameObject go = Instantiate(_playerCharacter.gameObject);
        player = go.GetComponent<Character>();
        player.Init();
        player.transform.position = Vector3.zero; // 초기 위치 설정
        Managers.PlayerController.SetCharacter(player); // 컨트롤러 연결
    }
    #endregion
}
