using System;
using System.Text;

namespace hashes
{
	public class GhostsTask :
		IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
		IMagic
	{
		private readonly byte[] documentArray = { 1, 2, 4 };
		private readonly Cat cat = new Cat("musya", "siamese", new DateTime(2015, 2, 10));
		private readonly Robot robot = new Robot("hobot-2000", 100);
		private Vector vector = new Vector(1, 2);
		Segment segment = new Segment(new Vector(0, 0), new Vector(20, 20));

		public void DoMagic()
		{
			documentArray[0] = 100;
			cat.Rename("koshechka");
			Robot.BatteryCapacity--;
			vector = vector.Add(new Vector(10, 20));
			segment.End.Add(new Vector(40, 40));
		}

		Vector IFactory<Vector>.Create()
		{
			return vector;
		}

		Segment IFactory<Segment>.Create()
		{
			return segment;
		}

		Document IFactory<Document>.Create()
        {
			return new Document("emae", Encoding.Unicode, documentArray); 
		}

		Cat IFactory<Cat>.Create()
        {
			return cat;
        }

		Robot IFactory<Robot>.Create()
		{
			return robot;
		}
	}
}