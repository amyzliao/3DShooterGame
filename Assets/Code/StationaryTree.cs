using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryTree : TreeBase
{
    public override void UpdateBehavior()
    {
        // nothing for now, just need to decrement score
    }

    private new void Update() {
        base.Update();
    }
}
