public static class RandomExtensions
{
  public static float NextFloat(this System.Random rand)
  {
    return (float)rand.NextDouble();
  }

  public static double NextDoubleRanged(this System.Random rand, double min, double max)
  {
    return min + rand.NextDouble() * (max - min);
  }

  public static float NextFloatRanged(this System.Random rand, float min, float max)
  {
    return min + (float)rand.NextDouble() * (max - min);
  }

  public static int NextIntRanged(this System.Random rand, int min, int max)
  {
    return min + UnityEngine.Mathf.FloorToInt(rand.NextFloat() * (max - min));
  }
}