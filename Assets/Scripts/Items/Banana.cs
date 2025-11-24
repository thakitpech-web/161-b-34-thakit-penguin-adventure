using UnityEngine;

public class Banana : Item
{
    [SerializeField] private int speedBoost = 2;  
    [SerializeField] private float duration = 3f;

    public override void Use(Player player)
    {
        player.AddTemporarySpeed(speedBoost, duration);
    }
}
