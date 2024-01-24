using Options;
using static Options.Prelude;

Option<int> i = None;
Option<int> j = Some(5);
Option<string> k = Some("test");
Option<string> l = None;

Console.WriteLine($"{i}, {j}, {k}, {l}");
