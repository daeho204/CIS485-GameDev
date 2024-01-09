using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStemina : MonoBehaviour
{
public int stemina;

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Player")
        {
            Destroy(gameObject);
            other.transform.GetComponent<Player>().GainStemina(stemina);
        }
   
        
    }
}
