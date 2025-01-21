using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridSystemVisual;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private GridSystemVisual _gridSystemVisual;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_gridSystem.GetGridPosition(MousePosWorld.GetPosition()));
        if(Input.GetKeyDown(KeyCode.T)) 
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MousePosWorld.GetPosition());
            GridPosition startGridPosition = new GridPosition(0, 0);

            List<GridPosition> gridPositionList = PathFinding.Instance.FindPath(startGridPosition, mouseGridPosition);

            for (int i = 0; i < gridPositionList.Count - 1; i++)
            {
                Debug.DrawLine(LevelGrid.Instance.GetWorldPosition(gridPositionList[i]), LevelGrid.Instance.GetWorldPosition(gridPositionList[i + 1]),
                    Color.white, 10f);
            }
        }     
    }
}
