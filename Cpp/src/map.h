#pragma once
#include <stdint.h>
#include <vector>
#include <string>

struct vector2
{
public:
	int32_t x, y;

	bool operator==(const vector2& other)
	{
		return x == other.x && y == other.y;
	}
};

struct Map 
{

public:

	static const char cellBlocked = '#';
	static const char cellFree = '.';
	static const char cellInvalid = '?';
	static const char cellPath = '@';

	static const uint32_t kMapSize = 32;

	Map(const std::vector<char>& mapFileData, vector2 startPoint, vector2 endPoint)
	{
		uint32_t idx = 0;

		for (int32_t i = 0; i < mapFileData.size() && idx < kMapSize*kMapSize; ++i)
		{
			if (mapFileData[i] == cellBlocked || mapFileData[i] == cellFree)
			{
				m_MapData[idx%kMapSize][idx / kMapSize] = mapFileData[i];
				idx++;
			}
			// Skipping new line and eventual spaces at the end of line.
		}

		m_StartPoint = startPoint;
		m_EndPoint = endPoint;
	}

	void ComputePath() 
	{
		// TODO: Implement solution here
	}

	std::string SolutionToString()
	{
		std::string sol;
		sol.reserve(kMapSize*kMapSize);

		for (int32_t i = 0; i < kMapSize; ++i)
		{
			sol.append(m_MapData[i], kMapSize);
			sol.append("\n");
		}
		return sol;
	}

	void DisplayMap()
	{
		for (int32_t y = 0; y < kMapSize; ++y)
		{
			for (int32_t x = 0; x < kMapSize; ++x)
			{
				if (x == m_StartPoint.x && y == m_StartPoint.y)
					putchar(cellPath);
				else if (x == m_EndPoint.x && y == m_EndPoint.y)
					putchar(cellPath);
				else
				{
					switch (m_MapData[x][y])
					{
					case cellBlocked: putchar(cellBlocked); break;
					case cellFree: putchar(cellFree); break;
					default: putchar(cellInvalid); break;
					}
				}
			}
			putchar('\n');
		}
	}

private:

	char m_MapData[kMapSize][kMapSize];
	vector2 m_StartPoint;
	vector2 m_EndPoint;
};



