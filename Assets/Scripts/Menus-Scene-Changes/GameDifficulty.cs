using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficulty : MonoBehaviour
{
    // 0 = easy, 1 = medium, 2 = hard
    public static int gameDifficulty = 0;

    public void setModeEasy()
    {
        gameDifficulty = 0;
    }

    public void setModeMedium()
    {
        gameDifficulty = 1;
    }

    public void setModeHard()
    {
        gameDifficulty = 2;
    }
}
