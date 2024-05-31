

public interface IAttack
{
    bool IsAttacking { get; set; }
    void Attack();
    void StopAttack();
}
