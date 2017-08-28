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

            AhoCorasick.Trie trie = new AhoCorasick.Trie();
            trie.Add("hello");
            trie.Add("world");
            trie.Build();

            var matches = trie.Find(text).ToArray();

            Assert.AreEqual(2, matches.Length);
            Assert.AreEqual(new Tuple<string, int>("hello", 4), (Tuple<string, int>)matches[0]);
            Assert.AreEqual(new Tuple<string, int>("world", 40), (Tuple<string, int>)matches[1]);
        }

        [Test]
        public void Contains()
        {
            string text = "hello and welcome to this beautiful world!";

            AhoCorasick.Trie trie = new AhoCorasick.Trie();
            trie.Add("hello");
            trie.Add("world");
            trie.Build();

            Assert.IsTrue(trie.Find(text).Any());
        }

        [Test]
        public void Ids()
        {
            string text = "hello and welcome to this beautiful world!";

            AhoCorasick.Trie<int> trie = new AhoCorasick.Trie<int>();
            trie.Add("hello", 123);
            trie.Add("world", 456);

            trie.Build();

            var matches = trie.Find(text).ToArray();

            Assert.AreEqual(2, matches.Length);
            Assert.AreEqual(new Tuple<int, int>(123, 4), matches[0]);
            Assert.AreEqual(new Tuple<int, int>(456, 40), matches[1]);
        }

        [Test]
        public void WordsAndIds()
        {
            string text = "hello and welcome to this beautiful world!";

            AhoCorasick.Trie<Tuple<string, int>> trie = new AhoCorasick.Trie<Tuple<string, int>>();

            trie.Add("hello", new Tuple<string, int>("hello", 123));
            trie.Add("world", new Tuple<string, int>("world", 456));

            trie.Build();

            var matches = trie.Find(text).ToArray();

            Assert.AreEqual(2, matches.Length);
            Assert.AreEqual(new Tuple<Tuple<string, int>, int>(new Tuple<string, int>("hello", 123), 4), matches[0]);
            Assert.AreEqual(new Tuple<Tuple<string, int>, int>(new Tuple<string, int>("world", 456), 40), matches[1]);
        }

        [Test]
        public void Words()
        {
            string[] text = "one two three four".Split(' ');
            
            AhoCorasick.Trie<string, bool> trie = new AhoCorasick.Trie<string, bool>();
            trie.Add(new[] { "three", "four" }, true);
            trie.Build();

            Assert.IsTrue(trie.Find(text).Any());
        }
    }
}
