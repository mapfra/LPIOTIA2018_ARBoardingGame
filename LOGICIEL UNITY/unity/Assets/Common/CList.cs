using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Common
{
    public class CList<T>
    {
        List<T> list = new List<T>();
        public int token = 0;

        public T this[int index]
        {
            get { return list[index % Count]; }
            set { list[index % Count] = value; }
        }

        public void NextTurn()
        {
            token = (token + 1) % Count;
        }

        public T GetPlayer()
        {
            return this[token];
        }

        public int Count
        {
            get { return list.Count; }
        }

        public List<T> List
        {
            get { return list; }
            set { list = value; }
        }
    }
}
