namespace BinaryTrees;

public static class ListExtensions
{
	public static bool HaveNoDuplicate<T>(this IList<T> list)
	{
		var listWithoutDuplicate = list.Distinct().ToList();
		
		return (listWithoutDuplicate.Count == list.Count);
	}
}