using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AI : Human
{
    [SerializeField] float movespeed;
    Vector2 moveDir;
    public static Vector2 mouse;
    float angle;

    void AIeye() // Ai�� �þ�
    {

    }
    void Update()
    {
        this.Hpcheck();
        AIeye();
        gameObject.transform.Translate(moveDir * movespeed * Time.deltaTime,Space.World);
    }
    void AiMove() //Ai�� ������
    {

    }

}
