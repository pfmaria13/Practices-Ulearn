using System;
using System.Collections.Generic;
using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism
{
    public class LevenshteinCalculator
    {
        public static double GetDistance(DocumentTokens firstDoc, DocumentTokens secondDoc)
        {
            var opt = new double[firstDoc.Count + 1, secondDoc.Count + 1];
            for (var i = 0; i <= firstDoc.Count; i++)
                opt[i, 0] = i;
            for (var i = 0; i <= secondDoc.Count; i++)
                opt[0, i] = i;
            for (var i = 1; i <= firstDoc.Count; i++)
                for (var j = 1; j <= secondDoc.Count; j++)
                {
                    var distance = firstDoc[i - 1] == secondDoc[j - 1] ? 0 :
                        TokenDistanceCalculator.GetTokenDistance(firstDoc[i - 1], secondDoc[j - 1]);
                    opt[i, j] = Math.Min(Math.Min(opt[i - 1, j] + 1, opt[i, j - 1] + 1),
                        opt[i - 1, j - 1] + distance);
                }
            return opt[firstDoc.Count, secondDoc.Count];
        }

        public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
        {
            var result = new List<ComparisonResult>();
            for (var i = 0; i < documents.Count; i++)
                for (var j = i + 1; j < documents.Count; j++)
                    result.Add(new ComparisonResult(documents[i], documents[j],
                        GetDistance(documents[i], documents[j])));
            return result;
        }
    }
}