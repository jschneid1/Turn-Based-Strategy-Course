using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private const float MIN_FOLLOW_Y_OFFSET = 2.0f, MAX_FOLLOW_Y_OFFSET = 12.0f;

    private Vector3 _targetFollowOffset;
    private CinemachineTransposer _cinemachineTransposer;
    // Start is called before the first frame update
    void Start()
    {
        _cinemachineTransposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _targetFollowOffset = _cinemachineTransposer.m_FollowOffset;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraMovement();
        HandleCameraRotation();
        HandleCameraZoom();
    }

    private void HandleCameraMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1.0f;
        }

        float cameraMoveSpeed = 10.0f;

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * cameraMoveSpeed * Time.deltaTime;
    }

    private void HandleCameraRotation()
    {
        Vector3 cameraRotationVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Q))
        {
            cameraRotationVector.y = +1.0f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            cameraRotationVector.y = -1.0f;
        }

        float cameraRotationSpeed = 100.0f;
        transform.eulerAngles += cameraRotationVector * cameraRotationSpeed * Time.deltaTime;        
    }

    private void HandleCameraZoom() 
    {
        float zoomAmount = 1.0f, zoomSpeed = 5.0f;
        if (Input.mouseScrollDelta.y > 0)
        {
            _targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            _targetFollowOffset.y += zoomAmount;
        }

        _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);

        _cinemachineTransposer.m_FollowOffset = Vector3.Lerp(_cinemachineTransposer.m_FollowOffset, _targetFollowOffset, Time.deltaTime * zoomSpeed);
    }
}
