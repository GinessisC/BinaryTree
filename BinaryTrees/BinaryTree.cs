using BinaryTrees.Interfaces;

namespace BinaryTrees;

public class BinaryTree<T>
{
	public INode<T> Root { get; }
	public int Count => GetCount(Root);
	public int Depth => GetDepth(Root);
	private readonly IList<T> _elements;

	
	public BinaryTree(IList<T> elements)
	{
		if (elements.HaveNoDuplicate() is false)
		{
			throw new ArgumentException("There are duplicate elements. Failed to construct bst.");
		}
		_elements = elements.OrderBy(e => e.GetHashCode()).ToList();
		Root = ConstructBst(0, _elements.Count - 1);
	}
	private Node<T> ConstructBst(int startIndex, int endIndex)
	{
		if (startIndex > endIndex)
		{
			return null;
		}
		int mid = (startIndex + endIndex) / 2;
		Node<T> node = new(_elements[mid]);
		
		var left = ConstructBst(startIndex, mid - 1);
		var right = ConstructBst(mid + 1, endIndex);
		node.Left = left;
		node.Right = right;

		return node;
	}
	
	public void Add(T value, int elementKey)
	{
		AddToTree(value, elementKey);
		_elements.Add(value);
	}
	public void Add(T value)
	{
		AddToTree(value, value.GetHashCode());
		_elements.Add(value);
	}
	private void AddToTree(T value, int valueKey, INode<T>? currentNode = null)
	{
		currentNode ??= Root;

		if (valueKey < currentNode.Key)
		{
			if (currentNode.Left == null)
			{
				currentNode.Left = new Node<T>(value);
			}
			else
			{
				AddToTree(value, valueKey, currentNode.Left);
			}
		}
		else if (valueKey > currentNode.Key)
		{
			if (currentNode.Right == null)
			{
				currentNode.Right = new Node<T>(value);
			}
			else
			{
				AddToTree(value, valueKey, currentNode.Right);
			}
		}
	}
	public void Remove(T value, int valueKey)
	{
		bool isRemoved = _elements.Remove(value);

		if (isRemoved)
		{
			RemoveFromTree(valueKey, Root);
		}
	}
	public void Remove(T value)
	{
		bool isRemoved = _elements.Remove(value);

		if (isRemoved)
		{
			RemoveFromTree(value.GetHashCode(), Root);
		}
	}
	private INode<T> RemoveFromTree(int valueKey, INode<T>? currentNode)
	{
		if (currentNode == null)
		{
			return currentNode;
		}
		
		if (valueKey > currentNode.Key)
		{
			currentNode.Right = RemoveFromTree(valueKey, currentNode.Right);
		}

		else if (valueKey < currentNode.Key)
		{
			currentNode.Left = RemoveFromTree(valueKey, currentNode.Left);
		}
		else
		{
			if (currentNode.Left == null)
			{
				return currentNode.Right;
			}

			if (currentNode.Right == null)
			{
				return currentNode.Left;
			}

			SwapNodeAndSuccessorValues(currentNode);
			currentNode.Right = RemoveFromTree(valueKey, currentNode.Right);
		}
		return currentNode;
	}

	
	public bool Contains(T value)
	{
		return _elements.Contains(value);
	}
	private int GetCount(INode<T>? node)
	{
		if (node == null)
		{
			return 0;
		}
		int countOfLeftSubtree = GetCount(node.Left);
		int countOfRightSubtree = GetCount(node.Right);
		
		return countOfLeftSubtree + countOfRightSubtree + 1;
	}

	private int GetDepth(INode<T>? node)
	{
		if (node == null)
		{
			return 0;
		}
		
		return 1 + Math.Max(GetDepth(node.Left), GetDepth(node.Right));
	}
	private void SwapNodeAndSuccessorValues(INode<T> node)
	{
		var successor = GetSuccessor(node);
		node.Value = successor.Value;
	}
	private static INode<T> GetSuccessor(INode<T> curr) 
	{
		curr = curr.Right;
		while (curr != null && curr.Left != null) 
		{
			curr = curr.Left;
		}
		return curr;
	}
}