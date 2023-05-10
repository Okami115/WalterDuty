using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int healt;
    [SerializeField] private int damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healt < 0) 
        { 
            Destroy(gameObject);
        }
    }

    public int GetHealt()
    {
        return healt;
    }

    public void GetDamage(int damage)
    {
        healt -= damage;
    }
}