using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public Sprite[] Weapons;

    GameManager GM;

    public int maxFire;
    public int maxWater;
    public int limitGold;

    public Button Weapon;
    public Image firebar;
    public Image waterbar;
    public TextMeshProUGUI state;
    public Button lockbutton;
    public TextMeshProUGUI locktext;
    public TextMeshProUGUI par;
    [SerializeField]
    private int power;
    ColorBlock cb;
    ColorBlock white;

    public int fireExp;
    public int waterExp;
    float exp;
    
    public bool IsFIre;
    public bool IsWater;
    void Start()
    {
        GM = GetComponent<GameManager>();
        power = (int)(GM.cost(5, 2, 1.07f, GM.Level)*0.4f);
        Debug.Log((GM.cost(50, 10, 1.07f, GM.Level) * 0.4f));
        fireExp = 0;
        waterExp = 0;
        maxFire = maxWater = (int)GM.cost(50, 15, 1.07f, GM.Level);
        exp = 0;
        cb = white = Weapon.colors;
        CangeWeapon();
        OnLock();
    }

    void Update()
    {
        locktext.text = "잠금\n" + limitGold + "/\n" + GM.gold;
        par.text = string.Format("{0:###.#}%", (exp * 100));
        if (exp == 0)
        {
            par.alignment = TextAlignmentOptions.Center;
            par.text = "0%";
        }
        else { par.alignment = TextAlignmentOptions.Right; }

    }

    public void WeaponTouch()
    {
        if (IsFIre)
        {
            fireExp += power;

            exp = (float)fireExp / (float)maxFire;

            firebar.fillAmount = exp;
            waterbar.fillAmount = exp;
            Debug.Log(exp);

            float color = exp * 127;
            Debug.Log(color);

            cb.highlightedColor = cb.normalColor = new Color32(255, (byte)(255 - color), (byte)(255 - color), 255);
            Weapon.colors = cb;

            if (maxFire <= fireExp)
            {
                fireExp = maxFire;
                exp = 1;
                return;
            }
        }
        if (IsWater)
        {
            waterExp += power;

            exp = (float)waterExp / (float)maxWater;

            firebar.fillAmount = 1 - exp;
            Debug.Log(1 - exp);

            float color = exp * 127;
            Debug.Log(color);

            cb.highlightedColor = cb.normalColor = new Color32(255, (byte)(128 + color), (byte)(128 + color), 255);
            Weapon.colors = cb;

            if (maxWater <= waterExp)
            {
                IsWater = false;
                waterExp = maxWater;
                GM.Level += 1;
                exp = 1;
                Weapon.colors = white;
                StartCoroutine(Cange());
            }
        }
    }

    IEnumerator Cange()
    {
        yield return new WaitForSecondsRealtime(1f);
        CangeWeapon();
    }

    public void OnFire()
    {
        if (fireExp == 0)
        {
            IsFIre = true;
            cb.pressedColor = new Color32(233, 113, 113, 255);
            Weapon.colors = cb;
            exp = 0;
            state.text = "단련";
        }
        else { return; }
    }

    public void OnWater()
    {
        if (fireExp == maxFire)
        {
            fireExp++;
            IsFIre = false;
            IsWater = true;
            cb.pressedColor = new Color32(0, 120, 255, 255);
            Weapon.colors = cb;
            exp = 0;
            state.text = "식히기"; 
        }
        else { return; }
    }

    void CangeWeapon()
    {
        Weapon.image.sprite = Weapons[GM.Level-1];

        state.text = "준비";
        fireExp = 0;
        waterExp = 0;
        waterbar.fillAmount = firebar.fillAmount = 0;
        cb = white;
        exp = 0;
        power = (int)(GM.cost(5, 2, 1.07f, GM.Level) * 0.4f);
        maxFire = maxWater = (int)GM.cost(50, 15, 1.07f, GM.Level);
        OnLock();

    }

    void OnLock()
    {
        lockbutton.gameObject.SetActive(true);
    }

    public void UnLock()
    {
        if (GM.gold >= limitGold)
        {
            lockbutton.gameObject.SetActive(false);
            GM.gold -= limitGold;
        }
        else { return; }
    }

}
