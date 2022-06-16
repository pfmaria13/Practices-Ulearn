using System;
using System.Collections.Generic;

namespace func_rocket
{
    public class LevelsTask
    {
        static readonly Physics standardPhysics = new Physics();

        public static IEnumerable<Level> CreateLevels()
        {
            yield return LevelZero();
            yield return LevelHeavy();
            yield return LevelUp();
            foreach (var level in caseWithHoles())
                yield return level;
        }

        static readonly Vector vectorForTargetCoordinates = new Vector(600, 200);
        static readonly Vector vectorGravity = new Vector(200, 500);
        private static IEnumerable<Level> caseWithHoles()
        {
            var vectorEnd = new Vector(700, 500);
            var rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
            Gravity whiteGravity = (size, v) =>
            {
                var d = (vectorEnd - v).Length;
                return (vectorEnd - v).Normalize() * (-140) * d / (d * d + 1);
            };
            var blackhole = (vectorEnd + rocket.Location) * 0.5;
            Gravity blackGravity = (size, v) =>
            {
                var d = (blackhole - v).Length;
                return (blackhole - v).Normalize() * 300 * d / (d * d + 1);
            };
            Gravity blackAndWhite = (size, v) => (whiteGravity(size, v) + blackGravity(size, v)) * 0.5;
            yield return new Level("WhiteHole", rocket, vectorEnd, whiteGravity, standardPhysics);
            yield return new Level("BlackHole", rocket, vectorEnd, blackGravity, standardPhysics);
            yield return new Level("BlackAndWhite", rocket, vectorEnd, blackAndWhite, standardPhysics);
        }

        private static Level LevelZero() => new Level("Zero",
                new Rocket(vectorGravity, Vector.Zero, (-0.5) * Math.PI),
                vectorForTargetCoordinates,
                (size, vector) => Vector.Zero, standardPhysics);

        private static Level LevelHeavy() => new Level("Heavy",
                new Rocket(vectorGravity, Vector.Zero, -0.5 * Math.PI),
                new Vector(700, 500),
                (size, vector) => new Vector(0, 0.9),
                standardPhysics);

        private static Level LevelUp() => new Level("Up",
                new Rocket(vectorGravity, Vector.Zero, -0.5 * Math.PI),
                new Vector(700, 500),
                (size, vector) => new Vector(0, -300 / (300 + size.Height - vector.Y)),
                standardPhysics);
    }
}
