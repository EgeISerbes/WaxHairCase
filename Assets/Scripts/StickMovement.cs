using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _targetPos;
    private InputManager _inputs;
    [Header("Character Settings")]
    public float velocityZ;
    [HideInInspector] public float currentVelocityZ;
    [Header("Character Settings(Not for AI)")]
    public float maxPositionX = 0.0f;
    public float maxForce;
    public float sideSpeed;
    public float maxVelocity;

    [SerializeField] private float _slowedAmount;
    public enum CharState
    {
        Idle,
        Started,
        EndPhase
    };

    [HideInInspector] public CharState charState = CharState.Idle;
    void Start()
    {
        StartCoroutine(LateStart());
    }
    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }
    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        _targetPos = _rb.position;
    }
    
    void FixedUpdate()
    {
        ControlState();
    }
    void ControlState()
    {
        if (charState == CharState.Started)
        {
            ProcessInput();
            _rb.MovePosition(_targetPos); 
        }
        
    }

    void ProcessInput()
    {
        if(_inputs.IsPressed)
        {
            _targetPos = _inputs.WorldPos + _inputs.InputVal;
        }
    }
}
