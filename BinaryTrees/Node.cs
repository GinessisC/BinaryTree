namespace BinaryTrees;

public class Node<T>
{
	private T _value;

	public virtual T Value
	{
		get => _value;
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			_value = value;
		}
	}

	public Node<T>? Left { get; set; }
	public Node<T>? Right { get; set; }

	public virtual int Count => GetCount(this);

	public virtual int Depth => GetDepth(this);

	public virtual int Key => Value!.GetHashCode(); // ! is used bc we cant call .Key before constructor initialize Value

	public Node(T value,
		Node<T>? left = null,
		Node<T>? right = null)
	{
		Left = left;
		Right = right;
		_value = value;
	}

	private int GetCount(Node<T>? node)
	{
		if (node == null)
		{
			return 0;
		}

		int countOfLeftSubtree = GetCount(node.Left);
		int countOfRightSubtree = GetCount(node.Right);

		return countOfLeftSubtree + countOfRightSubtree + 1;
	}

	private int GetDepth(Node<T>? node)
	{
		if (node == null)
		{
			return 0;
		}

		return 1 + Math.Max(GetDepth(node.Left), GetDepth(node.Right));
	}
}