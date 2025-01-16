using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagDollSpawner : MonoBehaviour
{
    [SerializeField] private Transform _ragDollPrefab;

    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
