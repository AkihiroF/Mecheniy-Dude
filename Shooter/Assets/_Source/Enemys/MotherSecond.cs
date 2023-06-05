using _Source.HealthSystem;
using _Source.Services;
using _Source.SignalsEvents.UpgradesEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSecond : ABaseHealth
{
    [SerializeField] private int countScore;

    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] private int amountEnemy;
    public override void GetDamage(float damage)
    {
        if (CurrentHp - damage <= 0)
        {
            Dead();
        }
        else
        {
            CurrentHp -= damage;
        }
    }

    private void Dead()
    {

        Signals.Get<OnAddScoreUpgrade>().Dispatch(countScore);
        for (int i = 0; i < amountEnemy; i++)
        {
            Instantiate(_enemyPrefab, transform.position, new Quaternion(0, 0, 0, 0));
        }
        Destroy(this.gameObject);
    }

    public override void ReturnHealth(float health)
    {
        throw new System.NotImplementedException();
    }

}
