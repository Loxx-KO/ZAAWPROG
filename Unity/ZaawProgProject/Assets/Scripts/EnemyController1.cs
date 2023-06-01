using Emgu.CV.Reg;
using Emgu.CV.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum EnemyActions
{
    Attack = 0, Defend
}

public class EnemyController1 : MonoBehaviour
{
    [Header("Components")]
    public Slider HP_Slider;
    public PlayerController PlayerController;

    [Header("Turn variables")]
    private bool EnemyTurn = false;

    [Header("Stats")]
    [SerializeField] private int MaxHP = 50;
    private int currentHP;
    [SerializeField] private int baseDmg = 5;
    private bool ShieldUp = false;

    void Start()
    {
        currentHP = MaxHP;
    }

    public void TakeDmg(int dmg)
    {
        if (ShieldUp)
        {
            currentHP -= dmg / 2;
        }
        else currentHP -= dmg;

        ShieldPutDown();

        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("Enemy died");
            PlayerController.GainPoints();
            SetUpNewEnemy();
        }
        HP_Slider.value = currentHP;
    }

    private void SetUpNewEnemy()
    {
        MaxHP = UnityEngine.Random.Range(40, 70);
        baseDmg = UnityEngine.Random.Range(4, 10);
        currentHP = MaxHP;
        HP_Slider.value = currentHP;
    }

    private void ShieldPutDown()
    {
        ShieldUp = false;
    }

    public void SetEnemyTurn()
    {
        EnemyTurn = true;
    }

    //Animation events
    public int Attack()
    {
        ShieldPutDown();
        return baseDmg + UnityEngine.Random.Range(0, 1) * UnityEngine.Random.Range(0, 10);
    }

    public void Defend()
    {
        ShieldUp = true;
    }

    private void Update()
    {
        if(EnemyTurn)
        {
            switch ((EnemyActions)UnityEngine.Random.Range(0,1))
            {
                case EnemyActions.Attack: Attack(); break;
                case EnemyActions.Defend: Defend(); break;
                default: break;
            }
            EnemyTurn = false;
            PlayerController.SetPlayerTurn();
        }
    }
}
