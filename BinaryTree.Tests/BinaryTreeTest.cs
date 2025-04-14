using BinaryTrees;

namespace BinaryTree.Tests;

public class BinaryTreeTest
{
	[Fact]
	public void GetCount_Bst_CorrectCount()
	{
		BinaryTree<int> bt = new([1, 2, 3, 4]);
		Assert.True(bt.Count == 4);
	}

	[Fact]
	public void GetDepth_Bst_CorrectDepth()
	{
		BinaryTree<int> bt = new([1, 2, 3, 4]);
		Assert.True(bt.Depth == 3);
	}

	[Fact]
	public void Add_Node_ReturnsBalancedBST()
	{
		BinaryTree<int> bt = new([1, 2, 3, 4]);
		bt.Add(6, 6);
		bt.Balance();
		Assert.True(bt.Contains(6));
		Assert.True(bt.IsBalanced());
	}

	[Fact]
	public void Remove_Node_ReturnsBalancedBST()
	{
		BinaryTree<int> bt = new([1, 2, 3, 4]);
		bt.Remove(4);

		bt.Balance();
		Assert.True(bt.IsBalanced());
		Assert.True(bt.Contains(4) is false);
	}

	[Fact]
	public void Contains_ExistingNode_ReturnsTrue()
	{
		BinaryTree<int> bt = new([1, 2, 3, 4]);
		Assert.True(bt.Contains(4));
	}

	[Fact]
	public void Contains_NonExistentNode_ReturnsFalse()
	{
		BinaryTree<int> bt = new([1, 2, 3, 4]);
		Assert.True(bt.Contains(55) is false);
	}

	[Fact]
	public void ValidateBST_BST_ReturnsTrue()
	{
		BinaryTree<int> bt = new([1, 2, 3, 4, 5, 6, 10]);
		Assert.True(IsBst(bt.Root));
	}

	[Fact]
	public void ValidateBalancedBst_ReturnsTrue()
	{
		BinaryTree<int> bt = new([1, 22, 3, 300, 5, 6, 10]);

		Assert.True(bt.IsBalanced());
	}

	private bool IsBst(Node<int>? root,
		int min = int.MinValue,
		int max = int.MaxValue)
	{
		if (root == null)
		{
			return true;
		}

		if (root.Value <= min || root.Value >= max)
		{
			return false;
		}

		bool leftIsBst = IsBst(root.Left, min, root.Value);
		bool rightIsBst = IsBst(root.Right, root.Value, max);

		return leftIsBst && rightIsBst;
	}
}