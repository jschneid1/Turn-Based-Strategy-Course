using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const int ACTION_POINTS_MAX = 2;
    private HealthSystem _healthSystem;
    private GridPosition _gridPosition;
    /*private MoveAction _moveAction;
    private SpinAction _spinAction;
    private ShootAction _shootAction;*/
    private BaseAction[] _baseActionArray;
    private int _actionPoints = ACTION_POINTS_MAX;
    [SerializeField] private Transform _ragDollPrefab, _unitWorldUI, _unitManager;

    [SerializeField] private bool _isEnemy, _enemyMoved;

    private void Awake()
    {
        /*_moveAction = GetComponent<MoveAction>();
        _spinAction = GetComponent<SpinAction>();
        _shootAction = GetComponent<ShootAction>();*/
        _baseActionArray = GetComponents<BaseAction>();
        _healthSystem = GetComponent<HealthSystem>();
        _unitManager = GameObject.Find("UnitManager").transform;
    }
    private void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_gridPosition, this);
        _unitManager.GetComponent<UnitManager>().OnAnyUnitSpawned(this);
    }

    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != _gridPosition)
        {
            //unit changed position
            LevelGrid.Instance.UnitMovedGridPosition(this, _gridPosition, newGridPosition);
            _gridPosition = newGridPosition;
        }
    }

    public T GetAction<T>() where T : BaseAction
    {
        foreach (BaseAction baseAction in _baseActionArray)
        {
            if(baseAction is T)
            {
                return (T)baseAction;
            }
        }
        return null;
    }
    /*public MoveAction GetMoveAction()
    { 
        return _moveAction; 
    }

    public SpinAction GetSpinAction() 
    {
        return _spinAction;
    }

    public ShootAction GetShootAction()
    {
        return _shootAction;
    }*/

    public GridPosition GetGridPosition() 
    {
        return _gridPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public BaseAction[] GetBaseActionArray()
    { 
        return _baseActionArray;
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (CanSpendActionPointsToTakeAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if(_actionPoints >= baseAction.GetActionPointsCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsEnemy()
    {
        return _isEnemy;
    }

    public void EnemyHasMoved()
        { 
            _enemyMoved = true; 
        }
    public bool EnemyMoved()
        { 
            return _enemyMoved; 
        }

    private void SpendActionPoints(int amount)
    {
        _actionPoints -= amount;
        _unitWorldUI.GetComponent<UnitWorldUI>().UpdateActionPointsText(_actionPoints);
    }

    public int GetActionPoints()
    {
        return _actionPoints; 
    }

    public void OnTurnChanged()
    {
        _actionPoints = ACTION_POINTS_MAX;
        _unitWorldUI.GetComponent<UnitWorldUI>().UpdateActionPointsText(_actionPoints);
    }

    public void Damage(int damageAmount)
    {
        _healthSystem.Damage(damageAmount);
        _unitWorldUI.GetComponent<UnitWorldUI>().UpdateHealthBar();
    }

    public void Die()
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(_gridPosition,this);
        _unitManager.GetComponent<UnitManager>().OnAnyUnitDied(this);

        Destroy(gameObject);

        Transform ragDollTransform = Instantiate(_ragDollPrefab, transform.position, transform.rotation);
        UnitRagDoll unitRagDoll = _ragDollPrefab.GetComponent<UnitRagDoll>();
        MatchAllChildTransforms(this.transform, ragDollTransform );
    }

    private void MatchAllChildTransforms(Transform root, Transform clone)
    {
        foreach (Transform child in root)
        {
            Transform cloneChild = clone.Find(child.name);
            if (cloneChild != null)
            {
                cloneChild.position = child.position;
                cloneChild.rotation = child.rotation;

                MatchAllChildTransforms(child, cloneChild);
            }
        }
    }

    public float GetHealthNormalised()
    {
        return _healthSystem.GetHealthNormalized();
    }
}
