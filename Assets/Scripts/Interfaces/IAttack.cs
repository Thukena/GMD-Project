

namespace Interfaces
{
    public interface IAttack
    {
        bool IsAttacking { get; set; }
        float Damage { get; set; }
        float AttackCooldown { get; set; }
        float AttackDuration { get; set; }
        void Attack();
        void StopAttack();
    }
}
