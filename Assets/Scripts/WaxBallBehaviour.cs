using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxBallBehaviour : MonoBehaviour
{
    [SerializeField] private float _expandRatio;
    private float _targetSize;
    private float _currentSize;
    [SerializeField] private float _increase_Amount_by_Frame;
    private bool _hasExpanded = false;
    [SerializeField] private float _secondExpandAmount = 2;
    void Awake()
    {
        _currentSize = transform.localScale.x;
        _targetSize = transform.localScale.x * _expandRatio;

    }
    public void SecondExpansion()
    {
        if (_hasExpanded) return;
        _targetSize *= _secondExpandAmount;
        _hasExpanded = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("WaxBall"))
        {
            var waxball = collision.gameObject.GetComponent<WaxBallBehaviour>();
            waxball.SecondExpansion();
        }
    }
    // Update is called once per frame
    void Update()
    {
        IncreaseSize();
    }
    void IncreaseSize()
    {
        _currentSize += _increase_Amount_by_Frame * Time.deltaTime;
        _currentSize = (_currentSize >= _targetSize) ? _targetSize : _currentSize;
        transform.localScale = new Vector3(_currentSize, transform.localScale.y, _currentSize);

    }
}
