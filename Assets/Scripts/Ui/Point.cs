using UnityEngine;
using UnityEngine.UI;

public class Point : Apple
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointText.text = currntPoint.ToString();
    }
}
