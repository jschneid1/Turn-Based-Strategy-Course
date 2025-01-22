using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PathNode
{
    private GridPosition _gridPosition;

    private int _gCost, _hCost, _fCost;

    private PathNode _cameFromPathNode;

    private bool _isWalkable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PathNode (GridPosition gridPosition)
    {
        this._gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return _gridPosition.ToString();
    }

    public int GetGCost()
    {
        return _gCost; 
    }

    public int GetFCost()
    {
        return _fCost;
    }

    public int GetHCost()
    {
        return _hCost;
    }

    public void SetGCost(int gCost)
    { 
        this._gCost = gCost;
    }

    public void SetHCost(int hCost)
    {
        this._hCost = hCost;
    }

    public void CalculateFCost()
    {
        _fCost = _gCost + _hCost;
    }

    public void ResetCameFromPathNode()
    { 
        _cameFromPathNode = null; 
    }

    public void SetCameFromPathNode(PathNode pathNode)
    { 
        _cameFromPathNode = pathNode;
    }

    public PathNode GetCameFromPathNode()
    {
        return _cameFromPathNode;
    }

    public GridPosition GetGridPosition()
    { 
        return _gridPosition;
    }

    public bool IsWalkable()
        { 
            return _isWalkable; 
    }

    public void SetIsWalkable(bool isWalkable)
    { 
        this._isWalkable = isWalkable;
    }
}
