using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputValues _inputValues;
    private Camera _mainCam;
    public float inputOffset = 1;
    private int _tempFingerID = -1;
    private Vector3 _prevPos = Vector3.zero;
    private Vector3 _tempMousePos;
    private Vector3 _tempWorldPos = Vector3.zero;
    public bool IsPressed
    { get => _inputValues.isPressed; }
    public bool IsReleased
    { get => _inputValues.isReleased; }
    public Vector3 InputVal
    {
        get => _inputValues.inputPos;
    }
    public Vector3 WorldPos
    {
        get => _inputValues.worldPos; 
    }
    private void Awake()
    {
        _mainCam = Camera.main;
    }
    void Update()
    {
        GetInputs();
    }
    public struct InputValues
    {
        public bool isPressed;
        public bool isReleased;
        public Vector3 inputPos;
        public Vector3 worldPos;
    }
    void GetInputs()
    {
        _inputValues.isReleased = false; //Always set isReleased false on running
        if (Application.isEditor)
        {
            //_inputValues.isPressed = (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0)) ? true : false;
            //_inputValues.isReleased = (Input.GetMouseButtonUp(0)) ? true : false;
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                _inputValues.isPressed = true;
                _tempMousePos = Input.mousePosition + new Vector3(0, 0, _mainCam.nearClipPlane + inputOffset);
                _tempWorldPos = _mainCam.ScreenToWorldPoint(_tempMousePos);
                if (Input.GetMouseButton(0))
                {
                    _inputValues.inputPos = _tempWorldPos - _prevPos;
                }
                _prevPos = _tempWorldPos;
                _inputValues.worldPos = _tempWorldPos;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _inputValues.isReleased = true;
                _inputValues.inputPos = Vector3.zero;
            }

            Debug.Log(_inputValues.inputPos);
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (_tempFingerID == -1)
                {
                    foreach (Touch inputs in Input.touches)
                    {
                        if (inputs.phase == TouchPhase.Began)
                        {
                            _tempFingerID = inputs.fingerId;
                            _inputValues.isPressed = true;
                            _inputValues.isReleased = false;
                            _tempMousePos = new Vector3(inputs.position.x, inputs.position.y, 0) + new Vector3(0, 0, _mainCam.nearClipPlane + inputOffset);
                            _tempWorldPos = _mainCam.ScreenToWorldPoint(_tempMousePos);
                            _prevPos = _tempWorldPos;
                            _inputValues.worldPos = _tempWorldPos;
                        }
                    }
                }
                else
                {


                    foreach (Touch inputs in Input.touches)
                    {
                        if (inputs.fingerId == _tempFingerID)
                        {
                            if (inputs.phase == TouchPhase.Moved || inputs.phase == TouchPhase.Stationary)
                            {
                                _inputValues.isPressed = true;
                                _tempMousePos = Input.mousePosition + new Vector3(0, 0, _mainCam.nearClipPlane + inputOffset);
                                _tempWorldPos = _mainCam.ScreenToWorldPoint(_tempMousePos);
                                if (inputs.phase == TouchPhase.Moved)
                                {
                                    _inputValues.inputPos = _tempWorldPos - _prevPos;
                                }
                                _prevPos = _tempWorldPos;
                                _inputValues.worldPos = _tempWorldPos;
                            }
                            else if (inputs.phase == TouchPhase.Ended || inputs.phase == TouchPhase.Canceled)
                            {
                                _inputValues.isReleased = true;
                                _inputValues.inputPos = Vector3.zero;
                                _tempFingerID = -1;
                            }
                            return;
                        }

                    }
                    _inputValues.isPressed = false;
                    _inputValues.isReleased = true;
                    _tempFingerID = -1;
                }
            }
        }
    }
}
