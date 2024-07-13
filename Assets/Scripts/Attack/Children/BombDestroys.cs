using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDestroys : DestroyPlatforms
{
    protected override void ApplyDamage(int island)
    {
        ScoreKeeper.HitByBomb(island);
    }
}
