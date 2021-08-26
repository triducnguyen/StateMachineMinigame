using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviour
{
    //when the behaviour changes
    public void OnEnter();
    public void OnExit();

    //checking to change current behaviour
    public bool CheckConditions(List<Func<bool>> conditions);
}
