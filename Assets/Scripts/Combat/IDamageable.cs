
public interface IDamageable
{
    void TakeDamage(int amount);
    void TakeDamage(int amount, UnityEngine.Vector2 knockback); // overload
}
