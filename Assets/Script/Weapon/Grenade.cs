using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade: Weapon
{

    private void OnDrawGizmos() //�׻� ����� ����
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4f);
    }

    //private void OnDrawGizmosSelected() //������Ʈ�� ���� ���϶� ��������.
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, 4f);
    //}
}
