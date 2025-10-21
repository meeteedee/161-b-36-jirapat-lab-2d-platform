using UnityEngine;

public class Croccodile : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Behavior()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        base.Initialize(100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
