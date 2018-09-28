using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHashTable
{
    public interface IHashCollectionSolver
    {
        //获得下一个地址位置
        int GetNextOffset(int length, int previous);

    }
}
