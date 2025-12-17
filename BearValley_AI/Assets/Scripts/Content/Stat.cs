using System;
using UnityEngine;

public class Stat
{
    #region 기본 속성
    private bool _isBaseInit = false;
    private float _baseValue;
    private float _flatBonus;
    private float _percentBonus;
    #endregion

    #region 읽기 전용 속성
    public float BaseValue => _baseValue;
    public float FlatBonus => _flatBonus;
    public float PercentBonus => _percentBonus;
    public float FinalValue => (_baseValue + _flatBonus) * (1 + _percentBonus);
    #endregion

    public void Init()
    {
        _isBaseInit = false;
        _baseValue = 0;
        _flatBonus = 0f;
        _percentBonus = 0f;
    }

    public void SetBaseValue(float value)
    {
        if (_isBaseInit)
        {
            Debug.LogError("BaseValue는 한 번만 설정가능.");
            return;
        }

        _baseValue = value;
        _isBaseInit = true;
    }

    public void AddFlatBonus(float value) => _flatBonus += value;
    public void AddPercentBonus(float value) => _percentBonus += value;


    // #Todo 버프, 디버프가 걸렸을때 빼지말고 리프레시로 처리
    //public void RemoveFlatBonus(float value) => _flatBonus -= value;
    //public void RemovePercentBonus(float value) => _percentBonus -= value;
}
