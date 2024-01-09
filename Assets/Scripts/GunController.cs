using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //Activated status
    public static bool isActivate = true;
    //current gun
    [SerializeField]
    private Gun currentGun;

    //calculate firerate
    private float currentFireRate;

    //variables
    private bool isReload = false;
    [HideInInspector]
    private bool isFineSightMode = false;

    //original position
    [SerializeField]
    private Vector3 originPos;

    //audio/sound
    private AudioSource audioSource;

    //hit info
    private RaycastHit hitInfo;

    //Components
    private Crosshair theCrosshair;

    [SerializeField]
    private Camera theCam;

    [SerializeField]
    private GameObject hit_effect_prefab;

    //GolfBall Components
    public Transform attackPoint;
    public GameObject bullet;
    public float spread;
    public float shootForce, upwardForce;

    void Start()
    {
        theCam = Camera.main;
        originPos = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
        theCrosshair = FindObjectOfType<Crosshair>();
        //originPos = transform.localPosition;

        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;
    }

    void Update()
    {
        if (isActivate)
        {
            GunFireRateCalc();
            TryFire();
            TryReload();
            TryFineSight();
        }
        
    }
    //Calculate Firerate
    private void GunFireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    //Trying fire
    private void TryFire()
    {
        if(Input.GetButton("Fire1") && currentFireRate <=0 && !isReload)
        {
            Fire();
        }
    }
    //calculate before fire
    private void Fire()
    {
        if (!isReload)
        {

            if (currentGun.currentBulletCount > 0)
            {
                Shoot1();
            }
            else
            {
                CancelFineSight();
                StartCoroutine(ReloadCoroutine());
            }
        }
    }

    //calculate after fire
    /*private void Shoot()
    {
        theCrosshair.FireAnimation();
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate;
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();
        Hit();
        

        

        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());
    }*/
    private void Shoot1()
    {
        isReload = false;
        //Find the exact hit position using a raycast
        Ray ray = theCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
   

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hitInfo))
        {
            targetPoint = hitInfo.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); //Just a point far away from the player
        }
        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(theCam.transform.up * upwardForce, ForceMode.Impulse);

        theCrosshair.FireAnimation();
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate;
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();
        Hit();




        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());

    }

    private void Hit()
    {
        if(Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hitInfo, currentGun.range))
        {
            GameObject clone = Instantiate(hit_effect_prefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(clone, 2f);

        }
        if(Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hitInfo, currentGun.range))
        {
            if(hitInfo.transform.tag == "Enemy")
            {
                Debug.Log("Enemy Hit");

                EnemyHealth enemyHealthScript = hitInfo.transform.GetComponent<EnemyHealth>();
                enemyHealthScript.DeductHealth(currentGun.damage);

            }
            else
            {
                Debug.Log("Not enemy");
            }
                
        }
    }

    //reload trial
    private void TryReload()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            CancelFineSight();
            StartCoroutine(ReloadCoroutine());
        }
    }

    public void CancelReload()
    {
        if(isReload)
        {
            StopAllCoroutines();
            isReload = false;
        }
    }

    //Reload
    IEnumerator ReloadCoroutine()
    {
        if(currentGun.carryBulletCount > 0)
        {
            isReload = true;
            currentGun.anim.SetTrigger("Reload");

            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGun.reloadTime);
            if(currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }
            isReload = false;
        }
        else
        {
            Debug.Log("No Bullet Left");
        }
    }

    //trying fine sight
    private void TryFineSight()
    {
        if(Input.GetButtonDown("Fire2") && !isReload)
        {
            FineSight();
        }

    }

    //calceling fine sight
    public void CancelFineSight()
    {
        if (isFineSightMode)
        {
            FineSight();
        }
        
    }

    //Fine sight Logic/animation
    private void FineSight()
    {
        isFineSightMode = !isFineSightMode;
        currentGun.anim.SetBool("FineSightMode", isFineSightMode);

        if (isFineSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(FineSightActivateCoroutine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FineSightDeactivateCoroutine());

        }
    }

    //Activating Fine Sight
    IEnumerator FineSightActivateCoroutine()
    {
        while(currentGun.transform.localPosition != currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f);
            yield return null;
        }
    }

    //Deactivating Fine Sight
    IEnumerator FineSightDeactivateCoroutine()
    {
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }

    //Gun recoiling/action
    IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(currentGun.retroActionForce, originPos.y, originPos.z);
        Vector3 retroActionRecoilBack = new Vector3(currentGun.retroActionFineSightForce, currentGun.fineSightOriginPos.y, currentGun.fineSightOriginPos.z);

        if(!isFineSightMode)
        {
            currentGun.transform.localPosition = originPos;

            //recoil start
            while (currentGun.transform.localPosition.x <= currentGun.retroActionForce -0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;

            }
            //return position
            while(currentGun.transform.localPosition != originPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
                yield return null;
            }
        }
        //recoil in fine sight mode
        else
        {
            currentGun.transform.localPosition = currentGun.fineSightOriginPos;

            while (currentGun.transform.localPosition.x <= currentGun.retroActionFineSightForce - 0.02f)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;

            }
            while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
            {
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.1f);
                yield return null;
            }
        }
    }

    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }

    public Gun GetGun()
    {
        return currentGun;
    }

    public void GunChange(Gun _gun)
    {
        if(WeaponManager.currentWeapon != null)
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }
        currentGun = _gun;
        WeaponManager.currentWeapon = currentGun.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentGun.anim;

        currentGun.transform.localPosition = Vector3.zero;
        currentGun.gameObject.SetActive(true);
        isActivate = true;
    }
    
}
