using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using JetBrains.Annotations;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _selectedGameObject, _gridSystemVisual;

    private BaseAction _baseAction;

    // Start is called before the first frame update
    void Start()
    {
        _gridSystemVisual = GameObject.Find("GridSystemVisual");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBaseAction(BaseAction baseAction)
    {
        this._baseAction = baseAction;
        _textMeshPro.text = baseAction.GetActionName().ToUpper();
        _button.onClick.AddListener(() => 
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
            _gridSystemVisual.GetComponent<GridSystemVisual>().UpdateGridVisual();
        });

        /* () => 
         * {
         * 
         * }is same as
         * 
         * private void AnonymousFunction
         * {
         * 
         * }*/ 
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        _selectedGameObject.SetActive(selectedBaseAction == _baseAction);
    }

}
