using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _actionPointsText;
    [SerializeField] private Unit _unit;
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private HealthSystem _healthSystem;
    // Start is called before the first frame update
    void Start()
    {
        int actionPoints = _unit.GetComponent<Unit>().GetActionPoints();
        UpdateActionPointsText(actionPoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateActionPointsText(int actionPoints)
    {
        _actionPointsText.text = actionPoints.ToString();
    }

    public void UpdateHealthBar()
    {
        _healthBarImage.fillAmount = _healthSystem.GetHealthNormalized();
    }
}
