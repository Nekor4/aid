using System;
using System.Threading.Tasks;

namespace Aid.Pool.Async
{
    public interface IAsyncPool<T>
    {
        void Prewarm(int num);
        Task<T> Request();
        void Request(Action<T> callback);
        void Return(T member);
    }
}