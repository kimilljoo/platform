using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static int maxHp = 3;
    public static int curHp;
    private void Start()
    {
        curHp = maxHp;
    }

}
