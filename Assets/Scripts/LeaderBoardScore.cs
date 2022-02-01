using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class SurvivalLeaderBoardScore
{
    public string name;
    public int score;
}

public class AdventureLeaderBoardScore
{
    public string name;
    public float score;
}
