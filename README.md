[![](https://img.shields.io/nuget/v/soenneker.utils.string.needlemanwunsch.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.string.needlemanwunsch/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.string.needlemanwunsch/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.utils.string.needlemanwunsch/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.utils.string.needlemanwunsch.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.string.needlemanwunsch/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.string.needlemanwunsch/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.utils.string.needlemanwunsch/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Utils.String.NeedlemanWunsch
Unit-cost global sequence alignment for string edit distance and normalized similarity.

## Installation

```bash
dotnet add package Soenneker.Utils.String.NeedlemanWunsch
```

## Usage

```csharp
using Soenneker.Utils.String.NeedlemanWunsch;

int distance = NeedlemanWunschStringUtil.CalculateSimilarity("kitten", "sitting");
double percentage = NeedlemanWunschStringUtil.CalculateSimilarityPercentage("kitten", "sitting");

// distance == 3
// percentage is approximately 57.14
```

Despite the method name, `CalculateSimilarity` returns a distance: `0` means the inputs are identical, and larger values mean more edits are required. Insertions, deletions, and substitutions each cost `1`; matching characters cost `0`.

The percentage method normalizes that distance against the longer input:

```text
(1 - distance / max(first.Length, second.Length)) × 100
```

Two empty strings return `100%`. An empty string compared with a non-empty string returns `0%`.

## Comparison rules and cost

- Comparison is case-sensitive.
- Characters are compared as UTF-16 code units, not Unicode scalar values or grapheme clusters.
- Whitespace and punctuation participate like any other character.
- Runtime is `O(m × n)` for input lengths `m` and `n`.
- Working memory is `O(min(m, n))`.

Call the static methods directly; no dependency-injection registration is required. Both arguments must be non-null. Normalize casing or Unicode representation before calling if your application requires those equivalences.

## Choosing the result

- Use `CalculateSimilarity` when an exact number of edits is useful for thresholds or ranking.
- Use `CalculateSimilarityPercentage` when inputs of different lengths need a common `0`–`100` scale.
