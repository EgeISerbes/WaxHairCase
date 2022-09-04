using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _targetPos;
    private InputValues _inputValues;
    private Camera _mainCam;
    [SerializeField] private bool _isAI;
    [Header("Character Settings")]
    public float velocityZ;
    [HideInInspector] public float currentVelocityZ;
    [Header("Character Settings(Not for AI)")]
    public float maxPositionX = 0.0f;
    public float maxForce;
    public float sideSpeed;
    public float maxVelocity;

    [SerializeField] private float _slowedAmount;

    [Header("Input Settings")]
    public float inputOffset = 1;
    private int _tempFingerID = -1;
    private Vector3 _prevPos = Vector3.zero;
    private Vector3 _tempMousePos;
    private Vector3 _tempWorldPos = Vector3.zero;
    public enum CharState
    {
        Idle,
        Started,
        EndPhase
    };

    [HideInInspector] public CharState charState = CharState.Idle;

    public bool IsPressed
    { get => _inputValues.isPressed; }
    public bool IsReleased
    { get => _inputValues.isReleased; }
    public Vector3 InputVal
    {
        get => _inputValues.inputPos;
    }
    
    private void Awake()
    {
        _mainCam = Camera.main;
        _rb = gameObject.GetComponent<Rigidbody>();
        currentVelocityZ = velocityZ;
    }

    private void Start()
    {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        _targetPos = _rb.position;
    }
    private void Update()
    {
        //GetInputs();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        ControlState();
    }

    void ProcessInput(float velocityZ, float sideSpeed)
    {
        currentVelocityZ = velocityZ;
        if (IsPressed)
        {
            currentVelocityZ = velocityZ * _slowedAmount;
        }

        var targetSideSpeed = sideSpeed * _inputValues.inputPos.x;
        var currentVelocity = _rb.velocity;
        currentVelocity.x = Mathf.Clamp(Mathf.MoveTowards(currentVelocity.x, targetSideSpeed, maxForce * Time.fixedDeltaTime), -maxVelocity, maxVelocity);
        _targetPos = _rb.position + new Vector3(currentVelocity.x, 0, currentVelocityZ) * Time.deltaTime;
        _targetPos = new Vector3(Mathf.Clamp(_targetPos.x, -maxPositionX, maxPositionX), _targetPos.y, _targetPos.z);

    }


    public struct InputValues
    {
        public bool isPressed;
        public bool isReleased;
        public Vector3 inputPos;
        public Vector3 worldPos;
    }

    void ControlState()
    {
        if (charState == CharState.Started)
        {
            if (_isAI)
            {

                _targetPos.z = _rb.position.z + (velocityZ) * Time.deltaTime;
                _rb.MovePosition(_targetPos);

            }
            else
            {

                ProcessInput(velocityZ, sideSpeed);
                _rb.MovePosition(_targetPos);


            }
        }
        else if (charState == CharState.EndPhase)
        {
            _targetPos.z = _rb.position.z + velocityZ * Time.deltaTime;
            _rb.MovePosition(_targetPos);
        }
    }
    //void GetInputs()
    //{
    //    _inputValues.isReleased = false; //Always set isReleased false on running
    //    if (Application.isEditor)
    //    {
    //        //_inputValues.isPressed = (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0)) ? true : false;
    //        //_inputValues.isReleased = (Input.GetMouseButtonUp(0)) ? true : false;
    //        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
    //        {
    //            _inputValues.isPressed = true;
    //            _tempMousePos = Input.mousePosition + new Vector3(0, 0, _mainCam.nearClipPlane + inputOffset);
    //            _tempWorldPos = _mainCam.ScreenToWorldPoint(_tempMousePos);
    //            if(Input.GetMouseButton(0))
    //            {
    //                _inputValues.inputPos = _tempWorldPos - _prevPos;
    //            }
    //            _prevPos = _tempWorldPos;
    //            _inputValues.worldPos = _tempWorldPos;
    //        }
    //        else if (Input.GetMouseButtonUp(0))
    //        {
    //            _inputValues.isReleased = true;
    //            _inputValues.inputPos = Vector3.zero;
    //        }

    //        Debug.Log(_inputValues.inputPos);
    //    }
    //    else
    //    {
    //        if (Input.touchCount > 0)
    //        {
    //            if (_tempFingerID == -1)
    //            {
    //                foreach (Touch inputs in Input.touches)
    //                {
    //                    if (inputs.phase == TouchPhase.Began)
    //                    {
    //                        _tempFingerID = inputs.fingerId;
    //                        _inputValues.isPressed = true;
    //                        _inputValues.isReleased = false;
    //                        _tempMousePos = new Vector3(inputs.position.x, inputs.position.y, 0) + new Vector3(0, 0, _mainCam.nearClipPlane + inputOffset);
    //                        _tempWorldPos = _mainCam.ScreenToWorldPoint(_tempMousePos);
    //                        _prevPos = _tempWorldPos;
    //                        _inputValues.worldPos = _tempWorldPos;

    //                    }
    //                }
    //            }
    //            else
    //            {


    //                foreach (Touch inputs in Input.touches)
    //                {
    //                    if (inputs.fingerId == _tempFingerID)
    //                    {
    //                        if(inputs.phase == TouchPhase.Moved || inputs.phase ==TouchPhase.Stationary)
    //                        {
    //                            _inputValues.isPressed = true;
    //                            _tempMousePos = Input.mousePosition + new Vector3(0, 0, _mainCam.nearClipPlane + inputOffset);
    //                            _tempWorldPos = _mainCam.ScreenToWorldPoint(_tempMousePos);
    //                            if(inputs.phase == TouchPhase.Moved)
    //                            {
    //                                _inputValues.inputPos = _tempWorldPos - _prevPos;
    //                            }
    //                            _prevPos = _tempWorldPos;
    //                            _inputValues.worldPos = _tempWorldPos;

    //                        }
    //                        else if (inputs.phase == TouchPhase.Ended || inputs.phase == TouchPhase.Canceled)
    //                        {
    //                            _inputValues.isReleased = true;
    //                            _inputValues.inputPos = Vector3.zero;
    //                            _tempFingerID = -1;
    //                        }
    //                        return;
    //                    }

    //                }
    //                _inputValues.isPressed = false;
    //                _inputValues.isReleased = true;
    //                _tempFingerID = -1;
    //            }
    //        }
    //    }
    //}
}
