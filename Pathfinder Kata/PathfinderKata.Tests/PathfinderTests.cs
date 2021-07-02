using NUnit.Framework;
using UnityEngine;

namespace PathfinderKata.Tests
{
	public sealed class PathfinderTests
	{
		[Test]
		public void CheckStartTargetAndPathLength()
		{
			var path = PathFinderCanYouReachTheExit.PathFinder(Pathfinder.Map);
			Assert.That(path![0], Is.EqualTo((8, 1)));
			Assert.That(path[^1], Is.EqualTo((2, 3)));
			Assert.That(path, Has.Count!.GreaterThanOrEqualTo(13));
		}
	}
}