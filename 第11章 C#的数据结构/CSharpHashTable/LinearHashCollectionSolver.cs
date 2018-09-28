using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHashTable
{
    public class LinearHashCollectionSolver : IHashCollectionSolver
    {
        public int GetNextOffset(int length, int previous)
        {
            //返回下一个槽位。如果当前已经是最后一个则返回0
            return previous < length - 1 ? previous + 1 : 0;
        }

    }
}
