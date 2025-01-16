using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChange;
    [SerializeField] private Unit _selectedUnit;
    [SerializeField] private LayerMask _unitLayermask;
    [SerializeField] private GameObject _actionBusyVisualImage, _gridSystemVisual, _enemyTurnVisualImage;
    private bool _isBusy;

    private BaseAction _selectedAction;
    [SerializeField] private GameObject _unitActionSystemUI;


    private void Awake()
    {
        if (Instance != null) 
        {
            Debug.LogError("There is more than one UnitActionSytem!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetSelectedUnit(_selectedUnit);
    }

    // Update is called once per frame
    void Update()
    {

        if (_isBusy) 
        {
            _gridSystemVisual.GetComponent<GridSystemVisual>().UpdateGridVisual();
            return;
        }
        if(!GameObject.Find("TurnSystem").GetComponent<TurnSystem>().IsPlayerTurn())
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (TryHandleUnitSelection())
        {
            return;
        }

        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MousePosWorld.GetPosition());

            if(_selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                if(_selectedUnit.TrySpendActionPointsToTakeAction(_selectedAction))
                {
                    SetBusy();
                    _selectedAction.TakeAction(mouseGridPosition, ClearBusy);
                }
                
            }            
        }
    }
    public void SetBusy()
    { 
        _isBusy = true;
        _actionBusyVisualImage.SetActive(true);
    }

    public void ClearBusy()
    { 
        _isBusy = false;
        _actionBusyVisualImage.SetActive(false);
        _unitActionSystemUI.GetComponent<UnitActionSystemUI>().UpdateActionPointsText();
        _gridSystemVisual.GetComponent<GridSystemVisual>().UpdateGridVisual();
    }

    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _unitLayermask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                { 
                    if (unit == _selectedUnit) 
                    {
                        return false;
                    }
                    if(unit.IsEnemy())
                    {
                        //Clicked on an enemy
                        return false;
                    }
                    SetSelectedUnit(unit);
                    _gridSystemVisual.GetComponent<GridSystemVisual>().UpdateGridVisual();
                    return true;
                }
            }
        }          

        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;

        SetSelectedAction(unit.GetAction<MoveAction>());

        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty); //This line does same as below
        //if (OnSelectedUnitChange != null) OnSelectedUnitChange(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        _selectedAction = baseAction;
        //_gridSystemVisual.GetComponent<GridSystemVisual>().UpdateGridVisual();
    }

    public Unit GetSelectedUnit() 
    { 
        return _selectedUnit; 
    }

    public BaseAction GetSelectedAction()
    {
        return _selectedAction;
    }
}
