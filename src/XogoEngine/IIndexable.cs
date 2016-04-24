namespace XogoEngine
{
    public interface IIndexable<out T1, in T2>
    {
        T1 this[T2 index]
        {
            get;
        }
    }
}
