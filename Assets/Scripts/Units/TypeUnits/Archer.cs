public class Archer : CharactersBase
{
    protected override void Start()
    {
        base.Start();
        characterName = "Archer";
        maxHealth = 75;
        health = maxHealth;
        movementRange = 2;
        attackRange = 3;
        attackPower = 15;
        defenseBonus = 0;

        MovementPattern = new CrossPatternM();
        AttackPattern = new CirclePatternA();
    }

    public override void SpecialAbility()
    {
        throw new System.NotImplementedException();
    }
}