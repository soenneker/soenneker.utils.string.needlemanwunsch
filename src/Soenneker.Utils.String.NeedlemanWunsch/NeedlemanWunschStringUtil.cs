using System;
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
        var matrix = new int[s1.Length + 1, s2.Length + 1];

        // Initialize the first row
        for (var i = 0; i <= s1.Length; i++)
        {
            matrix[i, 0] = i;
        }

        // Initialize the first column
        for (var j = 1; j <= s2.Length; j++)
        {
            matrix[0, j] = j;
        }

        // Fill in the matrix
        for (var i = 1; i <= s1.Length; i++)
        {
            for (var j = 1; j <= s2.Length; j++)
            {
                int cost = s1[i - 1] == s2[j - 1] ? 0 : 1;

                int deletion = matrix[i - 1, j] + 1;
                int insertion = matrix[i, j - 1] + 1;
                int substitution = matrix[i - 1, j - 1] + cost;

                matrix[i, j] = Math.Min(Math.Min(deletion, insertion), substitution);
            }
        }

        // The similarity score is the value in the bottom-right cell of the matrix
        return matrix[s1.Length, s2.Length];
    }
}