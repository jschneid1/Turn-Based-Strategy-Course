using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LookAtCamera : MonoBehaviour
{
    private Transform _cameraTransform;
    [SerializeField] private bool _invert;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if(_invert)
        {
            Vector3 dirToCamera = (_cameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position + dirToCamera * -1);
        }
        else
        {
            transform.LookAt(_cameraTransform);
        }
        
    }
}
