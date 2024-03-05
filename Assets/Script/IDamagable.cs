using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Human : MonoBehaviour,IDamagable
{
    [SerializeField] public int hp; //ü��
    [SerializeField] protected int ap; //��ź�� ��ġ
    [SerializeField] protected int money;  //���� �ִ� �� 
    [SerializeField] protected bool isAi;  //�÷��̾ ���������� �ƴ���
    /*
     *[0] : Main Weapon 
     *[1] : Sub Weapon
     *[2] : Knife
     *[3]~[4] : Grenades
     *[5] : C4 bomb
     **/
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
        Destroy(gameObject);
    }

}
