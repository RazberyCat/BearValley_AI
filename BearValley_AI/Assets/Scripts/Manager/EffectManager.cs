using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    /// <summary>
    /// 오브젝트 선언 및 사용
    /// 1. 파티클 오브젝트 선언
    /// 2. ResourcesLoad 함수에 프리팹 추가
    /// 3. 이펙트 재생 함수 선언 및 사용
    /// </summary>

    #region 파티클 오브젝트
    GameObject waveEffectObj;
    GameObject pixelSmallBurstEffectObj;
    GameObject anchorGripEffectObj;
    GameObject feverBackEffectObj;
    GameObject splashEffectObj;
    #endregion

    private GameObject _effects;

    public void Init()
    {
        ResourcesLoad();
    }

    void ResourcesLoad()
    {
        _effects = GameObject.Find("@Effects");
        // 파티클 프리팹 추가
        //waveEffectObj = Instantiate(Managers.Resource.Load<GameObject>("Wave"), _effects.transform);
        //waveEffectObj.GetComponent<ParticleSystem>().Stop();

    }

    #region 이펙트 재생
    public void WaveEffectPlay(Vector3 pos) 
    {
        //waveEffectObj.GetComponent<ParticleSystem>().Play();
    }
    #endregion
}
