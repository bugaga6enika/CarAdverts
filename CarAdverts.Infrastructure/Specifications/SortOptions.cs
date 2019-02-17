using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CarAdverts.Infrastructure.Specifications
{
    public class SortOptions
    {
        public IReadOnlyCollection<string> FieldToSortBy { get; }
        public string Direction { get; } = "";

        public SortOptions(string sortOptions)
        {
            var sortOptionsDirectionArr = sortOptions?.Split(' ');

            if (sortOptionsDirectionArr?.Length > 0)
            {
                FieldToSortBy = new ReadOnlyCollection<string>(sortOptionsDirectionArr[0].Split(',').ToList());

                if (sortOptionsDirectionArr.Length == 2 && sortOptionsDirectionArr[1] == "desc")
                {
                    Direction = "descending";
                }
            }
        }

        public bool IsValid => FieldToSortBy?.Any() ?? false;

        public override string ToString()
        {
            return $"{FieldToSortBy.Aggregate("", (acc, current) => acc + current + " " + Direction + ",").TrimEnd(',')}";
        }
    }
}
