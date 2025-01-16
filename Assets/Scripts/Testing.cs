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
        /*if(Input.GetKeyDown(KeyCode.T)) 
        {
            GridSystemVisual.Instance.HideAllGridPositions();
            GridSystemVisual.Instance.ShowGridPositionList(_unit.GetMoveAction().GetValidGridPositionList(), gridVisualType);
        }  */      
    }
}
