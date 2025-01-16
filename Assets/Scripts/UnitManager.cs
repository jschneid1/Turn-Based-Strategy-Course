using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    private List<Unit> _unitList;
    private List<Unit> _friendlyUnitList;
    public List<Unit> _enemyUnitList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one UnitManager!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _unitList = new List<Unit>();
        _friendlyUnitList = new List<Unit>();
        _enemyUnitList = new List<Unit>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnyUnitSpawned(Unit unit)
    {
        _unitList.Add(unit);

        if(unit.IsEnemy())
        {
            _enemyUnitList.Add(unit);
        }
        else
        {
            _friendlyUnitList.Add(unit);
        }
    }

    public void OnAnyUnitDied(Unit unit)
    {
        _unitList.Remove(unit);

        if (unit.IsEnemy())
        {
            _enemyUnitList.Remove(unit);
        }
        else
        {
            _friendlyUnitList.Remove(unit);
        }
    }

    public List<Unit> GetUnitList()
    { 
        return _unitList;
    }

    public List<Unit> GetEnemyUnitList()
    {
        return _enemyUnitList;
    }

    public List<Unit> GetFriendlyUnitList()
    {
        return _friendlyUnitList;
    }
}
