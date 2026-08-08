using System;
using System.Buffers;
using System.Diagnostics.Contracts;

namespace Soenneker.Utils.String.NeedlemanWunsch;

/// <summary>
/// A utility library for comparing strings via the Needleman-Wunsch algorithm
/// </summary>
public static class NeedlemanWunschStringUtil
{
    /// <summary>
    /// Calculates the similarity percentage between two strings using the Needleman-Wunsch algorithm.
    /// </summary>
    /// <param name="s1">The first string.</param>
    /// <param name="s2">The second string.</param>
    /// <returns>The similarity percentage between the two strings.</returns>
    [Pure]
    public static double CalculateSimilarityPercentage(string s1, string s2)
    {
        if (s1 == s2)
            return 100;

        int similarityScore = CalculateSimilarity(s1, s2);

        double maxPossibleScore = Math.Max(s1.Length, s2.Length);
        double similarityPercentage = (1 - similarityScore / maxPossibleScore) * 100;

        return similarityPercentage;
    }

    /// <summary>
    /// Calculates the similarity score between two strings using the Needleman-Wunsch algorithm.
    /// </summary>
    /// <param name="s1">The first string.</param>
    /// <param name="s2">The second string.</param>
    /// <returns>The similarity score between the two strings.</returns>
    [Pure]
    public static int CalculateSimilarity(string s1, string s2)
    {
        ReadOnlySpan<char> rows = s1;
        ReadOnlySpan<char> columns = s2;

        if (columns.Length > rows.Length)
        {
            ReadOnlySpan<char> temp = rows;
            rows = columns;
            columns = temp;
        }

        int width = columns.Length + 1;
        int[]? rented = null;
        Span<int> storage = width <= 256
            ? stackalloc int[width * 2]
            : (rented = ArrayPool<int>.Shared.Rent(width * 2)).AsSpan(0, width * 2);

        try
        {
            Span<int> previous = storage[..width];
            Span<int> current = storage[width..];

            for (var j = 0; j < width; j++)
                previous[j] = j;

            for (var i = 1; i <= rows.Length; i++)
            {
                current[0] = i;

                for (var j = 1; j < width; j++)
                {
                    int cost = rows[i - 1] == columns[j - 1] ? 0 : 1;
                    current[j] = Math.Min(previous[j] + 1, Math.Min(current[j - 1] + 1, previous[j - 1] + cost));
                }

                Span<int> swap = previous;
                previous = current;
                current = swap;
            }

            return previous[^1];
        }
        finally
        {
            if (rented is not null)
                ArrayPool<int>.Shared.Return(rented);
        }
    }
}
