using Options;
using static Options.Prelude;

Option<int> i = None;
Option<int> j = Some(5);
Option<string> k = Some("test");

Console.WriteLine($"{i}, {j}, {k}");
