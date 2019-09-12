using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownBox : icebox
{
    protected override void OnFreeze()
    {
        base.OnFreeze();
        manager.SlowdownGravity();
    }
}
