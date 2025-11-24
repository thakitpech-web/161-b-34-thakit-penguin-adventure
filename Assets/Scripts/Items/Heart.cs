using UnityEngine;

public class Heart : Item
{
    public override void Use(Player player)
    {
        if (player)
        {
            player.Heal(ItemValue);
        }
    }
}
