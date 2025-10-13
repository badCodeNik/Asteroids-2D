namespace _Project.Scripts.Health
{
    public interface IDamageable
    {
        void TakeDamage(bool destroyImmediately = false);
    }
}