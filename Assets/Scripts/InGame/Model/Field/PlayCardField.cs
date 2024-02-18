using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardField : MonoBehaviour
{
    [SerializeField] GameObject _playerField;
    [SerializeField] GameObject _enemyField;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ActiveField(PlayCardFieldType playCardFieldType) 
    {
        if (playCardFieldType == PlayCardFieldType.EnemyField)
        {
            _enemyField.SetActive(true);
        }
        else if (playCardFieldType == PlayCardFieldType.PlayerField) 
        {
            _playerField.SetActive(true);
        }
    }

    public void NonActive() 
    {
        _playerField.SetActive(false);
        _enemyField.SetActive(false);
    }
}
