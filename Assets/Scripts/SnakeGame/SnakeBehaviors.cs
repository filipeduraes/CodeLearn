using System;

namespace CodeLearn.SnakeGame
{
    [Flags]
    public enum SnakeBehaviors
    {
        EatApple = 1,
        Grow = 2,
        Collide = 4,
        RandomizeApple = 8,
    }
}