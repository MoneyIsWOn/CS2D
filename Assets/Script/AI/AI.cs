using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;
using System.IO;

public class AI : Human
{
    [SerializeField] float movespeed;
    Vector2 moveDir;
    public static Vector2 mouse;
    [SerializeField] WeaponManager weaponmanager;
    [SerializeField] FIRETRACKING firetrack; //총알의 궤적
    [SerializeField] Transform target;
    [SerializeField] GameObject astar;
    float angleRange = 90f; // 각도범위
    float distance = 15f; // 부채꼴(시야)의 반지름 크기.

    void Update()
    {
        AiMove();
        Hpcheck();
        AIeye();

    }
    void AiMove() //Ai의 움직임
    {
        AIPath();
    }


    void AIPath()
    {
        if (astar.GetComponent<PathFinding>().npcpath != null)
        {
            if (astar.GetComponent<PathFinding>().npcpath.Count > 0)
            {
                List<Node> path = astar.GetComponent<PathFinding>().npcpath;
                Node next = path[0];
                Vector2 dir = (next.worldPosition - transform.position).normalized;
                transform.Translate(dir * movespeed * Time.deltaTime);
            }
        }
    }

    void AIeye() // Ai의 시야
    {
        /*Vector3 targetVector = (target.transform.position - gameObject.transform.position); // 나와 적과의 벡터

        if (targetVector.magnitude < distance) //나와 적과의 거리가 지름 크기 보다 작다면
        {
            float Dot = Vector3.Dot(targetVector.normalized, transform.up); //두 백터 내적 결과를

            float theta = Mathf.Acos(Dot); //아크 코사인(역코사인)을 통해 세타(각) 구하기.

            float degree = Mathf.Rad2Deg * theta;

            if(degree <= angleRange)
            {
                float Dot_ = Vector3.Angle(targetVector, transform.up);

                //gameObject.transform.rotation = Quaternion.AngleAxis(Dot_, Vector3.forward);
                transform.up = targetVector.normalized;
            }
        }*/

        Vector2 targetVector = (target.transform.position - gameObject.transform.position);

        if (targetVector.sqrMagnitude < distance * distance)
        {
            float angle = Vector2.Angle(targetVector.normalized, transform.up);

            if (angle < angleRange)
            {
                transform.up = targetVector.normalized;
                //yield return new WaitForSeconds(3);
                Fire(weaponmanager.curweapon);

            }
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = new Color(2f, 0f, 0f,0.1f);

        Handles.DrawSolidArc(transform.position, transform.forward, transform.up, angleRange / 2, distance); 
        
        Handles.DrawSolidArc(transform.position, transform.forward, transform.up, -angleRange / 2, distance);
    }



    public void Fire(Weapon curweapon)
    {
        if (curweapon.magazine > 0)
        {
            firetrack.gameObject.SetActive(true);

            Vector3 fireDir = transform.up;

            Debug.DrawRay(transform.position, fireDir * float.MaxValue, Color.red, 1f);

            curweapon.magazine--;
        }

    }

    public void Reload(Weapon curweapon)
    {
        if (curweapon.maxammo > curweapon.magazineCapacity)
        {
            curweapon.maxammo -= curweapon.magazineCapacity - curweapon.magazine;
            curweapon.magazine = curweapon.magazineCapacity;
        }
        else if (curweapon.maxammo <= curweapon.magazineCapacity && (curweapon.maxammo != 0))
        {
            curweapon.magazine = curweapon.maxammo;
            curweapon.maxammo = 0;
        }
        else
        {
            Debug.Log("탄약이 다 떨어졌습니다. 상점에서 무기를 사거나, 다른 무기를 찾으십시오.");
        }

    }
}
