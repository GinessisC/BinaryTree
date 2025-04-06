namespace BinaryTrees.Interfaces;

public interface INode<T>
{
	T Value { get; set; }
	INode<T>? Left { get; set; } 
	INode<T>? Right { get; set; }
	int Key { get; }
}