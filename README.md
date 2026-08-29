[![](https://img.shields.io/nuget/v/soenneker.utils.string.needlemanwunsch.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.string.needlemanwunsch/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.string.needlemanwunsch/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.utils.string.needlemanwunsch/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.utils.string.needlemanwunsch.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.string.needlemanwunsch/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.string.needlemanwunsch/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.utils.string.needlemanwunsch/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Utils.String.NeedlemanWunsch
A utility library for comparing strings via the Needleman-Wunsch algorithm.

## Installation

```bash
dotnet add package Soenneker.Utils.String.NeedlemanWunsch
```

## Quick start

```csharp
using Soenneker.Utils.String.NeedlemanWunsch;
```

Call the static `NeedlemanWunschStringUtil` methods directly; no dependency-injection registration is required.

## Common operations

- `CalculateSimilarityPercentage()` - Calculates the similarity percentage between two strings using the Needleman-Wunsch algorithm. Returns the similarity percentage between the two strings.
- `CalculateSimilarity()` - Calculates the similarity score between two strings using the Needleman-Wunsch algorithm. Returns the similarity score between the two strings.
