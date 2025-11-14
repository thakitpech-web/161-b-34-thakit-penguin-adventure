using UnityEngine;

public abstract class Enemy : Character
{
    //auto-property
    public int DamageHit { get; protected set; }
    //abstract method for enemy
    public abstract void Behavior();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Intialize(100);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
