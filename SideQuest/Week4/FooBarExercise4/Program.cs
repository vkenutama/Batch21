using FooBarExercise4;

FooBarGenerator fooBarGenerator = new FooBarGenerator();

fooBarGenerator.AddRule(3, "foo");
fooBarGenerator.AddRule(4, "baz");
fooBarGenerator.AddRule(5, "bar");
fooBarGenerator.AddRule(7, "jazz");
fooBarGenerator.AddRule(9, "huzz");

Console.WriteLine(fooBarGenerator.Evaluate(2));
Console.WriteLine(fooBarGenerator.Evaluate(3));
Console.WriteLine(fooBarGenerator.Evaluate(33));
Console.WriteLine(fooBarGenerator.Evaluate(3 * 4 * 5 * 7 * 9));

fooBarGenerator.GenerateSequence(1, 81);