using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatePatten : MonoBehaviour
{
    [SerializeField] Transform missionPlace;
    [SerializeField] PathFinding pathfinder;
    enum State //Ai�� ���� ����
    {
        shopping,
        mission,
        alert,
        attack,
        die,
    }
    State curstate;

    private void Update()
    {
       
    }
    void Shopping() //�������� ���⸦ ������.
    {
       //���� ���Ⱑ ��� �ͺ��� ����, ���� ������ �� �������� ��� ���� ����.
    }
    void battle() //���� ����.
    {
        /* 0.��� ������ ���߰�, ������ ���� �����ϰ� �����.
         * 1.���̵��� ���� ���� ���� ���� �ӵ��� �ٸ��� ����.
         * 2.�� ���¿��� ���� ���� �þ߿��� ����� ��� ���·� ��ȯ.
         */

    }
    void Mission()
    {
        //�ӹ� ����.�̸� �׸� ��ź ��ġ�Ϸ� ���� ��
        /*0. ���� ��ġ���� �ӹ������� �Ÿ� ��� Ž��.
        *1. �ӹ� ���� ���� ������ �����ϸ� ���� ���·� ��ȯ  
        */
        pathfinder.FindPath(transform.position, missionPlace.position);
    }
    void alert() //��� ����.
    {
        /* ���� �־��� ������ ��ġ�� ����ϰ� ���� �ð� ���� �� ���� �ֽ���.
         * �� �ð����� ���� �ٽ� �þ߿� ���´ٸ� ���� ���·� ��ȯ
         * ���� �ð� ���� ���� �þ߿� ������ �ʴ´ٸ�, �ٽ� �ӹ� ���·� ��ȯ. 
         */

    }
    void Die() //��� ����
    {
        //���� ��� �ִ� ����� ����ź, ��ź�� ����߸���, ��Ȱ��ȭ��.
    }
}
