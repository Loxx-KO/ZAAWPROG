using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public CaptureManager captureManager;
    public Slider HP_Slider;
    public EnemyController1 EnemyController;

    [Header("Turn Variables")]
    private bool PlayersTurn = false;

    [Header("Buttons")]
    public Button fightButton;
    public Button defendButton;
    public Button healButton;

    [Header("Stats")]
    [SerializeField] private int MaxHP = 100;
    private int currentHP;
    [SerializeField] private int HealTurnCount = 0;
    [SerializeField] private int baseDmg = 10;
    private bool ShieldUp = false;
    public int EnemiesDefeated = 0;

    void Start()
    {
        currentHP = MaxHP;
    }

    public void TakeDmg(int dmg)
    {
        if(ShieldUp)
        {
            currentHP -= dmg / 2;
        }
        else currentHP -= dmg;

        ShieldPutDown();

        if (currentHP <= 0) 
        {
            currentHP = 0;
            Debug.Log("Player died");
        }
        HP_Slider.value = currentHP;
    }

    private void ShieldPutDown()
    {
        ShieldUp = false;
    }

    public void SetPlayerTurn()
    {
        PlayersTurn = true;
        captureManager.ResetGestureRec();
        captureManager.CanTakePhotos = true;
    }

    public void GainPoints()
    {
        EnemiesDefeated++;

        currentHP += 40;
        if (currentHP > MaxHP) currentHP = MaxHP;
        HP_Slider.value = currentHP;
    }

    //Animation events
    public int Attack()
    {
        ShieldPutDown();
        HealTurnCount++;
        return baseDmg + UnityEngine.Random.Range(0,1) * UnityEngine.Random.Range(0,10);
    }

    public void Heal()
    {
        HealTurnCount = 0;
        ShieldPutDown();

        currentHP += 20;
        if(currentHP > MaxHP) currentHP = MaxHP;
        HP_Slider.value = currentHP;
    }

    public void Defend()
    {
        HealTurnCount++;
        ShieldUp = true;
    }

    void Update()
    {
        if (PlayersTurn)
        {
            if(captureManager.palmFound || captureManager.fistFound || (captureManager.pointingLeftFound || captureManager.pointingRightFound))
            {
                captureManager.CanTakePhotos = false;
                EnemyController.SetEnemyTurn();
            }

            if (captureManager.palmFound)
            {
                fightButton.gameObject.GetComponent<Animation>().Play();

            }
            else if (captureManager.fistFound)
            {
                defendButton.gameObject.GetComponent<Animation>().Play();

            }
            else if ((captureManager.pointingLeftFound || captureManager.pointingRightFound) && HealTurnCount == 2)
            {
                healButton.gameObject.GetComponent<Animation>().Play();
            }
        }
    }
}
