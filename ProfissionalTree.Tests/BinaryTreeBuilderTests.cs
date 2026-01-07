using ProfissionalTree.Services;
using Xunit;

namespace ProfissionalTree.Tests;

public class BinaryTreeBuilderTests
{
    private readonly BinaryTreeBuilder _builder = new BinaryTreeBuilder();

    [Fact]
    public void Build_WithEmptyArray_ReturnsNull()
    {
        var result = _builder.Build(new int[] { });
        Assert.Null(result);
    }

    [Fact]
    public void Build_WithSingleElement_ReturnsSingleNode()
    {
        var result = _builder.Build(new int[] { 5 });
        Assert.NotNull(result);
        Assert.Equal(5, result.Value);
        Assert.Null(result.Left);
        Assert.Null(result.Right);
    }

    [Fact]
    public void Build_WithMultipleElements_BuildsCorrectStructure()
    {
        // Array: [3, 2, 1, 6, 0, 5]
        // Raiz: 6 (maior valor)
        // Esquerda: [3, 2, 1] em ordem decrescente
        // Direita: [5, 0] em ordem decrescente
        var result = _builder.Build(new int[] { 3, 2, 1, 6, 0, 5 });
        
        Assert.NotNull(result);
        Assert.Equal(6, result.Value);
        
        // Verifica galho esquerdo
        Assert.NotNull(result.Left);
        Assert.Equal(3, result.Left.Value);
        Assert.NotNull(result.Left.Left);
        Assert.Equal(2, result.Left.Left.Value);
        Assert.NotNull(result.Left.Left.Left);
        Assert.Equal(1, result.Left.Left.Left.Value);
        Assert.Null(result.Left.Left.Left.Left);
        
        // Verifica galho direito
        Assert.NotNull(result.Right);
        Assert.Equal(5, result.Right.Value);
        Assert.NotNull(result.Right.Right);
        Assert.Equal(0, result.Right.Right.Value);
        Assert.Null(result.Right.Right.Right);
    }

    [Fact]
    public void Build_Scenario2_VerifiesCorrectStructure()
    {
        // Array: [7, 5, 13, 9, 1, 6, 4]
        // Raiz: 13 (maior valor)
        // Esquerda (à esquerda de 13): [7, 5] em ordem decrescente
        // Direita (à direita de 13): [9, 6, 4, 1] em ordem decrescente
        var result = _builder.Build(new int[] { 7, 5, 13, 9, 1, 6, 4 });
        
        Assert.NotNull(result);
        Assert.Equal(13, result.Value);
        
        // Galho esquerdo: 7 -> 5
        Assert.NotNull(result.Left);
        Assert.Equal(7, result.Left.Value);
        Assert.NotNull(result.Left.Left);
        Assert.Equal(5, result.Left.Left.Value);
        Assert.Null(result.Left.Left.Left);
        
        // Galho direito: 9 -> 6 -> 4 -> 1
        Assert.NotNull(result.Right);
        Assert.Equal(9, result.Right.Value);
        Assert.NotNull(result.Right.Right);
        Assert.Equal(6, result.Right.Right.Value);
        Assert.NotNull(result.Right.Right.Right);
        Assert.Equal(4, result.Right.Right.Right.Value);
        Assert.NotNull(result.Right.Right.Right.Right);
        Assert.Equal(1, result.Right.Right.Right.Right.Value);
        Assert.Null(result.Right.Right.Right.Right.Right);
    }

    [Fact]
    public void Build_WithNullArray_ReturnsNull()
    {
        var result = _builder.Build(null);
        Assert.Null(result);
    }
}