using UnityEditor;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [field: SerializeField] protected int ItemValue { get; set; }

    public void PickUp(Player player)
    {
        Use(player);
        Destroy(this.gameObject);
    }

    public abstract void Use(Player player);
}
