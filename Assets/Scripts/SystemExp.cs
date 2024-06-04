using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemExp : MonoBehaviour
{
    public int nivel;
    public int currentExp;
    public int maxExp;
    public static Action<int> levelUp;
    public void OnEnable()
    {
        levelUp += QuantityExp;
    }
    public void OnDisable()
    {
        levelUp -= QuantityExp;
    }
    public void QuantityExp(int quantity)
    {
        currentExp += quantity;
        Compare();
    }
    private void Compare()
    {
        if (currentExp >= maxExp)
        {
            nivel++;
            maxExp = Mathf.RoundToInt(maxExp * 3.5f);
        }
    }
}
