using System.Threading.Tasks;

namespace Aid.Factory
{
    public interface IAsyncFactory<T>
    {
        Task<T> Create();
    }
}