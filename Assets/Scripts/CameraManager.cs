using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject _actionCameraGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowActionCamera()
    {
        _actionCameraGameObject.SetActive(true);
    }

    public void HideActionCamera()
    { 
        _actionCameraGameObject.SetActive(false);
    }

}
