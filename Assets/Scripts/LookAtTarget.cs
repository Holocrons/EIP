using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public GameObject _target;
    public GameObject _base;
    public GameObject _effector;
    public GameObject _joint;

    private Vector2 _targetCoor;
    private Vector2 _baseCoor;
    private Vector2 _effectorCoor;

    private float _targetBaseLenght;
    private float _baseEffectorLenght;
    private float _effectorTargetLenght;
    private float isAlligned = 100;

    private void Start()
    {
    }

    private void Update()
    {
        /*if (toTurn > 0)
        {
            transform.Rotate(new Vector3(0, 0, 1 * Time.deltaTime * 500));
            turning = true;
            UpdateVal();
            if (isAlligned < 1 && isAlligned > -1)
            {
                toTurn = -1;
                //turning = false;
            }
        }*/
    }

    public void Turn()
    {
        UpdateVal();
        if (isAlligned < 0.5f && isAlligned > -0.5f)
            return;
        double t = Math.Pow(_targetBaseLenght, 2) + Math.Pow(_baseEffectorLenght, 2) - Math.Pow(_effectorTargetLenght, 2);
        t = t / (2 * _targetBaseLenght * _baseEffectorLenght);
        t = Math.Acos(t);
        t = t * 180 / Math.PI;
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + (float)t);
        UpdateVal();
        if (isAlligned > 0.5f || isAlligned < -0.5f)
            transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z - ((float)t * 2));
    }

    void UpdateVal()
    {
        _targetCoor = _target.transform.position;
        _baseCoor = _base.transform.position;
        _effectorCoor = _effector.transform.position;

        _targetBaseLenght = Vector2.Distance(_targetCoor, _baseCoor);
        _baseEffectorLenght = Vector2.Distance(_baseCoor, _effectorCoor);
        _effectorTargetLenght = Vector2.Distance(_effectorCoor, _targetCoor);
        isAlligned = _targetCoor.x * (_baseCoor.y - _effectorCoor.y) + _baseCoor.x * (_effectorCoor.y - _targetCoor.y) + _effectorCoor.x * (_targetCoor.y - _baseCoor.y);
    }

}
