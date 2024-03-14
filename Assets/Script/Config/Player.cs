using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Human
{
    [SerializeField] float movespeed;
    Vector2 moveDir;
    public static Vector2 mouse;
    float angle;
    [SerializeField] FIRETRACKING firetrack; //�Ѿ��� ����
    [SerializeField] Animator animator;
    Coroutine reloadcoroutine;
    float reloadcooltime;
    bool IsReload;

    void cursor()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Vector2 dir = ((Vector3)mouse - transform.position).normalized;
        //transform.up = dir;

        angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        // itDebug.Log($"mousepoint ({Input.mousePosition}), angle : ({angle})");

        // transform.rotation = Quaternion.Euler(0, 0, angle -90);

        this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void Update()
    {
        this.Hpcheck();
        cursor();
        gameObject.transform.Translate(moveDir * movespeed * Time.deltaTime,Space.World);
    }

    void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.y = value.Get<Vector2>().y;
    }



    public void Fire(Weapon curweapon)
    {
        if (curweapon == transform.gameObject.GetComponentInChildren<WeaponManager>().HAND[2])
        {
            animator.SetBool("Knife", true);
            Vector3 fireDir = transform.up;
            firetrack.gameObject.SetActive(true);
            Debug.DrawRay(transform.position, fireDir * float.MaxValue, Color.red, 1f);
            curweapon.magazine--;
            Debug.Log("Į��");
            animator.SetBool("Knife", false);
        }
        else if (curweapon.magazine > 0)
        {
            animator.SetBool("Fire", true);
            Vector3 fireDir = transform.up;
            firetrack.gameObject.SetActive(true);
           Debug.DrawRay(transform.position, fireDir * float.MaxValue, Color.red, 1f);
            curweapon.magazine--;
            Debug.Log("���");
            animator.SetBool("Fire",false);
        }

    }
    public bool IsReloading { get; private set; }

    public void Reload(Weapon curWeapon)
    {
        if (IsReloading || curWeapon == null)
        {
            return; // �̹� ������ ���̰ų� ���Ⱑ ���� ��� �������� �������� ����
        }

        IsReloading = true;

        Debug.Log("������ ����");
        animator.SetBool("Reload", true);
        StartCoroutine(ReloadCoroutine(curWeapon));
    }

    private IEnumerator ReloadCoroutine(Weapon curWeapon)
    {
        yield return new WaitForSeconds(curWeapon.reloadtime);

        int bulletsNeeded = curWeapon.magazineCapacity - curWeapon.magazine;

        if (curWeapon.maxammo >= bulletsNeeded)
        {
            curWeapon.maxammo -= bulletsNeeded;
            curWeapon.magazine += bulletsNeeded;
            Debug.Log("������ �Ϸ�");
        }
        else
        {
            curWeapon.magazine += curWeapon.maxammo;
            curWeapon.maxammo = 0;
            Debug.Log("������ �Ϸ�. ���� ź���� �����մϴ�.");
        }
        animator.SetBool("Reload", false);
        IsReloading = false;
    }
    //public IEnumerator ReloadCoroutine(Weapon curweapon)
    //{
    //    while(reloadcooltime >= 0)
    //    {
    //        Debug.Log($"�������� {reloadcooltime}");
    //        yield return new WaitForSeconds(1);
    //        reloadcooltime -= 1;

    //    }
    //    if(reloadcooltime <= 0)
    //    {
    //        IsReload = true;
    //    }
    //}
    //public void Reload(Weapon curweapon)
    //{
    //    reloadcooltime = curweapon.reloadtime;
    //    if (curweapon.maxammo > curweapon.magazineCapacity)
    //    {
    //        if(reloadcoroutine != null)
    //        {
    //            reloadcoroutine = null;
    //        }

    //        reloadcoroutine = StartCoroutine(ReloadCoroutine(curweapon));
    //        Debug.Log($"{reloadcooltime}");

    //        if (IsReload)
    //        {
    //            curweapon.maxammo -= curweapon.magazineCapacity - curweapon.magazine;
    //            curweapon.magazine = curweapon.magazineCapacity;
    //            Debug.Log("��������");
    //            IsReload = false;
    //        }
    //    }
    //    else if (curweapon.maxammo <= curweapon.magazineCapacity && (curweapon.maxammo != 0))
    //    {
    //        //Debug.Log("2");
    //        reloadcoroutine = StartCoroutine(ReloadCoroutine(curweapon));
    //        if (IsReload)
    //        {
    //            StopCoroutine(reloadcoroutine);
    //            curweapon.magazine = curweapon.maxammo;
    //            curweapon.maxammo = 0;
    //            Debug.Log("��������");
    //            IsReload = false;
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("ź���� �� ���������ϴ�. �������� ���⸦ ��ų�, �ٸ� ���⸦ ã���ʽÿ�.");
    //    }
    //}
}
