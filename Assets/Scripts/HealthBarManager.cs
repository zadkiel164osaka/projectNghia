using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public Neko neko;
    // Start is called before the first frame update
    public Image healthBar;
    public float healthAmount;
    public Image staminaBar;
    public float staminaAmount;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        healthAmount = neko.hp;
        staminaAmount = neko.stamina;
        healthBar.fillAmount= healthAmount/100f;
        staminaBar.fillAmount = staminaAmount / 100f;
    }
}
