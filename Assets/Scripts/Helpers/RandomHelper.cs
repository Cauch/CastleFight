using System;

public static class RandomHelper {
    public static Random Random = new Random(new System.DateTime().Millisecond);
}
