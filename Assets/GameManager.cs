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
        goldText.text = gold + "��";
    }

    public double cost(int b, int k, float r, int n)
    {
        //��� ������ ����. b: ù°��(�⺻ ����ġ), r: ����(�����Ǵ� ��), k: ����, ���� ��ġ(����Ƚ��), n: ����
        return (b * ((Mathf.Pow(r, k) - Mathf.Pow(r, k + n)) / (1 - r)));
    }

    public void PulsGold()
    {
        gold += 100;
    }

}
