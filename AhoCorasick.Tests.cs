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

using System;
using System.Linq;

using NUnit.Framework;

namespace AhoCorasick
{
    public class Tests
    {
        [Test]
        public void HelloWorld()
        {
            string text = "hello and welcome to this beautiful world!";

            var trie = new AhoCorasick.Trie();
            trie.Add("hello");
            trie.Add("world");
            trie.Build();

            var matches = trie.Find(text).ToArray();

            Assert.AreEqual(2, matches.Length);
            Assert.AreEqual(Tuple.Create("hello", 4), matches[0]);
            Assert.AreEqual(Tuple.Create("world", 40), matches[1]);
        }

        [Test]
        public void Contains()
        {
            string text = "hello and welcome to this beautiful world!";

            var trie = new AhoCorasick.Trie();
            trie.Add("hello");
            trie.Add("world");
            trie.Build();

            Assert.IsTrue(trie.Find(text).Any());
        }

        [Test]
        public void Ids()
        {
            string text = "hello and welcome to this beautiful world!";

            var trie = new AhoCorasick.Trie<int>();
            trie.Add("hello", 123);
            trie.Add("world", 456);

            trie.Build();

            var matches = trie.Find(text).ToArray();

            Assert.AreEqual(2, matches.Length);
            Assert.AreEqual(Tuple.Create(123, 4), matches[0]);
            Assert.AreEqual(Tuple.Create(456, 40), matches[1]);
        }

        [Test]
        public void WordsAndIds()
        {
            string text = "hello and welcome to this beautiful world!";

            var trie = new AhoCorasick.Trie<Tuple<string, int>>();

            trie.Add("hello", Tuple.Create("hello", 123));
            trie.Add("world", Tuple.Create("world", 456));

            trie.Build();

            var matches = trie.Find(text).ToArray();

            Assert.AreEqual(2, matches.Length);
            Assert.AreEqual(Tuple.Create(Tuple.Create("hello", 123), 4), matches[0]);
            Assert.AreEqual(Tuple.Create(Tuple.Create("world", 456), 40), matches[1]);
        }

        [Test]
        public void Words()
        {
            string[] text = "one two three four".Split(' ');

            var trie = new AhoCorasick.Trie<string, bool>();
            trie.Add(new[] { "three", "four" }, true);
            trie.Build();

            Assert.IsTrue(trie.Find(text).Any());
        }
    }
}
