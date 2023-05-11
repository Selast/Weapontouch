using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int Level = 1;
    public int gold = 0;
    public TextMeshProUGUI goldText;

    void Start()
    {
        
    }

    void Update()
    {
        goldText.text = gold + "원";
    }

    public double cost(int b, int k, float r, int n)
    {
        //등비 수열과 같음. b: 첫째항(기본 경험치), r: 공비(제곱되는 수), k: 지수, 항의 위치(제곱횟수), n: 범위
        return (b * ((Mathf.Pow(r, k) - Mathf.Pow(r, k + n)) / (1 - r)));
    }

    public void PulsGold()
    {
        gold += 100;
    }

}
