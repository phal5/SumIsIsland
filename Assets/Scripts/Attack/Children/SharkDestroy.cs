using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkDestroy : DestroyPlatforms
{
    protected override void ApplyDamage(int island)
    {
        ScoreKeeper.HitByShark(island);
    }
}
