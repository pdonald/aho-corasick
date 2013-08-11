// Copyright (c) 2013 Pēteris Ņikiforovs
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;

namespace AhoCorasick
{
    public class Trie : Trie<string>
    {
        public void Add(string keyword)
        {
            Add(keyword, keyword);
        }

        public void Add(IEnumerable<string> keywords)
        {
            foreach (string keyword in keywords)
            {
                Add(keyword);
            }
        }
    }

    public class Trie<TValue> : Trie<char, TValue>
    {
    }

    public class Trie<TKeyword, TValue>
    {
        private readonly Node<TKeyword, TValue> root = new Node<TKeyword, TValue>();

        public void Add(IEnumerable<TKeyword> keyword, TValue value)
        {
            var node = root;

            foreach (TKeyword c in keyword)
            {
                var child = node[c];

                if (child == null)
                    child = node[c] = new Node<TKeyword, TValue>(c, node);

                node = child;
            }

            node.Values.Add(value);
        }

        public void Build()
        {
            var queue = new Queue<Node<TKeyword, TValue>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                foreach (var child in node)
                    queue.Enqueue(child);

                if (node == root)
                {
                    root.Fail = root;
                    continue;
                }

                var fail = node.Parent.Fail;

                while (fail[node.Keyword] == null && fail != root)
                    fail = fail.Fail;

                node.Fail = fail[node.Keyword] ?? root;
                if (node.Fail == node) 
                    node.Fail = root;
            }
        }

        public IEnumerable<TValue> Find(IEnumerable<TKeyword> text)
        {
            var node = root;

            foreach (TKeyword c in text)
            {
                while (node[c] == null && node != root)
                    node = node.Fail;

                node = node[c] ?? root;

                for (var t = node; t != root; t = t.Fail)
                {
                    foreach (TValue value in t.Values)
                        yield return value;
                }
            }
        }

        private class Node<TNodeKeyword, TNodeValue> : IEnumerable<Node<TNodeKeyword, TNodeValue>>
        {
            private readonly TNodeKeyword keyword;
            private readonly Node<TNodeKeyword, TNodeValue> parent;
            private readonly Dictionary<TNodeKeyword, Node<TNodeKeyword, TNodeValue>> children = new Dictionary<TNodeKeyword, Node<TNodeKeyword, TNodeValue>>();
            private readonly List<TNodeValue> values = new List<TNodeValue>();

            public Node()
            {
            }

            public Node(TNodeKeyword keyword, Node<TNodeKeyword, TNodeValue> parent)
            {
                this.keyword = keyword;
                this.parent = parent;
            }

            public TNodeKeyword Keyword
            {
                get { return keyword; }
            }

            public Node<TNodeKeyword, TNodeValue> Parent
            {
                get { return parent; }
            }

            public Node<TNodeKeyword, TNodeValue> Fail
            {
                get;
                set;
            }

            public Node<TNodeKeyword, TNodeValue> this[TNodeKeyword c]
            {
                get { return children.ContainsKey(c) ? children[c] : null; }
                set { children[c] = value; }
            }

            public List<TNodeValue> Values
            {
                get { return values; }
            }

            public IEnumerator<Node<TNodeKeyword, TNodeValue>> GetEnumerator()
            {
                return children.Values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override string ToString()
            {
                return Keyword.ToString();
            }
        }
    }
}
