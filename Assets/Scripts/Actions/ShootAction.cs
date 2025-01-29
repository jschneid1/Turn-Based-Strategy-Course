using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootAction : BaseAction
{
    private enum State
    {
        Aiming,
        Shooting,
        Cooloff,
    }
    private State _state;
    private int _maxShootDistance = 5;
    private float _stateTimer;
    private Unit _targetUnit;
    private bool _canShootBullet;
    [SerializeField] private GameObject _actionVirtualCamera;
    [SerializeField] private LayerMask obstaclesLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

// Update is called once per frame
    void Update()
    {
        if (!_isActive)
        {
            return;
        }

        _stateTimer -= Time.deltaTime;

        //if (_stateTimer <= 0f)
        {
            NextState();
        }

    }

    private void NextState()
    {
        switch (_state)
        {
            case State.Aiming:
                
                float aimingStateTime = 0.5f;
                while(aimingStateTime > 0)
                {
                    aimingStateTime -= Time.deltaTime;
                    Vector3 aimDir = (_targetUnit.GetWorldPosition() - transform.position).normalized;
                    float rotateSpeed = 10.0f;
                    transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * rotateSpeed);
                }
                _actionVirtualCamera.SetActive(true);
                if (_stateTimer <= 0f)
                {
                    _state = State.Shooting;
                    float shootingStateTime = 0.3f;
                    _stateTimer = shootingStateTime;
                }
                break;
            case State.Shooting:
                if (_canShootBullet)
                {
                    Shoot();
                    _canShootBullet = false;
                }
                if (_stateTimer <= 0f)
                {
                    _state = State.Cooloff;
                    float coolOffStateTime = 1f;
                    _stateTimer = coolOffStateTime;
                }
                break;
            case State.Cooloff:
                if (_stateTimer <= 0f)
                {
                    _isActive = false;
                    _onActionComplete();
                }
                break;
        }
    }

    private void Shoot()
    {
        GetComponentInParent<UnitAnimator>().ShootAction_Shoot(_targetUnit);
        _targetUnit.Damage(40);
        StartCoroutine(CameraPause());
    }

    IEnumerator CameraPause()
    {
        yield return new WaitForSeconds(1f);
        _actionVirtualCamera.SetActive(false);
        StopAllCoroutines();
    }

    public override string GetActionName()
    {
        return "Shoot";
    }

    /*public override List<GridPosition> GetValidGridPositionList()
    {
        GridPosition unitGridPosition = _unit.GetGridPosition();
        return GetValidGridPositionList(unitGridPosition);
    }*/

    /*public override List<GridPosition> GetValidGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        for (int x = -_maxShootDistance; x <= _maxShootDistance; x++)
        {
            for (int z = -_maxShootDistance; z <= _maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    //Grid position is empty, no unit
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if(targetUnit.IsEnemy() == _unit.IsEnemy())
                {
                    //Both units on same team
                    continue;
                }

                Vector3 unitWorldPosition = LevelGrid.Instance.GetWorldPosition(unitGridPosition);
                Vector3 shootDirection = (targetUnit.GetWorldPosition() - unitWorldPosition).normalized;
                float unitShoulderHeight = 1.7f;

                if(Physics.Raycast(unitWorldPosition + Vector3.up * unitShoulderHeight, shootDirection, Vector3.Distance(unitWorldPosition, targetUnit.GetWorldPosition()), obstaclesLayerMask))
                {
                    //Blocked by an obstacle
                    continue;  
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }*/

    public override List<GridPosition> GetValidGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = _unit.GetGridPosition();

        for (int x = -_maxShootDistance; x <= _maxShootDistance; x++)
        {
            for (int z = -_maxShootDistance; z <= _maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    //Unit already occupies this position
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    //Grid position occupied by another unit
                    continue;
                }

                /*if (!PathFinding.Instance.IsWalkableGridPosition(testGridPosition))
                {
                    continue;
                }

                if (!PathFinding.Instance.IsEndPositionReachable(unitGridPosition, testGridPosition))
                {
                    continue;
                }

                int pathFindingDistanceMultiplier = 10;
                if (PathFinding.Instance.GetPathLength(unitGridPosition, testGridPosition) > _maxShootDistance * pathFindingDistanceMultiplier)
                {
                    //Path length is too long.
                    continue;
                }*/

                validGridPositionList.Add(testGridPosition);
            }
        }
        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action _onActionComplete)
    {
        this._onActionComplete = _onActionComplete;
        _isActive = true;

        _targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        
        _state = State.Aiming;
        float aimingStateTime = 1f;
        _stateTimer = aimingStateTime;

        _canShootBullet = true;
    }

    public Unit TargetUnit()
    {
        return _targetUnit;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 100 + Mathf.RoundToInt((1 - targetUnit.GetHealthNormalised()) * 100f),
        };
    }

    public int GetTargetCountAtPosition(GridPosition gridPosition)
    { 
        return GetValidGridPositionList().Count;
    }
}
