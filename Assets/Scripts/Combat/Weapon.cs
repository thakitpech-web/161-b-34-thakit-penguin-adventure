using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float cooldown = 0.25f;

    protected GameObject owner;
    protected float lastShotTime;

    public float Cooldown => cooldown;

    public bool TryFire(GameObject owner, Vector2 dirNormalized)
    {
        if (Time.time < lastShotTime + cooldown) return false;
        lastShotTime = Time.time;
        this.owner = owner;
        return FireInternal(dirNormalized.normalized);
    }

    protected abstract bool FireInternal(Vector2 dirNormalized);
}
