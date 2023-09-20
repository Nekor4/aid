using System.Collections.Generic;
using Aid.Factory;

namespace Aid.Pool
{
    public class Pool<T> : IPool<T>
    {
        private IFactory<T> factory;

        public Pool(IFactory<T> factory)
        {
            this.factory = factory;
        }

        protected readonly Stack<T> Available = new Stack<T>();
        protected bool HasBeenPrewarmed { get; set; }

        protected virtual T Create()
        {
            return factory.Create();
        }

        public virtual void Prewarm(int num)
        {
            if (HasBeenPrewarmed)
            {
                return;
            }

            for (int i = 0; i < num; i++)
            {
                Available.Push(Create());
            }

            HasBeenPrewarmed = true;
        }

        public virtual T Request()
        {
            return Available.Count > 0 ? Available.Pop() : Create();
        }

        public virtual IEnumerable<T> Request(int num = 1)
        {
            List<T> members = new List<T>(num);
            for (int i = 0; i < num; i++)
            {
                members.Add(Request());
            }

            return members;
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