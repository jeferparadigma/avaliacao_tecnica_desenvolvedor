using Xunit;
using ProfissionalTree;

namespace ProfissionalTree.Tests;

public class BinaryTreeBuilderTests
{
    private readonly ITreeBuilder _builder = new BinaryTreeBuilder();

    [Fact]
    public void BuildTree_ShouldCreateRootWithMaxValue()
    {
        int[] arr = { 3, 2, 1, 6, 0, 5 };
        var tree = _builder.Build(arr);

        Assert.Equal(6, tree.Value);
    }

    [Fact]
    public void BuildTree_LeftBranchShouldBeDescending()
    {
        int[] arr = { 3, 2, 1, 6, 0, 5 };
        var tree = _builder.Build(arr);

        Assert.Equal(3, tree.Left!.Value);
        Assert.Equal(2, tree.Left.Right!.Value);
        Assert.Equal(1, tree.Left.Right.Right!.Value);
    }

    [Fact]
    public void BuildTree_RightBranchShouldBeDescending()
    {
        int[] arr = { 3, 2, 1, 6, 0, 5 };
        var tree = _builder.Build(arr);

        Assert.Equal(5, tree.Right!.Value);
        Assert.Equal(0, tree.Right.Right!.Value);
    }

    [Fact]
    public void BuildTree_ShouldHandleSecondScenario()
    {
        int[] arr = { 7, 5, 13, 9, 1, 6, 4 };
        var tree = _builder.Build(arr);

        Assert.Equal(13, tree.Value);
        Assert.Equal(7, tree.Left!.Value);
        Assert.Equal(9, tree.Right!.Value);
    }
}
