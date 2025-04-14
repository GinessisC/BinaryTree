namespace BinaryTrees;

public class BinaryTree<T> where T : IComparable<T>
{
	public Node<T> Root { get; private set; }
	public int Count => Root.Count;
	public int Depth => Root.Depth;

	public BinaryTree(IList<T> elements)
	{
		if (elements.HaveNoDuplicate() is false)
		{
			throw new ArgumentException("There are duplicate elements. Failed to construct bst.");
		}

		elements = elements.OrderBy(e => e).ToList();
		Root = ConstructBst(elements, 0, elements.Count - 1);
	}

	public void Balance()
	{
		if (IsBalanced())
		{
			return;
		}

		IList<T> elements = GetNodeValuesInOrder(Root);

		Root = ConstructBst(elements, 0, elements.Count - 1);
	}

	public bool Contains(int key)
	{
		return Contains(key, Root);
	}

	public void Remove(int valueKey)
	{
		RemoveFromTree(valueKey, Root);
	}

	public bool IsBalanced()
	{
		int leftHeight = Root.Left?.Depth ?? 0;
		int rightHeight = Root.Right?.Depth ?? 0;

		return Math.Abs(leftHeight - rightHeight) <= 1;
	}

	private Node<T> ConstructBst(IList<T> elements,
		int startIndex,
		int endIndex)
	{
		if (startIndex > endIndex)
		{
			return null;
		}

		int mid = (startIndex + endIndex) / 2;
		Node<T> node = new(elements[mid]);

		Node<T> left = ConstructBst(elements, startIndex, mid - 1);
		Node<T> right = ConstructBst(elements, mid + 1, endIndex);
		node.Left = left;
		node.Right = right;

		return node;
	}

	private IList<T> GetNodeValuesInOrder(Node<T>? node, IList<T>? values = null)
	{
		values ??= new List<T>();

		if (node == null)
		{
			return values;
		}

		GetNodeValuesInOrder(node.Left, values);
		values.Add(node.Value);
		GetNodeValuesInOrder(node.Right, values);

		return values;
	}

	public void Add(T value,
		int valueKey,
		Node<T>? currentNode = null)
	{
		currentNode ??= Root;

		if (valueKey < currentNode.Key)
		{
			AddToLeftChild(currentNode, value, valueKey);
		}
		else if (valueKey > currentNode.Key)
		{
			AddToRightChild(currentNode, value, valueKey);
		}
	}

	private void AddToRightChild(Node<T> node,
		T value,
		int valueKey)
	{
		node.Right ??= new Node<T>(value);

		if (node.Right != null)
		{
			Add(value, valueKey, node.Right);
		}
	}

	private void AddToLeftChild(Node<T> node,
		T value,
		int valueKey)
	{
		node.Left ??= new Node<T>(value);

		if (node.Left != null)
		{
			Add(value, valueKey, node.Left);
		}
	}

	private Node<T>? RemoveNode(Node<T> node)
	{
		if (node.Left == null || node.Right == null)
		{
			return node.Left ?? node.Right;
		}

		SwapNodeAndSuccessorValues(node);
		node.Right = RemoveFromTree(node.Key, node.Right);

		return node;
	}

	private Node<T>? RemoveFromTree(int valueKey, Node<T>? currentNode)
	{
		if (currentNode == null)
		{
			return currentNode;
		}

		return valueKey switch
		{
			var value when value < currentNode.Key =>
				currentNode.Left = RemoveFromTree(valueKey, currentNode.Left),
			var value when value > currentNode.Key =>
				currentNode.Right = RemoveFromTree(valueKey, currentNode.Right),
			var value when value == currentNode.Key =>
				RemoveNode(currentNode),
			_ => throw new ArgumentOutOfRangeException(nameof(valueKey), valueKey, null)
		};
	}

	private bool Contains(int key, Node<T>? node)
	{
		if (node == null)
		{
			return false;
		}

		if (node.Key == key)
		{
			return true;
		}

		Node<T>? next = key > node.Key ? node.Right : node.Left;

		return Contains(key, next);
	}

	private void SwapNodeAndSuccessorValues(Node<T> node)
	{
		Node<T>? successor = GetSuccessor(node);
		node.Value = successor!.Value; //In RemoveFromTree() successor is already checked on null
	}

	private static Node<T>? GetSuccessor(Node<T> curr)
	{
		curr = curr.Right;

		while (curr != null && curr.Left != null)
			curr = curr.Left;

		return curr;
	}
}