using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{

    private StickMovement _stickMovement;
    [SerializeField] private WaxGenerator _waxGenerator;
    [Header("End Game Settings")]
    [SerializeField] private Vector3 _finishPos;
    [SerializeField] private float _approachRate;
    private bool _hasEnded = false;
    private void Awake()
    {
        _stickMovement = GetComponent<StickMovement>();
    }

    private void Update()
    {
        if (_hasEnded)
        {
            transform.position = Vector3.Lerp(transform.position, _finishPos, _approachRate);
        }
    }
    public void StartStates()
    {
        _stickMovement.charState = StickMovement.CharState.Started;
        _waxGenerator.waxState = WaxGenerator.WaxState.Started;

    }
    public void StopStates()
    {
        _stickMovement.charState = StickMovement.CharState.Idle;
        _waxGenerator.waxState = WaxGenerator.WaxState.Idle;
    }
    public void FinishMove()
    {
        _hasEnded = true;
    }

}
