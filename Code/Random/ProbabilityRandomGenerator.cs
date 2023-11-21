using System.Collections.Generic;

namespace Aid.Random
{
    public class ProbabilityRandomGenerator
    {
        private readonly List<int> list;
        
        private readonly System.Random randomGenerator;

        public ProbabilityRandomGenerator(int capacity, int seed)
        {
            list = new List<int>(capacity);
            randomGenerator = new System.Random(seed);
        }

        public void AddNumber(int number, int probability)
        {
            for (int i = 0; i < probability; i++)
                list.Add(number);
        }

        public int GetNumber()
        {
            return list[randomGenerator.Next(0, list.Count)];
        }

        public void Clear()
        {
            list.Clear();
        }
    }
}