namespace BinaryTrees;

public class TreeElement<T>
{
	public int Key { get; }
	public T Value { get; }
	
	public TreeElement(T value)
	{
		Value = value;
	}
	public TreeElement(T value, int key)
	{
		Value = value;
		Key = key;
	}
}