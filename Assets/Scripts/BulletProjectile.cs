using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField]  private Unit _targetUnit;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private Transform _bulletHitVFXPrefab;
    [SerializeField] float _distanceToTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_targetUnit != null)
        {
            Vector3 targetUnitPosition = _targetUnit.GetWorldPosition();
            targetUnitPosition.y = transform.position.y;
            Vector3 moveDirection = (targetUnitPosition - transform.position).normalized;

            float distanceBeforeMoving = Vector3.Distance(transform.position, targetUnitPosition);
            float moveSpeed = 200f;

            
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            //_distanceToTarget = Mathf.Abs(targetUnitPosition.y - transform.position.y);
            float distanceAfterMoving = Vector3.Distance(transform.position, targetUnitPosition);

            if (distanceBeforeMoving < distanceAfterMoving)
            {
                transform.position = targetUnitPosition;
                _trailRenderer.transform.parent = null;
                Destroy(gameObject);
                Instantiate(_bulletHitVFXPrefab, targetUnitPosition, Quaternion.identity);
            }
        }


        
    }

    public void TargetUnit(Unit targetUnit)
    {
        _targetUnit = targetUnit;
        Vector3 targetUnitPosition = _targetUnit.GetWorldPosition();
        _distanceToTarget = Mathf.Abs(targetUnitPosition.y - transform.position.y);
    }
}
