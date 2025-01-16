using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private Unit _unit;
    [SerializeField] private int _health = 100, _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }
    // Start is called before the first frame update
    void Start()
    {
        _unit = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damageAmount)
    {
        _health -= damageAmount;

        if(_health < 0)
        {
            _health = 0;
        }

        if (_health == 0)
        {
            StartCoroutine(Die());
            //Die();
        }

        Debug.Log(_health);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.3f);
        _unit.Die();
        StopAllCoroutines();
    }

    public float GetHealthNormalized()
    {
        return (float)_health / _maxHealth;
    }
}
