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
    public Button Weapon;
    public Image firebar;
    public Image waterbar;
    public TextMeshProUGUI state;

    private int power;
    ColorBlock cb;

    public int fireExp;
    public int waterExp;
    
    public bool IsFIre;
    public bool IsWater;
    void Start()
    {
        GM = GetComponent<GameManager>();
        power = 5;
        fireExp = 0;
        waterExp = 0;
        maxFire = 150;
        maxWater = 150;
        cb = Weapon.colors;
    }

    void Update()
    {

    }

    public void WeaponTouch()
    {
        if (IsFIre)
        {
            if(maxFire <= fireExp)
            {
                fireExp = maxFire;
                return;
            }
            fireExp += power;

            float exp = (float)fireExp / (float)maxFire;

            firebar.fillAmount = exp;
            waterbar.fillAmount = exp;
            Debug.Log(exp);

            float color = exp * 127;
            Debug.Log(color);
            
            cb.normalColor = new Color32(255, (byte)(255 - color), (byte)(255 - color), 255);
            cb.selectedColor = new Color32(255, (byte)(255 - color), (byte)(255 - color), 255);
            cb.highlightedColor = new Color32(255, (byte)(255 - color), (byte)(255 - color), 255);
            Weapon.colors = cb;
        }
        if (IsWater)
        {
            if (maxWater <= waterExp)
            {
                waterExp = maxWater;
                GM.Level += 1;
                CangeWeapon();
            }
            waterExp += power;

            float exp = (float)waterExp / (float)maxWater;

            firebar.fillAmount = 1 - exp;
            Debug.Log(1 - exp);

            float color = exp * 127;
            Debug.Log(color);

            cb.normalColor = new Color32(255, (byte)(128 + color), (byte)(128 + color), 255);
            cb.selectedColor = new Color32(255, (byte)(128 + color), (byte)(128 + color), 255);
            cb.highlightedColor = new Color32(255, (byte)(128 + color), (byte)(128 + color), 255);
            Weapon.colors = cb;
        }
    }

    public void OnFire()
    {
        if (fireExp == 0)
        {
            IsFIre = true;
            cb.pressedColor = new Color32(233, 113, 113, 255);
            Weapon.colors = cb;
            state.text = "단련";
        }
        else { return; }
    }

    public void OnWater()
    {
        if (fireExp == maxFire)
        {
            IsFIre = false;
            IsWater = true;
            cb.pressedColor = new Color32(0, 120, 255, 255);
            Weapon.colors = cb;
            state.text = "단련";
        }
        else { return; }
    }

    void CangeWeapon()
    {
        Weapon.image.sprite = Weapons[GM.Level];

        fireExp = 0;
        waterExp = 0;
        firebar.fillAmount = 0;
        waterbar.fillAmount = 0;

    }

}
