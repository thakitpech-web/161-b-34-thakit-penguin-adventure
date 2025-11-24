using UnityEngine;

public class Apple : Item
{
    public override void Use(Player player)
    {
        if(player)
        {
            player.addPoint(ItemValue);
        }
    }
}
