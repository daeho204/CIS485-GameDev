using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{   [SerializeField]
    public float enemyHealth;
    public int damage = 20;
    public Player playerHealth;
    public GameObject ItemModel;
    



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }


    public void DeductHealth(int deductHealth)
    {
        enemyHealth -= deductHealth;

        if(enemyHealth <= 0)
        {
            enemyDead();
        }

    }
    public void enemyDead()
    {
        Destroy(gameObject);
        DropItem();
        
    }
    
    void DropItem()
    {
        GameObject item = Instantiate(ItemModel, transform.position, Quaternion.identity);
        Destroy(item,5f);
    }


}
