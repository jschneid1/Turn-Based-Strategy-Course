using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        WaitingForEnemyTurn,
        TakingTurn,
        Busy,
    }

    [SerializeField] private State _state;

    private void Awake()
    {
        _state = State.WaitingForEnemyTurn;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameObject.Find("TurnSystem").GetComponent<TurnSystem>().IsPlayerTurn())
        {
            EnemyTurn();
        }
    }

    private void EnemyTurn()
    {
        switch(_state)
        {
            case State.WaitingForEnemyTurn:
                break;
            case State.TakingTurn:

                if (TryTakeEnemyAIAction())
                {
                    _state = State.Busy;
                    
                }
                else
                {
                    GameObject.Find("TurnSystem").GetComponent<TurnSystem>().NextTurn();
                    _state = State.WaitingForEnemyTurn;

                }
                break ;
            case State.Busy: 
                break ;
        }
        
        StopAllCoroutines();
    }

    public void SetStateTakingTurn()
    {
        _state = State.TakingTurn;
        Debug.Log("Enemy Taking Turn");
    }

    /*public void SetStateWaitingForEnemyTurn()
    {
        _state = State.WaitingForEnemyTurn;
    }*/

    //private bool TryTakeEnemyAIAction(Action onEnemyAIActionComplete)
    private bool TryTakeEnemyAIAction()
    {
        foreach (Unit enemyUnit in UnitManager.Instance.GetEnemyUnitList())
        {
            if (TryTakeEnemyAIAction(enemyUnit))
            {
                return true;
            }
            
        }
        return false;
    }
    private bool TryTakeEnemyAIAction(Unit enemyUnit)
    {
        EnemyAIAction bestEnemyAIAction = null;
        BaseAction bestBaseAction = null;

        foreach (BaseAction baseAction in enemyUnit.GetBaseActionArray())
        {
            if (!enemyUnit.CanSpendActionPointsToTakeAction(baseAction))
            {
                continue;
            }

            if (bestEnemyAIAction == null)
            {
                bestEnemyAIAction = baseAction.GetBestEnemyAIAction();
                bestBaseAction = baseAction;
            }
            else
            {
                EnemyAIAction testEnemyAIAction = baseAction.GetBestEnemyAIAction();
                if (testEnemyAIAction != null && testEnemyAIAction.actionValue > bestEnemyAIAction.actionValue)
                {
                    bestEnemyAIAction = baseAction.GetBestEnemyAIAction();
                    bestBaseAction = baseAction;
                }

            }
        }
        if (bestEnemyAIAction != null && enemyUnit.TrySpendActionPointsToTakeAction(bestBaseAction))
        {
            bestBaseAction.TakeAction(bestEnemyAIAction.gridPosition, Clearbusy);
            return true;
        }

        else
        {
            return false;
        }
    }

    private void Clearbusy()
    {
        Debug.Log("Clear busy in EnemyAI");
        //_state = State.WaitingForEnemyTurn;
        //StartCoroutine(EnemyAIClearBusyCoroutine());
        _state = State.TakingTurn;
    }
    IEnumerator EnemyAIClearBusyCoroutine()
    {

        Debug.Log("Clear Busy Visual in EnemyAI");
        //_state = State.TakingTurn;
        yield return new WaitForSeconds(3);
        //_state = State.TakingTurn;
        //GameObject.Find("UnitActionSystem").GetComponent<UnitActionSystem>().ClearBusy();


    }
   
}
