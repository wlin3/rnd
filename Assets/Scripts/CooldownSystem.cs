using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownSystem : MonoBehaviour
{
    public static CooldownSystem instance;

    public bool autoFire;
    public CooldownBar cooldownBar1;
    public CooldownBar floatingCooldownBar1;
    private float basicCooldown;
    private float basicTimer;
    public bool basicCanAttack;

    public CooldownBar cooldownBar2;
    public CooldownBar floatingCooldownBar2;
    private float chargeCooldown;
    private float chargeTimer;
    public bool chargedCanAttack;
    private float storedChargeCooldown;

    public CooldownBar cooldownBar3;
    public CooldownBar floatingCooldownBar3;
    private float secondCooldown;
    private float secondTimer;
    public bool secondCanAttack;

    public CooldownBar cooldownBar4;
    public CooldownBar floatingCooldownBar4;
    private float thirdCooldown;
    private float thirdTimer;
    public bool thirdCanAttack;
    
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if(!basicCanAttack)
        {
            basicTimer += Time.deltaTime;
            cooldownBar1.SetCooldown(basicCooldown - basicTimer);
            if(floatingCooldownBar1 != null)
            {
                floatingCooldownBar1.SetCooldown(basicCooldown - basicTimer);
            }
            if (basicTimer > basicCooldown)
            {
                basicCanAttack = true;
                basicTimer = 0;
            }
        }
        if(!chargedCanAttack)
        {
            chargeTimer += Time.deltaTime;
            cooldownBar2.SetCooldown(chargeCooldown - chargeTimer);
            if(floatingCooldownBar2 != null)
            {
                floatingCooldownBar2.SetCooldown(chargeCooldown - chargeTimer);
            }
            if(chargeTimer > chargeCooldown)
            {
                chargedCanAttack = true;
                chargeTimer = 0;
            }
        }
        if(!secondCanAttack)
        {
            secondTimer += Time.deltaTime;
            cooldownBar3.SetCooldown(secondCooldown - secondTimer);
            if(floatingCooldownBar3 != null)
            {
                floatingCooldownBar3.SetCooldown(secondCooldown - secondTimer);
            }
            if(secondTimer > secondCooldown)
            {
                secondCanAttack = true;
                secondTimer = 0;
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
            if(!autoFire)
            {
                chargeTimer = chargeCooldown;
            }
            basicCanAttack = false;
            basicCooldown = cooldown;
            cooldownBar1.SetMaxCooldown(basicCooldown);
            if(floatingCooldownBar1!= null)
            {
               floatingCooldownBar1.SetMaxCooldown(basicCooldown);
            }

        }
    }

    public void ChargedCooldown(float cooldown)
    {
        if(chargedCanAttack)
        {
            storedChargeCooldown = cooldown;
            chargedCanAttack = false;
            chargeCooldown = cooldown;
            cooldownBar2.SetMaxCooldown(chargeCooldown);
            if(floatingCooldownBar2 != null)
            {
                floatingCooldownBar2.SetMaxCooldown(chargeCooldown);
            }

        }
    }

    public void Cooldown3(float cooldown)
    {
        if(secondCanAttack)
        {
            secondCanAttack = false;
            secondCooldown = cooldown;
            cooldownBar3.SetMaxCooldown(secondCooldown);
            if(floatingCooldownBar3 != null)
            {
                floatingCooldownBar3.SetMaxCooldown(secondCooldown);
            }

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
