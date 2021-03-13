using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    ///"miliseconds from 1970-01-01T00:00:00Z (UTC)")
    public static long EpochTime()
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        long cur_time = (long)(System.DateTime.UtcNow - epochStart).TotalMilliseconds;
        return cur_time;
    }
}
