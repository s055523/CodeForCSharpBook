using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new CSharpLinkedList<int>();
            var data = Enumerable.Range(1, 50).ToList();
            foreach (var i in data)
            {
                test.AddLast(i);
            }
            Console.WriteLine(test.Count);

            foreach (var i in data.Where(d => d % 2 == 0))
            {
                test.Remove(i);
            }
            Console.WriteLine(test.Count);

            test.Print();

            foreach (var i in data.Where(d => d % 3 == 0))
            {
                test.Remove(i);
            }
            Console.WriteLine(test.Count);

            test.Print();

            Console.ReadKey();
        }
    }

     /// <summary>
     /// 双向链表
     /// </summary>
     /// <typeparam name="T"></typeparam>
    public class CSharpLinkedList<T> : IEnumerable<T>
    {
        //双向链表的头节点。通过它可以访问到链表所有的节点
        //C#选择不把头节点暴露为公共的，这样可以使得用户不能在初始化链表时传入头节点
        //防止传入了头节点却忘了设置它的prev和next导致之后插入删除发生错误的问题
        //头节点必须通过AddFirst或AddLast方法插入，在那两个方法里，设置头节点的prev和next
        internal CSharpLinkedListNode<T> head;

        //C#的链表内部维护一个长度，使得统计长度的时间复杂度为常数
        private int count;

        //对外的
        public int Count
        {
            get { return count; }
        }

        //链表第一个成员
        public CSharpLinkedListNode<T> First
        {
            get { return head; }
        }

        //链表最后一个成员
        public CSharpLinkedListNode<T> Last
        {
            get
            {
                if (head != null)
                {
                    return head.prev;
                }
                return null;
            }
        }

        //构造函数什么都不需要做
        public CSharpLinkedList()
        {
        }

        public CSharpLinkedListNode<T> Find(T value)
        {
            var next = head;
            var comparer = EqualityComparer<T>.Default;
            if (next != null)
            {
                //如果传入的不是null，遍历链表
                if (value != null)
                {
                    //泛型不能直接比较，需要引入EqualityComparer
                    while (!comparer.Equals(next.item, value))
                    {
                        next = next.next;
                        //双向链表转回头意味着遍历结束
                        if (next == head)
                        {
                            return null;
                        }
                    }
                    return next;
                }
                //如果传入的是null，遍历链表，返回第一个值为null的节点
                while (next.item != null)
                {
                    next = next.next;
                    //双向链表转回头意味着遍历结束
                    if (next == head)
                    {
                        return null;
                    }
                }
                return next;
            }
            return null;
        }

        internal void InsertNodeToEmptyList(CSharpLinkedListNode<T> node)
        {
            head = node;
            head.next = head;
            head.prev = head;
            count++;
        }
        internal void InsertNodeBefore(CSharpLinkedListNode<T> node, CSharpLinkedListNode<T> newNode)
        {
            newNode.next = node;
            newNode.prev = node.prev;
            node.prev.next = newNode;
            node.prev = newNode;
            count++;
        }
        //在链表开头插入节点（插入到head节点之前）
        public void AddFirst(CSharpLinkedListNode<T> node)
        {
            //空链表
            if (head == null)
            {
                InsertNodeToEmptyList(node);
            }
            else
            {
                InsertNodeBefore(head, node);
            }
        }

        //在链表开头插入值为value的节点（插入到head节点之前）
        public void AddFirst(T value)
        {
            AddFirst(new CSharpLinkedListNode<T>(value));
        }
        //在链表结尾插入节点
        public void AddLast(CSharpLinkedListNode<T> node)
        {
            //空链表
            if (head == null)
            {
                InsertNodeToEmptyList(node);
            }
            else
            {
                //链表的结尾就是链表head节点的前一个节点
                InsertNodeBefore(head, node);
            }
        }

        //在链表开头插入值为value的节点（插入到head节点之前）
        public void AddLast(T value)
        {
            AddLast(new CSharpLinkedListNode<T>(value));
        }
        //在某个节点前面插入一个值
        public void AddBefore(CSharpLinkedListNode<T> node, CSharpLinkedListNode<T> newNode)
        {
            InsertNodeBefore(node, node);

            //设置头节点
            if (node == head)
            {
                head = newNode;
            }
        }

        //在某个节点后面插入一个值
        public void AddAfter(CSharpLinkedListNode<T> node, CSharpLinkedListNode<T> newNode)
        {
            InsertNodeBefore(node.next, newNode);
        }
        public void Remove(CSharpLinkedListNode<T> node)
        {
            //链表仅有这个节点
            if (node.next == node)
            {
                head = null;
            }
            else
            {
                node.prev.next = node.next;
                node.next.prev = node.prev;

                //如果删除的是开头
                if (node == head)
                {
                    head = head.next;
                }
            }
            //干掉node发出来的两根指针，使它成为垃圾
            node.Invalidate();
            count--;
        }

        //传入值的重载形式
        public void Remove(T value)
        {
            var node = Find(value);
            if (node != null)
            {
                Remove(node);
            }
        }

        public void Print()
        {
            var current = First;
            do
            {
                Console.Write(current.item.ToString());
                current = current.next;
                if(current != First)
                {
                    Console.Write("->");
                }
            } while (current != First);

            Console.Write("\n");
        }

        public IEnumerator<T> GetEnumerator()
        {
            //传入链表本身
            return new CSharpLinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //实现CSharpLinkedListEnumerator，继承IEnumerator<T>接口
        public class CSharpLinkedListEnumerator : IEnumerator<T>
        {
            private T current;
            public T Current
            {
                get { return current; }
            }

            object IEnumerator.Current
            {
                get { return current; }
            }

            //数据源
            private readonly CSharpLinkedList<T> list;

            //当前节点
            private CSharpLinkedListNode<T> node;

            //当前位置
            private int _position = -1;

            //初始化
            public CSharpLinkedListEnumerator(CSharpLinkedList<T> data)
            {
                list = data;
                node = list.head;
            }

            //访问下一个
            public bool MoveNext()
            {
                _position++;
                if (_position == list.count)
                {
                    return false;
                }

                current = node.item;
                node = node.next;
                return true;
            }

            //不需要实现
            public void Reset()
            {
                throw new NotImplementedException();
            }

            //不需要代码，C#知道该如何做
            public void Dispose()
            {
            }
        }
    }

    /// <summary>
    /// 双向链表节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CSharpLinkedListNode<T>
    {
        //这两个字段是私有的，防止用户不通过公共方法来胡乱篡改
        internal CSharpLinkedListNode<T> next;
        internal CSharpLinkedListNode<T> prev;

        //节点的值
        public T item { get; set; }

        public CSharpLinkedListNode(T value)
        {
            item = value;
        }

        //令一个节点成为垃圾
        public void Invalidate()
        {
            this.prev = null;
            this.next = null;
        }

    }

}
