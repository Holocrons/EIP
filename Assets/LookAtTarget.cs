using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public GameObject _target;
    public GameObject _base;
    public GameObject _effector;

    private Vector2 _targetCoor;
    private Vector2 _baseCoor;
    private Vector2 _effectorCoor;

    private float _targetBaseLenght;
    private float _baseEffectorLenght;
    private float _effectorTargetLenght;


    public void Turn()
    {
        float tmp;

        UpdateVal();
        tmp = _targetCoor.x * (_baseCoor.y - _effectorCoor.y) + _baseCoor.x * (_effectorCoor.y - _targetCoor.y) + _effectorCoor.x * (_targetCoor.y - _baseCoor.y);
        double t = Math.Pow(_targetBaseLenght, 2) + Math.Pow(_baseEffectorLenght, 2) - Math.Pow(_effectorTargetLenght, 2);
        t = t / (2 * _targetBaseLenght * _baseEffectorLenght);
        t = Math.Acos(t);
        t = t * 180 / Math.PI;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + (float)t);
        tmp = _targetCoor.x * (_baseCoor.y - _effectorCoor.y) + _baseCoor.x * (_effectorCoor.y - _targetCoor.y) + _effectorCoor.x * (_targetCoor.y - _baseCoor.y);
        if (tmp > 1.5f || tmp < -1.5f)
        {
            Turn();
        }
    }

    void UpdateVal()
    {
        _targetCoor = _target.transform.position;
        _baseCoor = _base.transform.position;
        _effectorCoor = _effector.transform.position;

        _targetBaseLenght = Vector2.Distance(_targetCoor, _baseCoor);
        _baseEffectorLenght = Vector2.Distance(_baseCoor, _effectorCoor);
        _effectorTargetLenght = Vector2.Distance(_effectorCoor, _targetCoor);
    }

}
