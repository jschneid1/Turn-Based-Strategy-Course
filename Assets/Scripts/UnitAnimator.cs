using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _bulletProjectilePrefab;
    [SerializeField] private Transform _shootPointTransform;
    // Start is called before the first frame update
    private void Awake()
    {
        /*if(TryGetComponent<MoveAction>(out MoveAction moveAction))
        {

        }*/
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveAction_OnStartMoving()
    {
        _animator.SetBool("IsWalking", true);
    }

    public void MoveAction_OnStopMoving()
    {
        _animator.SetBool("IsWalking", false);
    }

    public void ShootAction_Shoot(Unit targetUnit)
    {
        _animator.SetTrigger("Shoot");
        Instantiate(_bulletProjectilePrefab, _shootPointTransform.position, Quaternion.identity);
        _bulletProjectilePrefab.GetComponent<BulletProjectile>().TargetUnit(targetUnit);
    }
}
