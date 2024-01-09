using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    //Activated status
    public static bool isActivate = false;
    // Start is called before the first frame update
    [SerializeField]
    private Hand currentHand;

    private bool isAttack = false;
    private bool isSwing = false;

    private RaycastHit hitinfo;

    

    // Update is called once per frame
    void Update()
    {
        if (isActivate)
        {
            TryAttack();
        }
        
    }

    private void TryAttack()
    {
        if(Input.GetButton("Fire1"))
        {
            if(!isAttack)
            {
                StartCoroutine(AttackCoroutine());

            }
        }
    }
    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentHand.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentHand.attackDelayA);
        isSwing = true;

        StartCoroutine(HitCoroutine());



        yield return new WaitForSeconds(currentHand.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentHand.attackDelay - currentHand.attackDelayA - currentHand.attackDelayB);
        isAttack = false;
    }

    IEnumerator HitCoroutine()
    {
        while(isSwing)
        {
            if(CheckObject())
            {
                isSwing = false;
                Debug.Log(hitinfo.transform.name);
            }
            yield return null;
           
        }
    }
    private bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitinfo, currentHand.range))
        {
            return true;
        }
        return false;
    }

    public void HandChange(Hand _hand)
    {
        if (WeaponManager.currentWeapon != null)
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }
        currentHand = _hand;
        WeaponManager.currentWeapon = currentHand.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentHand.anim;

        currentHand.transform.localPosition = Vector3.zero;
        currentHand.gameObject.SetActive(true);

        isActivate = true;
    }

}
