using Monster.Skill;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbitBall : MonoBehaviour
{
    [SerializeField] private float damageCooldown = 0.5f;
    [SerializeField] private int damage = 10;

    private bool canDamage = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canDamage || (((1<<collision.gameObject.layer) & (1<<7))==0 ))
        {
            return;
        }

        MonsterController monsterControll = collision.GetComponent<MonsterController>();
        if (monsterControll != null) 
        { 
        
        monsterControll.TakeDamage(damage);
        }

        StartCoroutine(DamageCooldown());
    }

    private IEnumerator DamageCooldown()
    { 
    canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;   
        
    }
}
