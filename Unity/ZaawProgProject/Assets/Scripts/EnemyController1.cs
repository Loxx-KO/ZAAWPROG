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

    [Header("Stats")]
    [SerializeField] public int MaxHP = 50;
    public int currentHP;
    [SerializeField] private int baseDmg = 10;
    private bool ShieldUp = false;

    [Header("Particles")]
    public ParticleSystem DefendParticles;
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
        Animator.Play("Hurt");
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
            FindObjectOfType<PlayerController>().SetPlayerTurn();
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
        StopDefendParticles();
    }

    public int Attack()
    {
        Animator.Play("Attack");
        ShieldPutDown();
        return baseDmg + UnityEngine.Random.Range(0, 1) * UnityEngine.Random.Range(1, 10);
    }

    public void Defend()
    {
        ShieldUp = true;
        PlayDefendParticles();
    }

    //Animation events
    public void PlayDefendParticles()
    {
        DefendParticles.Play();
    }

    public void StopDefendParticles()
    {
        DefendParticles.Stop();
    }

    private void Update()
    {
        if(!GameVariables.PlayerTurn && !GameVariables.GameEnded)
        {
            switch ((EnemyActions)UnityEngine.Random.Range(0,2))
            {
                case EnemyActions.Attack: FindObjectOfType<PlayerController>().TakeDmg(Attack()); break;
                case EnemyActions.Defend: Defend(); break;
                default: break;
            }
            FindObjectOfType<PlayerController>().SetPlayerTurn();
        }
    }
}
