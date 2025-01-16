using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{

    private GridObject _gridObject;
    [SerializeField] private TextMeshPro _textMeshPro;

    public void SetGridObject(GridObject gridObject)
    {
        this._gridObject = gridObject;
    }

    private void Update()
    {
        _textMeshPro.text = _gridObject.ToString();
    }
}
