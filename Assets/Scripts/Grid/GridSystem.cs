using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridSystem
{

    private int _width, _height;
    private float _cellSize;
    private GridObject[,] _gridObjectArray; //[,] means a 2d array


    public GridSystem(int width, int height, float cellSize)
    {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize; 
                
        _gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++) 
            {
                GridPosition gridPosition = new GridPosition(x, z);

                _gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) 
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * _cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x/_cellSize),
            Mathf.RoundToInt(worldPosition.z/_cellSize));
    }

    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return _gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.z >= 0 && gridPosition.x < _width && gridPosition.z < _height;
    }

    public int GetWidth()
    {
        return _width;
    }
    public int GetHeight() 
    {
        return _height;
    }
}
