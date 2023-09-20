using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aid.Factory;

namespace Aid.Pool.Async
{
    public class AsyncPool<T> : IAsyncPool<T>
    {
        private readonly IAsyncFactory<T> factory;

        public AsyncPool(IAsyncFactory<T> factory)
        {
            this.factory = factory;
        }

        protected readonly Stack<T> Available = new Stack<T>();
        protected bool HasBeenPrewarmed { get; set; }

        protected virtual Task<T> Create()
        {
            return factory.Create();
        }

        public virtual async void Prewarm(int num)
        {
            if (HasBeenPrewarmed)
            {
                return;
            }

            for (int i = 0; i < num; i++)
            {
                var task = Create();
                await task;
                Available.Push(task.Result);
            }

            HasBeenPrewarmed = true;
        }

        public Task<T> Request()
        {
            if (Available.Count > 0)
            {
                return new Task<T>(Available.Pop);
            }
            
            return Create();
        }

        public virtual async void Request(Action<T> callback)
        {
            if (Available.Count > 0)
            {
                callback?.Invoke(Available.Pop());
                return;
            }

            var task = Create();
            await task;

            callback?.Invoke(task.Result);
        }

        public virtual async void Request(int num, Action<IEnumerable<T>> callback)
        {
            var members = new List<T>(num);
            for (int i = 0; i < num; i++)
            {
                if (Available.Count > 0)
                {
                    members.Add(Available.Pop());
                    continue;
                }

                var task = Create();
                await task;
                members.Add(task.Result);
            }

            callback?.Invoke(members);
        }

        public virtual void Return(T member)
        {
            Available.Push(member);
        }

        public virtual void Return(IEnumerable<T> members)
        {
            foreach (var member in members)
            {
                Return(member);
            }
        }
    }
}