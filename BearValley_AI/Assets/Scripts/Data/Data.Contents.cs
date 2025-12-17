using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Stat
[Serializable]
public class Stats
{
    public int level;
    public string hp;
    public string attack;
}

[Serializable]
public class StatData : ILoader<int, Stats>
{
    public List<Stats> stats = new List<Stats>();

    public Dictionary<int, Stats> MakeDict()
    {
        Dictionary<int, Stats> dict = new Dictionary<int, Stats>();
        foreach (Stats testStat in stats)
            dict.Add(testStat.level, testStat);

        return dict;
    }
}

#endregion
