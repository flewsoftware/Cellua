using Cellua.Random;

namespace Cellua.Api.Common
{
    public class RandomApi
    {
        public RandomBool NewRandomBool()
        {
            return new RandomBool();
        }
        public RandomBool NewRandomBool(int seed)
        {
            return new RandomBool(seed);
        }
    }
}