using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public enum TargettingType { First, Strong, Weak, Last}


    GlobalData WorldAccessData;

    public LayerMask EnemyMask;
    public float Range = 25f;
    public GameObject Target;

    public float AttackSpeed = 1f;
    public float Damage = 5f;
    float Delay;

    private void Start()
    {

        WorldAccessData = FindObjectOfType<GlobalData>();
        Delay = AttackSpeed;
    }

    private void Update()
    {
        if (Target)
        {
            Vector3 LookAtRot = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);

            transform.LookAt(LookAtRot);
            Attack();
        }
        else
        {
            if (EnemyInRange())
            {
                LookForEnemies();
            }
        }
    }

    private void Attack()
    {
        if(Delay > 0f)
        {
            Delay -= Time.deltaTime;
        }
        else if(Delay <= 0f)
        {
            Target.GetComponent<EnemyScript>().TakeDamage(Damage);
            Delay = AttackSpeed;
        }
    }

    private void LookForEnemies()
    {
        float ClosestDistance = Mathf.Infinity;
        GameObject ClosestEnemy = null;
        // First Targetting
        foreach (GameObject Enemy in WorldAccessData.EnemiesInScene)
        {
            if (Enemy)
            {
                if (Vector3.Distance(transform.position, Enemy.transform.position) <= Range)
                {
                    EnemyScript EnemySC = Enemy.GetComponent<EnemyScript>();

                    if (EnemySC.TrueDistance < ClosestDistance)
                    {
                        ClosestDistance = EnemySC.TrueDistance;
                        ClosestEnemy = Enemy;
                    }
                }
            }
            else
            {
                continue;
            }
        }

        if (ClosestEnemy && Vector3.Distance(transform.position, ClosestEnemy.transform.position) <= Range)
        {
            Target = ClosestEnemy;
        }

    }

    private bool EnemyInRange()
    {
        return Physics.CheckSphere(transform.position, Range, EnemyMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
