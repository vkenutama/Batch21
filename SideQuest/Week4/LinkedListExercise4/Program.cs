using LinkedListExercise4;

CustomLinkedList ccl = new CustomLinkedList();
ccl.SetSorting((a, b) => a - b);
ccl.AddFilter(val => true);
ccl.Append(5);
ccl.Append(2);
ccl.Append(8);
ccl.Print();
ccl.PrintReverse();