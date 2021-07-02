using System;
using System.Collections.Generic;
using System.Linq;

public class PathFinderCanYouReachTheExit
{
	public static List<(int, int)> PathFinder(string maze) =>
		new PathFinderCanYouReachTheExit(
			maze.Split('\n', '\r').Where(line => line.Length > 0).ToArray()).Find();

	private PathFinderCanYouReachTheExit(string[] lines)
	{
		width = lines[0].Length;
		height = lines.Length;
		map = new bool[height, width];
		targetX = width - 1;
		targetY = height - 1;
		for (var y = 0; y < height; y++)
		for (var x = 0; x < width; x++)
			if (lines[y][x] == 'S')
			{
				map[y, x] = true;
				validMoves = new List<(int, int)> { (x, y) };
				finalMoves = new List<(int, int)> { (x, y) };
			}
			else if (lines[y][x] == 'T')
			{
				map[y, x] = true;
				targetX = x;
				targetY = y;
			}
			else
				map[y, x] = lines[y][x] == '.';
		Console.WriteLine("PathFinderCanYouReachTheExit width=" + width + ", height=" + height +
			", start=" + validMoves[0].Item1 + "," + validMoves[0].Item2 + ", target=" + targetX + "," +
			targetY + "\nPathFinderCanYouReachTheExit: map=\n" + string.Join("\n", lines));
	}

	private readonly int width;
	private readonly int height;
	private readonly bool[,] map;
	private readonly int targetX;
	private readonly int targetY;

	private List<(int, int)> Find()
	{
		if (targetX == 0 && targetY == 0)
			return new List<(int, int)> { (targetX, targetY) }; //ncrunch: no coverage
		do
		{
			if (FindMoreValidMoves())
				return finalMoves;
		} while (validMoves.Count > 0);
		return new List<(int, int)>();
	}

	private readonly List<(int, int)> validMoves = new List<(int, int)> { (0, 0) };
	private readonly List<(int, int)> finalMoves = new List<(int, int)> { (0, 0) };

	private bool FindMoreValidMoves()
	{
		var lastIndex = validMoves.Count - 1;
		var x = validMoves[lastIndex].Item1;
		var y = validMoves[lastIndex].Item2;
		finalMoves.Add((x, y));
		validMoves.RemoveAt(lastIndex);
		map[y, x] = false;
		foreach (var move in Directions)
			if (CanAddNewValidMove(x + move.Item1, y + move.Item2))
			{
				finalMoves.Add((x + move.Item1, y + move.Item2));
				return true;
			}
		return false;
	}

	private bool CanAddNewValidMove(int newX, int newY)
	{
		if (newX == targetX && newY == targetY)
			return true;
		// ReSharper disable once ComplexConditionExpression
		if (newX >= 0 && newX < width && newY >= 0 && newY < height && map[newY, newX])
			validMoves.Add((newX, newY));
		return false;
	}

	private static readonly (int, int)[] Directions = { (1, 0), (-1, 0), (0, 1), (0, -1) };
}