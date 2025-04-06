using BinaryTrees;
using BinaryTrees.Interfaces;

namespace BinaryTree.Tests;

public class BinaryTreeTest
{
	[Fact]
	public void GetCount_Bst_CorrectCount()
	{
		BinaryTree<int> bt = new([ 1, 2, 3, 4 ]);
		Assert.True(bt.Count == 4);
	}
	[Fact]
	public void GetDepth_Bst_CorrectDepth()
	{
		BinaryTree<int> bt = new([ 1, 2, 3, 4 ]);
		Assert.True(bt.Depth == 3);
	}
	[Fact]
	public void Create_BSTWithDuplicateNodes_ThrowsException()
	{
		var elements = new List<int> { 1, 2, 2 };
		Assert.Throws<ArgumentException>(() => new BinaryTree<int>(elements));
	}
	[Fact]
	public void Add_Node_ReturnsBalancedBST()
	{
		BinaryTree<int> bt = new([ 1, 2, 3, 4 ]);
		bt.Add(10);
		Assert.True(IsBalanced(bt.Root));
	}
	[Fact]
	public void Remove_Node_ReturnsBalancedBST()
	{
		BinaryTree<int> bt = new([ 1, 2, 3, 4 ]);
		bt.Remove(4);
		Assert.True(IsBalanced(bt.Root));
		Assert.True(bt.Contains(4) is false);
	}
	[Fact]
	public void Contains_ExistingNode_ReturnsTrue()
	{
		BinaryTree<int> bt = new([ 1, 2, 3, 4 ]);
		Assert.True(bt.Contains(4));
	}
	[Fact]
	public void Contains_NonExistentNode_ReturnsFalse()
	{
		BinaryTree<int> bt = new([ 1, 2, 3, 4 ]);
		Assert.True(bt.Contains(55) is false);
	}
	[Fact]
	public void ValidateBST_BST_ReturnsTrue()
	{
		BinaryTree<int> bt = new([1, 2, 3, 4]);
		Assert.True(IsBst(bt.Root));
	}
	
	private bool IsBalanced(INode<int> root) 
	{
		int leftHeight = CalculateHeight(root.Left);
		int rightHeight = CalculateHeight(root.Right);
		
		return (leftHeight - rightHeight > 1 is false) || (rightHeight - leftHeight > 1 is false); 
	}

	private int CalculateHeight(INode<int>? root)
	{
		if (root == null)
		{
			return 0;
		}
		var left  = root.Left;
		var right = root.Right;
		
		return 1 + Math.Max(CalculateHeight(left), CalculateHeight(right));
	}
	
	private bool IsBst(INode<int>? root, int min = int.MinValue, int max = int.MaxValue)
	{
		if (root == null)
		{
			return true;
		}

		if (root.Value <= min || root.Value >= max)
		{
			return false;
		}
		
		var leftIsBst = IsBst(root.Left, min, root.Value);
		var rightIsBst = IsBst(root.Right, root.Value, max);
		
		return leftIsBst && rightIsBst;
	}
}