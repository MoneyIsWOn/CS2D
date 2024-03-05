using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Weapon[] HAND = new Weapon[5]; //현재 가진 무기 목록
    [SerializeField] Dictionary<int,Weapon> WeaponInfo = new Dictionary<int,Weapon>(); // 무기 매니저 활성화, 비활성화 여부
    [SerializeField] GameObject droppoint;
    [SerializeField] string CurrentWeapon;

    public Weapon curweapon;

    Coroutine firecoroutine;
    Coroutine reloadcoroutine;

    void FirstSetting() //시작 시 모든 무기 비활성화+ 칼 무기로 시작
    {
        Weapon[] Weapons = gameObject.GetComponentsInChildren<Weapon>(); //자식 아래의 모든 무기 정보들을 딕셔너리에 추가.

        for (int x = 0; x < Weapons.Length; x++)
        {
            WeaponInfo.Add(Weapons[x].GetComponent<Weapon>().weaponnumber, Weapons[x]);
            Debug.Log(Weapons[x].GetComponent<Weapon>().weaponnumber);
        }

        HAND[2] = WeaponInfo[50]; // weapon number 50 -> knife
        Weapon[] setweapon = gameObject.GetComponentsInChildren<Weapon>();

        foreach(Weapon a in setweapon)
        {
            a.gameObject.SetActive(false);
        }
        Debug.Log("모든 무기 비활성화");

        curweapon = HAND[2];

        curweapon.gameObject.SetActive(true); //칼 무기만 활성화.

    }

    void Start()
    {
        FirstSetting();
    }
    private void Update()
    {
        if (curweapon != null)
        {
            CurrentWeapon = $"{curweapon.name}";
        }
        else
        {
            CurrentWeapon = $"None";
        }
    }

    void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            if (curweapon == null)
            {
                Debug.Log("현재 무기가 null입니다.");
            }
            else
            {
                firecoroutine = StartCoroutine(curweapon.GetComponent<Weapon>().FireCoroutine());
            }
        }
        else
        {
            StopCoroutine(firecoroutine); 
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

            for(int x=0;x<HAND.Length;x++) //무기를 버린 뒤 현재 무기 목록에서 지움.
            {
               if(curweapon == HAND[x])
                {
                    HAND[x] = null;
                }
            }
            curweapon = HAND[2];
            curweapon.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("이 무기는 버릴 수 없다.");
        }

    }

    void OnReload()
    {
        reloadcoroutine = StartCoroutine(curweapon.GetComponent<Weapon>().ReloadCoroutine());
    }
    void OnRifle(InputValue button)
    {

        if (HAND[0] != null)
        {
           curweapon.gameObject.SetActive(false);
           curweapon = HAND[0];
           curweapon.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("현재 그 번호에 무기가 없다.");
        }

    }
    void OnPistol(InputValue button)
    {

        if (HAND[1] != null)
        {
            curweapon.gameObject.SetActive(false);
            curweapon = HAND[1];
            curweapon.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("현재 그 번호에 무기가 없다.");
        }


    }
    void OnMelee(InputValue button)
    {

        if (HAND[2] != null)
        {
            curweapon.gameObject.SetActive(false);
            curweapon = HAND[2];
            curweapon.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("현재 그 번호에 무기가 없다.");
        }

    }
    void OnGrenade(InputValue button)
    {

        if (HAND[3] != null)
        {
            curweapon.gameObject.SetActive(false);
            curweapon = HAND[3];
            curweapon.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("현재 그 번호에 무기가 없다.");
        }


    }
    void OnBomb(InputValue button)
    {

        if (HAND[4] != null)
        {
            curweapon.gameObject.SetActive(false);
            curweapon = HAND[4];
            curweapon.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("현재 그 번호에 무기가 없다.");
        }

    }
}
