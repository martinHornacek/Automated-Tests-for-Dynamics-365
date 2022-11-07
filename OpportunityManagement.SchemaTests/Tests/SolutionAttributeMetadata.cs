using OpportunityManagement.SchemaTests.Logic;
using OpportunityManagement.SchemaTests.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpportunityManagement.SchemaTests.Tests
{
    public class SolutionAttributeMetadata
    {
        public static readonly string _solutionFilesDirectoryPath;

        static SolutionAttributeMetadata()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var vsSolutionDirectoryPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            _solutionFilesDirectoryPath = Path.Combine(vsSolutionDirectoryPath, "_SolutionFiles");
        }

        public static IEnumerable<object[]> All()
        {
            var solution = new Solution(_solutionFilesDirectoryPath);

            foreach (var item in solution.Entities.Values.SelectMany(x => x.Attributes))
            {
                yield return new object[] { item };
            }
        }

        public static IEnumerable<object[]> Lookups()
        {
            var solution = new Solution(_solutionFilesDirectoryPath);

            foreach (var item in solution.Entities.Values.SelectMany(x => x.Attributes).Where(y => y.Type == AttributeType.Lookup))
            {
                yield return new object[] { item };
            }
        }

        public static IEnumerable<object[]> NonLookups()
        {
            var solution = new Solution(_solutionFilesDirectoryPath);

            foreach (var item in solution.Entities.Values.SelectMany(x => x.Attributes).Where(y => y.Type != AttributeType.Lookup))
            {
                yield return new object[] { item };
            }
        }
    }
}