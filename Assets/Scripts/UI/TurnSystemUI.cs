using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] TMP_Text _turnNumberText;
    [SerializeField] private GameObject _enemyActionVisualImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTurnNumberText(int turnNumber)
    {
        _turnNumberText.text = "Turn " + turnNumber.ToString();
    }

    public void EnemyActionImageEnable()
    {
        _enemyActionVisualImage.SetActive(true);
    }

    public void EnemyActionImageDisable()
    {
        _enemyActionVisualImage.SetActive(false);
    }
}
