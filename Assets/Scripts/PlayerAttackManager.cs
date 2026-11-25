using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public enum AttackID
    {
        LightOne,
        LightTwo,
        LightThree,
        HeavyOne,
        HeavyTwo,
        HeavyThree,
        Special
    }
    
    [Header("Attack colliders")] 
    public Collider2D defaultCollider;
    public Collider2D lightThreeCollider;
    public Collider2D specialCollider;
    
    [Header("Attack damage")] 
    public int lightOne;
    public int lightTwo, lightThree;
    public int heavyOne, heavyTwo, heavyThree;
    public int special;

    private List<Collider2D> colResults = new();
    
    public void Attack(AttackID id)
    {
        Collider2D dmgCol;
        int dmg;

        switch (id)
        {
            default:
                dmgCol = defaultCollider;
                dmg = lightOne;
                break;
            case AttackID.LightOne:
                dmg = lightOne;
                dmgCol = lightThreeCollider;
                break;
            case AttackID.LightTwo:
                dmg = lightTwo;
                dmgCol = defaultCollider;
                break;
            case AttackID.LightThree:
                dmg = lightThree;
                dmgCol = lightThreeCollider;
                break;
            case AttackID.HeavyOne:
                dmg = heavyOne;
                dmgCol = defaultCollider;
                break;
            case AttackID.HeavyTwo:
                dmg = heavyTwo;
                dmgCol = defaultCollider;
                break;
            case AttackID.HeavyThree:
                dmg = heavyThree;
                dmgCol = defaultCollider;
                break;
            case AttackID.Special:
                dmgCol = specialCollider;
                dmg = special;
                break;
        }

        Physics2D.OverlapCollider(dmgCol, colResults);
        

        foreach (var col in colResults)
        {
            if (col.TryGetComponent(out BossHealth health))
            {
                health.TakeDamage(dmg);
            }
        }
    }
}
