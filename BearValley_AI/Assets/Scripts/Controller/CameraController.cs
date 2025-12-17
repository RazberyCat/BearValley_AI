using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _target = null;
    [SerializeField] Camera _camera;

    bool _isInit = false;

    float moveSpeed = 4.5f;
    float distance = 10.0f;
    float height = 10.0f;

    public void Init()
    {
        _target = Managers.Game.player.gameObject.transform;
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _target.transform.position = Vector3.zero;
        _isInit = true;
    }

    public void SetInfo(Transform target)
    {
        _target = target;
    }

    void FixedUpdate()
    {
        // 카메라가 캐릭터를 추적
        //if (_isInit)
        //{
        //    // ( target.position.y - 1f ) 의 -1f 값은 위치 보정값 -일수록 아래로
        //    var camPos = (_target.position.y - 7f) * Vector3.up + Vector3.up * height - Vector3.forward * distance;
        //    _camera.transform.position = Vector3.Slerp(_camera.transform.position, camPos, Time.deltaTime * moveSpeed);
        //}

    }
}
