using System;

namespace func_rocket
{
    public class ControlTask
    {
        public static double MainAngle;

        public static Turn ControlRocket(Rocket rocket, Vector target)
        {
            var distanceVector = new Vector(target.X - rocket.Location.X, target.Y - rocket.Location.Y);
            if (Math.Abs(distanceVector.Angle - rocket.Direction) < 0.5
                || Math.Abs(distanceVector.Angle - rocket.Velocity.Angle) < 0.5)
                MainAngle = (distanceVector.Angle + distanceVector.Angle - rocket.Direction - rocket.Velocity.Angle) / 2;
            else
                MainAngle = distanceVector.Angle - rocket.Direction;
            if (MainAngle < 0)
                return Turn.Left;
            return MainAngle > 0 ? Turn.Right : Turn.None;
        }
    }
}