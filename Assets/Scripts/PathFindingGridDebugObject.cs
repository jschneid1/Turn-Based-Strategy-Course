using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathFindingGridDebugObject : GridDebugObject
{
    [SerializeField] private TextMeshPro _gCostText, _fCostText, _hCostText;
    [SerializeField] private SpriteRenderer _isWalkableSpriteRenderer;

    private PathNode _pathNode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        _gCostText.text = _pathNode.GetGCost().ToString();
        _hCostText.text = _pathNode.GetHCost().ToString();
        _fCostText.text = _pathNode.GetFCost().ToString();
        _isWalkableSpriteRenderer.color = _pathNode.IsWalkable() ? Color.green : Color.red;
    }

    public override void SetGridObject(object gridObject)
    {
        base.SetGridObject(gridObject);
        _pathNode = (PathNode)gridObject;
    }
}
