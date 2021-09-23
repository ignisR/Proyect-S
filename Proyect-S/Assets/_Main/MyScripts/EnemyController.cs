﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //---------------TRANSFORM TARGET--------------------------///
    [SerializeField] private Transform target;

    ///-------------------TIMER---------------------------------///
    [SerializeField] private float timerToShootSet;
    [SerializeField] private float timerBetweenShootSet;

    ///----------------ENEMY COMPONENTS-------------------------///
    private LifeController enemy;
    private EnemyAI enemyAI;

    ///---------------------STATS-------------------------------///
    [SerializeField] private float speed;
    ///---------------------****------------------------------///
    private bool shootState;
    private float timerBetweenShoot;
    private float timerToShoot;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemy = GetComponent<LifeController>();
    }
    void Update()
    {
        if (enemyAI.IsInSight(target) && enemy.CurrentHealth > 0)
        {
            if (!shootState)
            {
                //Debug.Log($"IsInSight is {enemyAI.IsInSight(target)} from {gameObject.name}");
                enemy.Anim.SetBool("Walk", true);
                Vector3 dir = target.position - transform.position;
                dir.y = 0;
                transform.rotation = Quaternion.LookRotation(dir);
                transform.position += transform.forward * speed * Time.deltaTime;
            }

            #region timer
            if (timerBetweenShoot > 0)
            {
                timerBetweenShoot -= Time.deltaTime;
            }
            else
            {

                ///animacion
                if (!shootState)
                {
                    enemy.Anim.SetTrigger("Shoot");
                    timerToShoot = timerToShootSet;
                    shootState = true;
                }
                if (timerToShoot <= 0)
                {
                    //enemyAI.Shoot();
                    timerBetweenShoot = timerBetweenShootSet;
                    shootState = false;
                }
            }
            #endregion
        }
        else if(!enemyAI.IsInSight(target))
        {
            enemy.Anim.SetBool("Walk", false);
        }
        if (timerToShoot > 0)
        {
            timerToShoot -= Time.deltaTime;
        }
    }
}
