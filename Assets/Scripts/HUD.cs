using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GunController theGunController;
    private Gun currentGun;

    //Able and disable BulletHUD
    [SerializeField]
    private GameObject go_BulletHUD;

    //bullet count
    [SerializeField]
    private TextMeshProUGUI[] text_Bullet;   
    
    
        

    // Update is called once per frame
    void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        currentGun = theGunController.GetGun();
        text_Bullet[0].text = currentGun.carryBulletCount.ToString();
        text_Bullet[1].text = currentGun.reloadBulletCount.ToString();
        text_Bullet[2].text = currentGun.currentBulletCount.ToString();
    }
}
