using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{

    private StickMovement _stickMovement;
    [SerializeField] private WaxGenerator _waxGenerator;
    private void Awake()
    {
        _stickMovement = GetComponent<StickMovement>();   
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
    
}
