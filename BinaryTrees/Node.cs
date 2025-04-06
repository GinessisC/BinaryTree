using BinaryTrees.Interfaces;

namespace BinaryTrees;

public class Node<T> : INode<T>
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
	public virtual INode<T>? Left { get; set; }
	public virtual INode<T>? Right { get; set; }
	public virtual int Key => Value.GetHashCode();

	public Node(T value, Node<T>? left = null, Node<T>? right = null)
	{
		Left = left;
		Right = right;
		_value = value;
	}
}