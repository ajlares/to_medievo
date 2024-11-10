using UnityEngine;

public class Cleric : CharactersBase
{
    protected override void Start()
    {
        base.Start();
        characterName = "Cleric";
        maxHealth = 50;
        health = maxHealth;
        movementRange = 3;
        attackRange = 2;
        attackPower = 20;
        defenseBonus = 0;

        MovementPattern = new CrossPatternM();
        AttackPattern = new CirclePatternCl();
    }

    public override void SpecialAbility()
    {
        throw new System.NotImplementedException();
    }
}