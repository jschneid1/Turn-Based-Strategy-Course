using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{

    private int _turnNumber = 1;
    private bool _isPlayerTurn = true;
    [SerializeField] private GameObject[] _units, _enemyUnits;
    [SerializeField] private GameObject _endTurnButton;

    // Start is called before the first frame update
    void Start()
    {
        _units = GameObject.FindGameObjectsWithTag("Player");
        _enemyUnits = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsPlayerTurn()
        { 
            return _isPlayerTurn; 
        }

    public void NextTurn()
    {
        _turnNumber++;
        GameObject.Find("Canvas/TurnSystemUI").GetComponent<TurnSystemUI>().UpdateTurnNumberText(_turnNumber);
        if (IsPlayerTurn())
        {
            _isPlayerTurn = false;
            GameObject.Find("EnemyAI").GetComponent<EnemyAI>().SetStateTakingTurn();
            //GameObject.Find("Canvas/TurnSystemUI").GetComponent<TurnSystemUI>().UpdateTurnNumberText(_turnNumber);
            GameObject.Find("Canvas/TurnSystemUI").GetComponent<TurnSystemUI>().EnemyActionImageEnable();
            _endTurnButton.SetActive(false);
            foreach (GameObject unit in _enemyUnits)
            {
                if(unit != null)
                {
                    unit.GetComponent<Unit>().OnTurnChanged();
                }
            }
            //GameObject.Find("UnitActionSystemUI").GetComponent<UnitActionSystemUI>().UpdateActionPointsText();
        }
        else
        {
            _isPlayerTurn = true;
            //GameObject.Find("EnemyAI").GetComponent<EnemyAI>().SetStateWaitingForEnemyTurn();
            GameObject.Find("Canvas/TurnSystemUI").GetComponent<TurnSystemUI>().EnemyActionImageDisable();
            _endTurnButton.SetActive(true);
            foreach (GameObject unit in _units)
            {
                if (unit != null)
                {
                    unit.GetComponent<Unit>().OnTurnChanged();
                }
            }
        }
    }
}
