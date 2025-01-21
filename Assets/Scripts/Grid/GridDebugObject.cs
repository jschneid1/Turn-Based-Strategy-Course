using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{

    private object _gridObject;
    [SerializeField] private TextMeshPro _textMeshPro;

    public virtual void SetGridObject(object gridObject)
    {
        this._gridObject = gridObject;
    }

    protected virtual void Update()
    {
        _textMeshPro.text = _gridObject.ToString();
    }
}
