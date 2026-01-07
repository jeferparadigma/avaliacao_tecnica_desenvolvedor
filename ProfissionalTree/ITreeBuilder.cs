using ProfissionalTree.Models;

namespace ProfissionalTree.Interfaces;

/// <summary>
/// Define o contrato para construção de árvores binárias.
/// Princípio: Dependency Inversion (SOLID D)
/// </summary>
public interface ITreeBuilder
{
    /// <summary>
    /// Constrói uma árvore binária a partir de um array.
    /// </summary>
    TreeNode? Build(int[] values);
}