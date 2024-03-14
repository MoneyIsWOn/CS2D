using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class AIStatePatten : MonoBehaviour
{
    [SerializeField] Transform missionPlace;
    [SerializeField] PathFinding pathfinder;
    [SerializeField] GameObject Enemy;
    List<Node> MissionTrack;
    int nextNode;
    float alertTime = 3f;
    

    float angleRange = 90f; // ��������
    float distance = 15f; // ��ä��(�þ�)�� ������ ũ��.
    enum State //Ai�� ���� ����
    {
        shopping,
        mission,
        alert,
        battle,
        die,
    }
    State curstate;
    bool IspathFind = false;

    private void Start()
    {
        //curstate = State.shopping;
        curstate = State.mission;
        nextNode = 0;
    }
    private void Update()
    {
        switch (curstate)
        {
            case State.shopping:
                Shopping();
                break;
            case State.mission:

                Mission();
                break;
            case State.alert:

                Alert();
                break;
            case State.battle:
                Battle();
                break;
            case State.die:
                Die();
                break;
        }

    }
    void ChangeState(State state) //���� �ٲٱ�
    {
        this.curstate = state;
    }

    void Shopping() //�������� ���⸦ ������.
    {
       //���� ���Ⱑ ��� �ͺ��� ����, ���� ������ �� �������� ��� ���� ����.
    }
    void Battle() //���� ����.
    {
        /* 0.��� ������ ���߰�, ������ ���� �����ϰ� �����.
         * 1.���̵��� ���� ���� ���� ���� �ӵ��� �ٸ��� ����.
         * 2.�� ���¿��� ���� ���� �þ߿��� ����� ��� ���·� ��ȯ.
         */
        gameObject.GetComponent<AI>().movespeed = 0;

        Vector2 targetVector = (Enemy.transform.position - gameObject.transform.position);
        transform.up = (targetVector).normalized;

        //Al.fire();
        if(targetVector.sqrMagnitude>distance*distance)
        {
            ChangeState(State.alert);

        }
    }
    void Mission()
    {
        //�ӹ� ����.�̸� �׸� ��ź ��ġ�Ϸ� ���� ��
        /*0. ���� ��ġ���� �ӹ������� �Ÿ� ��� Ž��.
        *1. �ӹ� ���� ���� ������ �����ϸ� ���� ���·� ��ȯ
        *2. ������ ���� �� �ӹ� ���� �Ǵ� �� �ڸ����� ��� ��� ����.
        */
        gameObject.GetComponent<AI>().movespeed = 8f;

        if (!IspathFind) //��� Ž���� �ѹ��� �����ϰ� ��.
        {
            pathfinder.FindPath(transform.position, missionPlace.position);
            nextNode = 0;
            Debug.Log("��� Ž�� �Ϸ�");
            IspathFind = true;
        }
        else
        {
            AIPath();

            Vector2 targetVector = (Enemy.transform.position - gameObject.transform.position); //1
            if (targetVector.sqrMagnitude < distance * distance)
            {
                float angle = Vector2.Angle(targetVector.normalized, transform.up);

                if (angle < angleRange)
                {
                    ChangeState(State.battle);
                }
            }
        }
    }
    void Alert() //��� ����.
    {
        /* ���� �־��� ������ ��ġ�� ����ϰ� ���� �ð� ���� �� ���� �ֽ���.
         * �� �ð����� ���� �ٽ� �þ߿� ���´ٸ� ���� ���·� ��ȯ
         * ���� �ð� ���� ���� �þ߿� ������ �ʴ´ٸ�, �ٽ� �ӹ� ���·� ��ȯ. 
         */
        gameObject.GetComponent<AI>().movespeed = 2f;

        Vector2 targetVector = (Enemy.transform.position - gameObject.transform.position);
        Coroutine alertcoroutine = StartCoroutine(AlertTime());
        if (targetVector.sqrMagnitude < distance * distance) //���� �ٽ� �þ߿� ������ ��
        {
            StopCoroutine(alertcoroutine);
            ChangeState(State.battle);
        }
        else if(this.alertTime <=0f)
        {
            StopCoroutine(alertcoroutine);
            ChangeState(State.mission);
            IspathFind = false;
            this.alertTime = 5f;
            nextNode = 0;
            Debug.Log("��θ� ��Ž���մϴ�.");

        }

    }
    void Die() //��� ����
    {
        //���� ��� �ִ� ����� ����ź, ��ź�� ����߸���, ��Ȱ��ȭ��.
        //dropped weapon();
    }


    private void OnDrawGizmos()
    {
        Handles.color = new Color(2f, 0f, 0f, 0.1f);

        Handles.DrawSolidArc(transform.position, transform.forward, transform.up, angleRange / 2, distance);

        Handles.DrawSolidArc(transform.position, transform.forward, transform.up, -angleRange / 2, distance);
    }
    IEnumerator AlertTime()
    {
        this.alertTime -= Time.deltaTime;
        //Debug.Log($"���� �ð�{this.alertTime}");
        yield return new WaitForSeconds(0.1f);
    }

    void AIPath()
    {
        if (pathfinder != null)
        {
            if (pathfinder.npcpath.Count > 0)
            {
                List<Node> path = pathfinder.npcpath;
                Node next = path[nextNode];
                Vector2 dir = (next.worldPosition - transform.position).normalized;
                // gameObject.transform.up = dir;

                transform.Translate(dir * Time.deltaTime*gameObject.GetComponent<AI>().movespeed, Space.World);

                if(Vector2.Distance(gameObject.transform.position,next.worldPosition) < 0.1f)
                {

                    if (next == path[path.Count - 1])
                    {
                        IspathFind = false;
                        ChangeState(State.alert);
                        return;
                    }

                    nextNode++;
                }
            }
            
        }
    }

}
