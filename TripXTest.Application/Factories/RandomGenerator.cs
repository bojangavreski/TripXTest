using System.Security.Cryptography;

namespace TripXTest.Application.Factories
{
    public static class RandomGenerator
    {
        private const string Chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int CodeLength = 6;

        public static string GenerateCode()
        {
            return RandomNumberGenerator.GetString(Chars, CodeLength).ToString();
        }

        public static double GeneratePrice(double min = 100, double max = 1000)
        {
            return (RandomNumberGenerator.GetInt32((int)min, (int)max) + 
                        RandomNumberGenerator.GetInt32(0, 100) / 100.0);
        }

        public static int GenerateSleepTime()
        {
            return RandomNumberGenerator.GetInt32(30,61);
        }
    }
}
