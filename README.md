Aho–Corasick string matching algorithm in C#
============================================

The [Aho–Corasick string matching algorithm](http://en.wikipedia.org/wiki/Aho%E2%80%93Corasick_string_matching_algorithm) is a string searching algorithm. It's useful in NLP when you have a dictionary with words and you need to tell if a text contains any of the words.

```csharp
AhoCorasick.Trie trie = new AhoCorasick.Trie();

// add words
trie.Add("hello");
trie.Add("world");

// build search tree
trie.Build();

string text = "hello and welcome to this beautiful world!";

// find words and wordEndIndices
foreach (Tuple<string, int> tuple in trie.Find(text)) {
  var word = tuple.Item1;
  var wordEndIndex = tuple.Item2;
  Console.WriteLine("{0}, {1}", word, wordEndIndex);
}
```

You could associate other data with the words (like an ID or line number).

```csharp
AhoCorasick.Trie<int> trie = new AhoCorasick.Trie<int>();

// add words
trie.Add("hello", 123);
trie.Add("world", 456);

// build search tree
trie.Build();

// retrieve IDs and wordEndIndices
foreach (Tuple<int, int> tuple in trie.Find(text))
{
  var id = tuple.Item1;
  var wordEndIndex = tuple.Item2;
  Console.WriteLine("{0}, {1}", id, wordEndIndex);
}
```

You also could retrieve matched strings and associated data (like an ID or line number)

```csharp
AhoCorasick.Trie<Tuple<string, int>> trie = new AhoCorasick.Trie<int>();

// add words
trie.Add("hello", new Tuple<string, int>("hello", 123));
trie.Add("world", new Tuple<string, int>("world", 456));

// build search tree
trie.Build();

// find words, IDs and wordEndIndices
foreach (Tuple<Tuple<string, int>, int> tuple in trie.Find(text))
{
  var word = tuple.Item1.Item1;
  var id = tuple.Item1.Item2;
  var wordEndIndex = tuple.Item2;
  Console.WriteLine("{0}, {1}, {2}", word, id, wordEndIndex);
}
```

Use `IEnumerable<T>.Any()` to check if the text contains a match without retrieving all of them.

If you want to match whole words, you can use `Trie<string, T>`.

```csharp
string[] text = "hello world i say to you".Split(' ');
            
AhoCorasick.Trie<string, bool> trie = new AhoCorasick.Trie<string, bool>();
trie.Add("hello world".Split(' '), true);
trie.Build();
bool containsHelloWorld = trie.Find(text).Any();
```

License
-------

MIT