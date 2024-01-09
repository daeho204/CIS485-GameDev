using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    
    public void OnTriggerEnter(Collider other)
    {
       
    
        SceneManager.LoadScene("hole 1-2");
        
    }

}
