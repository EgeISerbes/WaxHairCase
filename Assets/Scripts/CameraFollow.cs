using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    //[SerializeField] private Stick _mainCharacter ;
    [SerializeField] private Transform _armPos;
    [HideInInspector] public float _playerFollowOffsetZ;
    private float _playerFollowOffsetX;
    public float playerFollowRateX;
    public float playerVelocityXApproachRate;
    [HideInInspector] public CamState camState = CamState.Idle;
    public Transform startPos;
    public Transform secondPos;
    [HideInInspector] public Camera camMain;
    public float approachRate = 0.6f;
    Vector3 temp = Vector3.zero;
    public float multiplier = 0.0f;
    public float cur_Multiplier = 1.0f;
    public enum CamState
    {
        Idle,
        Started,
        EndPhase
    };

    public void enableState()
    {
        camState = CamState.Started;
    }
    private void Awake()
    {
        camMain = Camera.main;
        enableState();
    }
    void Start()
    {
        transform.position = _armPos.position;
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        camMain.transform.position = startPos.position;
        camMain.transform.rotation = startPos.rotation;
    }
    //private void LateUpdate()
    //{
    //    if(camState == CamState.Started || camState == CamState.EndPhase)
    //    {
    //        temp = transform.position;
    //        temp = new Vector3(Mathf.MoveTowards(temp.x, (_mainCharacter.transform.position.x) * playerFollowRateX, playerVelocityXApproachRate), temp.y, Mathf.MoveTowards(temp.z, _mainCharacter.transform.position.z, _mainCharacter.VelocityZ * cur_Multiplier));
    //        transform.position = temp;

    //        if (camState == CamState.EndPhase)
    //        {
    //            camMain.transform.eulerAngles = new Vector3(Mathf.LerpAngle(camMain.transform.eulerAngles.x, secondPos.eulerAngles.x, approachRate), Mathf.LerpAngle(camMain.transform.eulerAngles.y, secondPos.eulerAngles.y, approachRate), Mathf.LerpAngle(camMain.transform.eulerAngles.z, secondPos.eulerAngles.z, approachRate));

    //            camMain.transform.position = new Vector3(Mathf.Lerp(camMain.transform.position.x, secondPos.position.x, approachRate), Mathf.Lerp(camMain.transform.position.y, secondPos.position.y, approachRate),Mathf.Lerp(camMain.transform.position.z,secondPos.position.z,approachRate));
    //        }
    //    }

    //}
    //public void setEndPhase()
    //{
    //    camState = CamState.EndPhase;
    //    cur_Multiplier = multiplier;
    //}


}
