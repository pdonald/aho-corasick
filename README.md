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

// find words
foreach (string word in trie.Find(text)) {
  Console.WriteLine(word);
}
```

You can associate other data with the words (like an ID or line number).

```csharp
AhoCorasick.Trie<int> trie = new AhoCorasick.Trie<int>();

// add words
trie.Add("hello", 123);
trie.Add("world", 456);

// build search tree
trie.Build();

// retrieve IDs
foreach (int id in trie.Find(text)) {
  Console.WriteLine(id);
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