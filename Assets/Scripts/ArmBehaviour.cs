using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 _targetPos;
    [SerializeField] private float _approachRate;
    private bool _canMove;
    [SerializeField] private Transform _armBodyTr;

    public void Init()
    {
        _canMove = true;
    }

    private void Update()
    {
        if (_canMove)
        {
            _armBodyTr.position = Vector3.Lerp(_armBodyTr.position, _targetPos, _approachRate);
        }
    }

}
