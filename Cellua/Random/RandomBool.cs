namespace Cellua.Random
{
    public class RandomBool: System.Random
    {
        private uint _boolBits;

        public RandomBool()
        {
        }

        public RandomBool(int seed) : base(seed)
        {
        }

        public bool NextBoolean()
        {
            _boolBits >>= 1;
            if (_boolBits <= 1) _boolBits = (uint) ~Next();
            return (_boolBits & 1) == 0;
        }
    }
}