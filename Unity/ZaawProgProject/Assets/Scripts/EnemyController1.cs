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
    public Animator Animator;

    [Header("Turn variables")]
    private bool EnemyTurn = false;

    [Header("Stats")]
    [SerializeField] public int MaxHP = 50;
    public int currentHP;
    [SerializeField] private int baseDmg = 5;
    private bool ShieldUp = false;

    void Awake()
    {
        MaxHP = 50;
        currentHP = MaxHP;
    }

    public void SetAniumationToIdle()
    {
        Animator.Play("Idle");
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
            Animator.Play("Death");
            currentHP = 0;
            Debug.Log("Enemy died");
            FindObjectOfType<PlayerController>().GainPoints();
            SetUpNewEnemy();
        }
    }

    private void SetUpNewEnemy()
    {
        Animator.Play("Recover");
        MaxHP = UnityEngine.Random.Range(40, 70);
        baseDmg = UnityEngine.Random.Range(4, 10);
        currentHP = MaxHP;
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
        Animator.Play("Attack");
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
            FindObjectOfType<PlayerController>().SetPlayerTurn();
        }
    }
}
