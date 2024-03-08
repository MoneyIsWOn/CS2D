using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Human : MonoBehaviour, IDamagable
{
    enum BelongTeam
    {
        T,
        CT
    }

    [SerializeField] bool T;
    [SerializeField] string Team;
    [SerializeField] public int hp; //ü��
    [SerializeField] public int ap; //��ź�� ��ġ
    [SerializeField] protected int money;  //���� �ִ� �� 
    [SerializeField] protected bool isAi;  //�÷��̾ ���������� �ƴ���
    /*
     *[0] : Main Weapon 
     *[1] : Sub Weapon
     *[2] : Knife
     *[3]~[4] : Grenades
     *[5] : C4 bomb
     **/
    private void Awake()
    {
        if(T)
        {
            gameObject.tag = "T";
            Team = "Terrorist";
        }
        else
        {
            gameObject.tag = "CT";
            Team = "Counter_Terrorist";
        }
    }

    private void Update()
    {
        Hpcheck();
    }


    protected void Hpcheck()
    {
        if(hp<=0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Debug.Log($"{gameObject.name} is Dead");
        Destroy(gameObject);
    }

}
