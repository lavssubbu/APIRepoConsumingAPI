namespace APICodeFirst.Interface
{
    public interface IBookAuthor<T, K>//Generic Interface
    {
        T Add(T item);
        T Update(T item);
        T Delete(K item);
        T GetValue(K item);
        ICollection<T> GetAll();
    }

}
