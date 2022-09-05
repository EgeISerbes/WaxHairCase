using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxGenerator : MonoBehaviour
{
    [SerializeField] private InputManager _inputs;
    [SerializeField] private GameObject _wax_Shape_to_Spawn;
    private List<WaxBallBehaviour> _waxBalls = new List<WaxBallBehaviour>();
    private RaycastHit[] _hitInfo;
    private Vector3 _hitPoint;
    private Ray _tempRay;
    [SerializeField] private LayerMask _targetLayer;
    private bool _hasWax = false;
    private Vector3 _tempPos;
    [SerializeField] private float _waxShapeDiameterOffset;
    private Vector3 _deltaDistance;
    public enum WaxState
    {
        Idle,
        Started
    };
    [HideInInspector] public WaxState waxState = WaxState.Idle; 

    private void Start()
    {
        _tempPos = gameObject.transform.position;
    }

    void Update()
    {   
        if(waxState == WaxState.Started)
        {
            ProcessWax();
        }
    }

    void Generate()
    {
        var obj = Object.Instantiate(_wax_Shape_to_Spawn,_hitPoint,transform.rotation).GetComponent<WaxBallBehaviour>();
        _waxBalls.Add(obj);

    }

    void ProcessWax()
    {
        if (_inputs.IsPressed)
        {
            _tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            _hitInfo = Physics.RaycastAll(_tempRay, Mathf.Infinity, _targetLayer);
            foreach (RaycastHit hit in _hitInfo)
            {
                if (hit.collider.gameObject.CompareTag("WaxBall"))
                {
                    _hasWax = true;
                    Debug.Log("Dogru");
                }
                else if(hit.collider.gameObject.CompareTag("WaxLayer"))
                {
                    _hitPoint = hit.point;
                }
            }
            if (_hitInfo.Length != 0 && !_hasWax)
            {
                if(_inputs.InputVal !=Vector3.zero)
                {
                    Generate();
                }
                //if (_inputs.InputVal.x>_waxShapeDiameterOffset || _inputs.InputVal.z >= _waxShapeDiameterOffset)
                //{   

                //    Generate();
                //}
                
            }
            _tempPos = _inputs.WorldPos;
            _hasWax = false;
        }
    }
}
