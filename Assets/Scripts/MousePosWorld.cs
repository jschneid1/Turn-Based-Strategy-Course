using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosWorld : MonoBehaviour
{
    private static MousePosWorld instance;

    [SerializeField]
    private LayerMask _mousePlaneLayermask;

    private void Awake()
    {
        instance = this;
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance._mousePlaneLayermask);
        return raycastHit.point;
    }
}
