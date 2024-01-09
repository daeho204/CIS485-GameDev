using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : MonoBehaviour
{
public int healing;

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Player")
        {
            Destroy(gameObject);
            other.transform.GetComponent<Player>().GainHealth(healing);
        }
   
        
    }
}
