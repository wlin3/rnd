using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownSystem : MonoBehaviour
{
    public static CooldownSystem instance;

    public CooldownBar cooldownBar1;
    private float basicCooldown;
    private float basicTimer;
    public bool basicCanAttack;

    public CooldownBar cooldownBar2;
    private float chargeCooldown;
    private float chargeTimer;
    public bool chargedCanAttack;

    public CooldownBar cooldownBar3;
    private float secondCooldown;
    private float secondTimer;
    public bool secondCanAttack;

    public CooldownBar cooldownBar4;
    private float thirdCooldown;
    private float thirdTimer;
    public bool thirdCanAttack;
    
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        if(!basicCanAttack)
        {
            basicTimer += Time.deltaTime;
            cooldownBar1.SetCooldown(basicCooldown - basicTimer);
            if(basicTimer > basicCooldown)
            {
                basicCanAttack = true;
                basicTimer = 0;
            }
        }
        if(!chargedCanAttack)
        {
            chargeTimer += Time.deltaTime;
            cooldownBar2.SetCooldown(chargeCooldown - chargeTimer);
            if(chargeTimer > basicCooldown)
            {
                chargedCanAttack = true;
                chargeTimer = 0;
            }
        }
        if(secondCanAttack)
        {
            basicTimer += Time.deltaTime;
            cooldownBar1.SetCooldown(basicCooldown - basicTimer);
            if(basicTimer > basicCooldown)
            {
                basicCanAttack = true;
                basicTimer = 0;
            }
        }
        if(thirdCanAttack)
        {
            basicTimer += Time.deltaTime;
            cooldownBar1.SetCooldown(basicCooldown - basicTimer);
            if(basicTimer > basicCooldown)
            {
                basicCanAttack = true;
                basicTimer = 0;
            }
        }
    }

    public void BasicCooldown(float cooldown)
    {
        if(basicCanAttack)
        {
            basicCanAttack = false;
            basicCooldown = cooldown;
            cooldownBar1.SetMaxCooldown(basicCooldown);
        }
    }

    public void ChargedCooldown(float cooldown)
    {
        if(chargedCanAttack)
        {
            chargedCanAttack = false;
            chargeCooldown = cooldown;
            cooldownBar2.SetMaxCooldown(chargeCooldown);
        }
    }

    public void Cooldown3(float cooldown)
    {
        if(basicCanAttack)
        {
            basicCanAttack = false;
            basicCooldown = cooldown;
            cooldownBar1.SetMaxCooldown(basicCooldown);
        }
    }

    public void Cooldown4(float cooldown)
    {
        if(basicCanAttack)
        {
            basicCanAttack = false;
            basicCooldown = cooldown;
            cooldownBar1.SetMaxCooldown(basicCooldown);
        }
    }
}
