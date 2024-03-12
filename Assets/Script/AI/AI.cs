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
    [SerializeField] FIRETRACKING firetrack; //�Ѿ��� ����
    [SerializeField] Transform target;
    [SerializeField] GameObject astar;
    float angleRange = 90f; // ��������
    float distance = 15f; // ��ä��(�þ�)�� ������ ũ��.

    void Update()
    {
        AiMove();
        //AIeye();

    }
    void AiMove() //Ai�� ������
    {
        AIPath();
    }


    void AIPath()
    {
        List<Node> path = astar.GetComponent<PathFinder>().npcpath;
        Node next = path[1];
        Vector2 dir = (next.worldPosition - transform.position).normalized;
        transform.Translate(dir * movespeed * Time.deltaTime);
    }

    void AIeye() // Ai�� �þ�
    {
        /*Vector3 targetVector = (target.transform.position - gameObject.transform.position); // ���� ������ ����

        if (targetVector.magnitude < distance) //���� ������ �Ÿ��� ���� ũ�� ���� �۴ٸ�
        {
            float Dot = Vector3.Dot(targetVector.normalized, transform.up); //�� ���� ���� �����

            float theta = Mathf.Acos(Dot); //��ũ �ڻ���(���ڻ���)�� ���� ��Ÿ(��) ���ϱ�.

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
                Debug.Log("���� ������");
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
            Debug.Log("ź���� �� ���������ϴ�. �������� ���⸦ ��ų�, �ٸ� ���⸦ ã���ʽÿ�.");
        }

    }
}
