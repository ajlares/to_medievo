using UnityEngine;
public class Builder : CharactersBase
{
    protected override void Start()
    {
        base.Start();
        characterName = "Builder";
        maxHealth = 1;
        health = maxHealth;
        movementRange = 3;
        attackRange = 0;
        attackPower = 0;
        defenseBonus = 0;

        MovementPattern = new CrossPatternM();
        AttackPattern = new CirclePatternCl();
    }

    public override void SpecialAbility()
    {
        throw new System.NotImplementedException();
    }
}