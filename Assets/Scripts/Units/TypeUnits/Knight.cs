using UnityEngine;

public class Knight : CharactersBase
{
    protected override void Start()
    {
        base.Start();
        characterName = "Kight";
        maxHealth = 100;
        health = maxHealth;
        movementRange = 3;
        attackRange = 2;
        attackPower = 15;
        defenseBonus = 5;

        MovementPattern = new CrossPatternM();
        AttackPattern = new CrossPatternA();
    }

    public override void SpecialAbility()
    {
        throw new System.NotImplementedException();
    }
}