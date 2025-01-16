using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveAction : BaseAction
{
    //public Event OnStartMoving;
    //public Event OnStopMoving;
    //[SerializeField] private GameObject _gridSystemVisual;
    private Vector3 _targetPosition;
    [SerializeField] private int _maxMoveDistance = 4;
    

    /*protected override void Awake()
    {
       
    }*/
    // Start is called before the first frame update
    void Start()
    {
        //_gridSystemVisual = GameObject.Find("GridSystemVisual");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive) 
        {
            return;
        }

        Vector3 moveDirection = (_targetPosition - transform.position).normalized;

        float stoppingDistance = 0.1f;
        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
        {
            

            float moveSpeed = 4.0f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            GetComponentInParent<UnitAnimator>().MoveAction_OnStartMoving();
        }

        else
        {
            //_unitAnimator.SetBool("IsWalking", false);
            GetComponentInParent<UnitAnimator>().MoveAction_OnStopMoving();
            _isActive = false;
            _onActionComplete();
        }

        float rotateSpeed = 10.0f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this._onActionComplete = onActionComplete;
        this._targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        _isActive = true;
        
        //OnStartMoving?.Invoke(this, EventArgs.Empty);
    }

    public override List<GridPosition> GetValidGridPositionList() 
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = _unit.GetGridPosition();

        for(int x = -_maxMoveDistance; x <= _maxMoveDistance; x++ )
        {
            for (int z = -_maxMoveDistance; z <= _maxMoveDistance; z++ )
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if(unitGridPosition == testGridPosition)
                {
                    //Unit already occupies this position
                    continue;
                }

                if(LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    //Grid position occupied by another unit
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        int targetCountAtGridPosition = _unit.GetAction<ShootAction>().GetTargetCountAtPosition(gridPosition);

        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = targetCountAtGridPosition * 10,
        };
    }
}
