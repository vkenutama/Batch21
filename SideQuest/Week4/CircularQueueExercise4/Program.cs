using CircularQueueExercise4;

CircularQueue circularQueue = new CircularQueue();


// Test 1
circularQueue.SetCapacity(2);
circularQueue.SetOverwritePolicy(false);
circularQueue.Log(1);
circularQueue.Log(2);
circularQueue.Log(3);
circularQueue.Log(4);
circularQueue.Read();
circularQueue.Read();
circularQueue.Read();
circularQueue.Read();

circularQueue.SetCapacity(3);
circularQueue.SetOverwritePolicy(true);
circularQueue.Log(1);
circularQueue.Log(2);
circularQueue.Log(3);
circularQueue.Log(4);
circularQueue.Read();
circularQueue.Read();
circularQueue.Read();
circularQueue.Read();
