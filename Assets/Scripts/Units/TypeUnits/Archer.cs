public class Archer : CharactersBase
{
    protected override void Start()
    {
        base.Start();
        characterName = "Archer";
        maxHealth = 75;
        movementRange = 2;
        attackRange = 2;
        attackPower = 15;
        defenseBonus = 0;

        MovementPattern = new CrossPatternM();
        //AttackPattern = new CirclePattern();
    }

    public override void SpecialAbility()
    {
        throw new System.NotImplementedException();
    }
}