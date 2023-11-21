using System.Threading.Tasks;

namespace Aid.Factory.Async
{
    public interface IAsyncFactory<T>
    {
        Task<T> Create();
    }
}