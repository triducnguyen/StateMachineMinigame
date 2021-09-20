using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : Singleton<Money>
{
    public Text text;

    public float money
    {
        get
        {
            return _money;
        }
        protected set
        {
            _money = value;
            //update display number
            UpdateMoneyCounter();
        }
    }
    public float _money = 0;

    public float maxMoney = 9999999;

    public void UpdateMoneyCounter()
    {
        text.text = money.ToString("$0000000000.00");
    }

    private void Start()
    {
        UpdateMoneyCounter();
    }

    public bool CanDepositWholeAmount(float amount, out float sum)
    {
        sum = money + amount;
        if (sum > maxMoney)
        {
            sum = money;
            return false;
        }
        return true;
    }

    public bool CanWithdrawlWholeAmount(float amount, out float sum)
    {
        sum = money - amount;
        if (sum < 0)
        {
            sum = money;
            return false;
        }
        return true;
    }

    public void Deposit(float amount)
    {
        float sum;
        if (CanDepositWholeAmount(amount, out sum))
        {
            money = sum;
        }
        else
        {
            money = maxMoney;
        }
    }

    public bool Withdrawl(float amount)
    {
        float sum;
        if (CanWithdrawlWholeAmount(amount, out sum))
        {
            money = sum;
            return true;
        }
        return false;
    }

}
