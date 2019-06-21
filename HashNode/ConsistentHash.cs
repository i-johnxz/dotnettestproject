using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HashNode
{
    /// <summary>
    /// 1. 采用虚拟节点方式 2.节点总数可以自定义 3.每个物理节点的虚拟节点数可以自定义
    /// </summary>
    public class ConsistentHash
    {
        // 哈希环的虚拟节点信息
        public class VirtualNode
        {
            public string VirtualNodeName { get; set; }

            public NodeInfo Node { get; set; }
        }
        
        // 添加元素 删除元素时候的锁，来保证线程安全，或者采用读写锁也可以
        private readonly object objLock = new object();

        // 虚拟环节点的总数量，默认未100
        private int ringNodeCount;

        // 每个物理节点对应的虚拟节点数量
        private int virtualNodeNumber;

        // 哈希环，这里用数组来存储
        public VirtualNode[] nodes = null;
        public ConsistentHash(int _ringNodeCount, int _virtualNodeNumber)
        {
            if (_ringNodeCount <= 0 || _virtualNodeNumber <= 0)
            {
                throw new Exception("_ringNodeCount和_virtualNodeNumber 必须大于0");
            }

            this.ringNodeCount = _ringNodeCount;
            this.virtualNodeNumber = _virtualNodeNumber;
            nodes = new VirtualNode[_ringNodeCount];
        }

        //根据一致性哈希key 获取node信息,查找操作请业务方自行处理超时问题，因为多线程环境下，环的node可能全被清除
        public NodeInfo GetNode(string key)
        {
            var ringStartIndex = Math.Abs(GetKeyHashCode(key) % ringNodeCount);
            var vNode = FindNodeFromIndex(ringStartIndex);
            return vNode == null ? null : vNode.Node;
        }

        //虚拟环添加一个物理节点
        public void AddNode(NodeInfo newNode)
        {
            var nodeName = newNode.NodeName;
            int virtualNodeIndex = 0;
            lock (objLock)
            {
                while (virtualNodeIndex < virtualNodeNumber)
                {
                    var vNodeName = $"{nodeName}#{virtualNodeIndex}";
                    var findStartIndex = Math.Abs(GetKeyHashCode(vNodeName) % ringNodeCount);
                    var emptyIndex = FindEmptyNodeFromIndex(findStartIndex);
                    if (emptyIndex < 0)
                    {
                        // 已经超出设置的最大节点数
                        break;
                    }
                    nodes[emptyIndex] = new VirtualNode()
                    {
                        VirtualNodeName = vNodeName,
                        Node = newNode
                    };
                    virtualNodeIndex++;
                }
            }
        }


        //删除一个虚拟节点
        public void RemoveNode(NodeInfo node)
        {
            var nodeName = node.NodeName;
            int virtualNodeIndex = 0;
            List<string> lstRemoveNodeName = new List<string>();
            while (virtualNodeIndex < virtualNodeNumber)
            {
                lstRemoveNodeName.Add($"{nodeName}#{virtualNodeIndex}");
                virtualNodeIndex++;
            }
            // 从索引为0的位置循环一遍，把所有的虚拟节点都删除
            int startFindIndex = 0;
            lock (objLock)
            {
                while (startFindIndex < nodes.Length)
                {
                    if (nodes[startFindIndex] != null &&
                        lstRemoveNodeName.Contains(nodes[startFindIndex].VirtualNodeName))
                    {
                        nodes[startFindIndex] = null;
                    }

                    startFindIndex++;
                }
            }
        }


        protected virtual int GetKeyHashCode(string key)
        {
            var sh = new SHA1Managed();
            byte[] data = sh.ComputeHash(Encoding.Unicode.GetBytes(key));
            return BitConverter.ToInt32(data, 0);
        }


        #region 私有方法
        //从虚拟环的某个位置查找第一个node
        private VirtualNode FindNodeFromIndex(int startIndex)
        {
            if (nodes == null || nodes.Length < 0)
            {
                return null;
            }

            VirtualNode node = null;
            while (node == null)
            {
                startIndex = GetNextIndex(startIndex);
                node = nodes[startIndex];
            }

            return node;
        }

        //从虚拟环的某个位置开始查找空位置
        private int FindEmptyNodeFromIndex(int startIndex)
        {
            while (true)
            {
                if (nodes[startIndex] == null)
                {
                    return startIndex;
                }

                var nextIndex = GetNextIndex(startIndex);
                //如果索引回到原地，说明找了一圈，虚拟环节点已经满了，不会添加
                if (nextIndex == startIndex)
                {
                    return -1;
                }

                startIndex = nextIndex;
            }
        }

        //获取一个位置的下一个位置索引
        private int GetNextIndex(int preIndex)
        {
            int nextIndex = 0;
            if (preIndex != nodes.Length - 1)
            {
                nextIndex = preIndex + 1;
            }

            return nextIndex;
        }

        #endregion
    }
}