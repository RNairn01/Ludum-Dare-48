using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public enum Difficulty
{
   Easy,
   Normal,
   Hard
}
public class GameSettings : MonoBehaviour
{
   public static Difficulty gameDifficulty = Difficulty.Normal;
   public static int TimerLength { get; private set; }
   public static int lives;
   public static float scoreMultiplier;
   

   public static void ImplementGameSettings()
   {
      switch (gameDifficulty)
      {
         case Difficulty.Easy:
            TimerLength = 15;
            lives = 5;
            scoreMultiplier = 1;
            break;
         case Difficulty.Normal:
            TimerLength = 10;
            lives = 3;
            scoreMultiplier = 1.5f;
            break;
         case Difficulty.Hard:
            TimerLength = 5;
            lives = 3;
            scoreMultiplier = 2;
            break;
      }
   }
}
