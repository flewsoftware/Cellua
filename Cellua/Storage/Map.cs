namespace Cellua.Storage
{
    public struct Map<T>
    {
        public T[,] Data;

        public Map(uint size)
        {
            Data = new T[size, size];
        }
    }
}