using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBox : icebox
{
    protected override void OnFreeze()
    {
        base.OnFreeze();
        manager.IncraeseLives();
    }
}
