using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float followSpeed = 5f;

    private Transform _playerTransform;
    //private Vector3 _offset;
    private float _minY;
    private float _maxY;

    public void Init(Transform target)
    {
        _playerTransform = target;
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }

    public void SetBound(float minY, float maxY)
    {
        _minY = minY;
        _maxY = maxY;
        
        Debug.Log($"{minY}, {maxY}");
    }

    void LateUpdate()
    {
        if (_playerTransform == null) return;

        float targetY = _playerTransform.position.y;
        float boundY = Mathf.Clamp(targetY, _minY, _maxY);

        Vector3 targetPosition = new Vector3(
            transform.position.x,
            Mathf.Lerp(transform.position.y, boundY, Time.unscaledDeltaTime * followSpeed),
            transform.position.z
        );

        transform.position = targetPosition;
    }
}
