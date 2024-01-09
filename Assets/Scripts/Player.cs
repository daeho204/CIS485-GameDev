using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StatusController statController;
    public EnemyHealth enemy;
    // Start is called before the first frame update
    void Start()
    {
        statController.currentHp = statController.hp;
        statController.currentSp = statController.sp;
        statController.maxHealth(statController.hp);        
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage(enemy.damage);
    }

    public void TakeDamage(int damage)
    {
        statController.currentHp -= damage;
        statController.setHealth(statController.currentHp);
    }
    public void GainHealth(int gainhp)
    {
        if (statController.currentHp < 100)
        {
            statController.currentHp += gainhp;
            statController.setHealth(statController.currentHp);
        }
    }
    public void GainStemina(int gainsp)
    {
        if (statController.currentSp < 1000)
        {
            statController.currentSp += gainsp;
            statController.setStemina(statController.currentSp);
        }
    }
   
}
