using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<PlayerData> playersData;
    public float gameSoundVolume = 0.8f;
    public float musicVolume = 0.2f;
}
