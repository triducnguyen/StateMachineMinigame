using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviour
{
    public IEnumerator Behaviour();
    //when the behaviour changes
    public void OnEnter();
    public void OnExit();
    public void OnTargetReached();

}
