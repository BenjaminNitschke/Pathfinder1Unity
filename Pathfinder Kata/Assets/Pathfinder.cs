using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class Pathfinder : MonoBehaviour
{
	void Update()
	{
		var path = GetPath(Map);
		if (Time.time / 10 < path!.Count)
			transform.position =
				new Vector2(1 + path[(int)(Time.time / 10)].x, 9 - path[(int)(Time.time / 10)].x);
	}

	public const string Map = @"
.......W..
.......WS.
.......W..
..T....W..
.......W..
.......W..
.......W..
..........
..........
..........";

	public List<Vector2> GetPath(string map) =>
		PathFinderCanYouReachTheExit.PathFinder(map).Select(xy => new Vector2(xy.Item1, xy.Item2)).
			ToList();
}