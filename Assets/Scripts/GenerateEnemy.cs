using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public GameObject theEnemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());
        
        
    }

    IEnumerator EnemyDrop()
    {
        while(enemyCount < 20)
        {
            xPos = Random.Range(1, 30);
            float yPos = 0.5f;
            zPos = Random.Range(1, 30);

            Instantiate(theEnemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            enemyCount += 1;
        }
    }
    
    
}
