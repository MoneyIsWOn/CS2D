using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Player user;
    [SerializeField] Weapon[] HAND = new Weapon[5]; //���� ���� ���� ���
    [SerializeField] Dictionary<int,Weapon> WeaponInfo = new Dictionary<int,Weapon>(); // ��� ����� �����ȣ�� ������.
    [SerializeField] GameObject droppoint;
    [SerializeField] public Weapon curweapon; //���� ����
    [SerializeField] GameObject BuyMenu;

    bool weaponcooltime;
   

    Coroutine firecoroutine;
    Coroutine reloadcoroutine;
    void FirstSetting() //���� �� ��� ���� ��Ȱ��ȭ+ Į ����� ����
    {
        Weapon[] Weapons = gameObject.GetComponentsInChildren<Weapon>(); //�ڽ� �Ʒ��� ��� ���� �������� ��ųʸ��� �߰�.

        for (int x = 0; x < Weapons.Length; x++)
        {
            WeaponInfo.Add(Weapons[x].GetComponent<Weapon>().weaponnumber, Weapons[x]);
        }

        HAND[2] = WeaponInfo[50]; // weapon number 50 -> knife
        Weapon[] setweapon = gameObject.GetComponentsInChildren<Weapon>();

        foreach(Weapon a in setweapon) //��� ���� ��Ȱ��ȭ
        {
            a.gameObject.SetActive(false);
        }

        curweapon = HAND[2];

        curweapon.gameObject.SetActive(true); //Į ���⸸ Ȱ��ȭ.

    }
    void Start()
    {
        FirstSetting();
    }





    public IEnumerator FireCoroutine(Weapon curweapon)
    {
        while (true)
        {
            if (user != null)
            {
                user.Fire(curweapon);
            }
            else
            {
                Debug.Log($"{gameObject.transform.parent.gameObject.name}�� ���������̳ʸ� ����� �� �����ϴ�.");
            }

            yield return new WaitForSeconds(curweapon.firecooltime);

        }
    }
    public IEnumerator ReloadCoroutine(Weapon curweapon)
    {
        //animator.SetBool("Reload", true);
        yield return new WaitForSeconds(curweapon.reloadtime);
        //animator.SetBool("Reload", false);
    }
    void OnFire(InputValue value)
    {

        if (value.isPressed)
        {
            if(curweapon.GetComponent<Bomb>())
            {

            }

            if(curweapon != null)
            {
                firecoroutine = StartCoroutine(FireCoroutine(curweapon));               
            }
        }
        else
        {
            StopCoroutine(firecoroutine);
        }

    }
    void OnReload()
    {
        if (user != null)
        {

            bool IsReload = user.Reload(curweapon);
            if (IsReload) //�������� �����ϸ�
            { reloadcoroutine = StartCoroutine(ReloadCoroutine(curweapon)); }
        }
    }
    public void PickUpWeapon(GameObject pickupweapon)
    {
        int number = pickupweapon.GetComponent<item>().weaponnumber;

        switch (pickupweapon.GetComponent<item>().weaponstyle)
        {
            case 0:
                if (HAND[0] == null)
                {
                    HAND[0] = WeaponInfo[number];
                    Destroy(pickupweapon);
                }
                break;
            case 1:
                if (HAND[1] == null)
                {
                    HAND[1] = WeaponInfo[number];
                    Destroy(pickupweapon);
                }
                break;
            case 3:
                if (HAND[3] == null)
                {
                    HAND[3] = WeaponInfo[number];
                    Destroy(pickupweapon);
                }
                break;
            case 4:
                if (HAND[4] == null)
                {
                    HAND[4] = WeaponInfo[number];
                    Destroy(pickupweapon);
                }
                break;
        }
    }
    void OnDropWeapon(InputValue value)
    {
        DropWeapon();
    }
    void DropWeapon()
    {
        if (curweapon != HAND[2])
        {
            curweapon.gameObject.SetActive(false);

            GameObject dropitem = curweapon.GetComponent<Weapon>().dropPrefab;

            Instantiate(dropitem, droppoint.transform.position, transform.rotation);

            for (int x = 0; x < HAND.Length; x++) //���⸦ ���� �� ���� ���� ��Ͽ��� ����.
            {
                if (curweapon == HAND[x])
                {
                    HAND[x] = null;
                }
            }
            curweapon = HAND[2];
            curweapon.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("�� ����� ���� �� ����.");
        }

    }
    void OnRifle(InputValue button)
    {
        ChangeWeapon(HAND[0]);
    }
    void OnPistol(InputValue button)
    {
        ChangeWeapon(HAND[1]);
    }
    void OnMelee(InputValue button)
    {
        ChangeWeapon(HAND[2]);
    }
    void OnGrenade(InputValue button)
    {
        ChangeWeapon(HAND[3]);
    }
    void OnBomb(InputValue button)
    {
        ChangeWeapon(HAND[4]);
    }

    void OnBuyMenu(InputValue button)
    {
        if(BuyMenu.activeSelf == true)
        {
            BuyMenu.SetActive(false);
        }
        else if(BuyMenu.activeSelf == false)
        {
            BuyMenu.SetActive(true);
        }
    }
    void ChangeWeapon(Weapon swapweapon) //���� ������ �ִ� ���� �� �ٸ� ����� ���.
    {
        if (HAND[swapweapon.weaponstyle] != null)
        {
            Weapon prevweapon = curweapon;
            this.curweapon = swapweapon;
            prevweapon.gameObject.SetActive(false);
            curweapon.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("�� ��ȣ�� ���Ⱑ ���� ������ ���� �ʽ��ϴ�.");
        }
    }
    public void BuyWeapon(Weapon purchaseweapon)
    {
        ChangeWeapon(HAND[purchaseweapon.weaponstyle]);
        DropWeapon();
        HAND[purchaseweapon.weaponstyle] = purchaseweapon;
        curweapon = purchaseweapon;
    }
}
