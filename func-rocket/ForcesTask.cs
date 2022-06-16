using System;
using System.Drawing;
using System.Linq;

namespace func_rocket
{
	public class ForcesTask
	{
		public static RocketForce GetThrustForce(double forceValue)
		{
			return r => {
				var newVector = new Vector(forceValue, 0);
				var x = Math.Cos(r.Direction) * newVector.X - newVector.Y * Math.Sin(r.Direction);
				var y = Math.Cos(r.Direction) * newVector.Y + newVector.X * Math.Sin(r.Direction);
				return new Vector(x, y);
			};
		}

		public static RocketForce ConvertGravityToForce(Gravity gravity, Size spaceSize)
		{
			return r => gravity(spaceSize, r.Location);
		}

		public static RocketForce Sum(params RocketForce[] forces)
		{
			return r =>
			{
				Vector vector = Vector.Zero;
				foreach (var force in forces)
					vector = vector + force(r);
				return vector;
			};
		}
	}
}