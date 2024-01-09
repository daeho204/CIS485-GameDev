using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatusController : MonoBehaviour
{
    [SerializeField]
    public int hp;
    public int currentHp;

    [SerializeField]
    public int sp;
    public int currentSp;

    [SerializeField]
    private int spIncreaseSpeed;

    [SerializeField]
    private int spRechargeTime;
    private int currentSpRechargeTime;

    private bool spUsed;

    [SerializeField]
    private Image[] images_Guage;
    private const int HP = 0, SP = 1;

    public Slider slider, slider1;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = hp;
        currentSp = sp;

    }

    // Update is called once per frame
    void Update()
    {
        GaugeUpdate();
        SPRechargeTime();
        SPRecover();

    if ((int)currentHp == 0)
        {
            SceneManager.LoadScene("Ending");
        }   
    }

    public void maxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void setHealth(int health)
    {
        slider.value = health;
    }


    public void maxStemina(int stemina)
    {
        slider1.maxValue = stemina;
        slider1.value = stemina;
    }
    public void setStemina(int stemina)
    {
        slider1.value = stemina;
    }


    private void GaugeUpdate()
    {
        images_Guage[HP].fillAmount = (float)currentHp / hp;
        images_Guage[SP].fillAmount = (float)currentSp / sp;


    }

    private void SPRechargeTime()
    {
        if (spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
            {
                currentSpRechargeTime++;
            }
            else
            {
                spUsed = false;
            }
        }
    }

    private void SPRecover()
    {
        if (!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed;
        }
    }

    public void DecreaseStamina(int _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - _count > 0)
        {
            currentSp -= _count;
        }
        else
        {
            currentSp = 0;
        }
    }

    public int GetCurrentSP()
    {
        return currentSp;
    }

    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < hp)
        {
            currentHp += _count;
        }
        else currentHp = hp;
    }
    public void DecreaseHP(int _count)
    {
        currentHp -= _count;
        if (currentHp <= 0)
        {
            Debug.Log("Your Character Died");
        }
    }
}

   
