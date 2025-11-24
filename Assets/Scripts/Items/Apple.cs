using UnityEngine;
using UnityEngine.UI;
public class Apple : Item
{
    public int currntPoint;
    public Text pointText;
    public override void Use(Player player)
    {
        if(player)
        {
            player.addPoint(ItemValue);
        }
    }
    void Update()
    {
        pointText.text = currntPoint.ToString();
    }
}
