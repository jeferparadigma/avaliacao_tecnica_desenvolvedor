using ProfissionalTree.Models;

namespace ProfissionalTree.Interfaces;

/// <summary>
/// Define o contrato para impressão de árvores.
/// Princípio: Open/Closed (SOLID O) - aberto para extensão
/// </summary>
public interface ITreePrinter
{
    /// <summary>
    /// Imprime a árvore de forma formatada.
    /// </summary>
    void Print(TreeNode? root, string title = "");
}