namespace ProfissionalTree;

public static class TreePrinter
{
    public static void Print(int[] values, string title)
    {
        Console.WriteLine(title + ":"); 
        // 1. Raiz int 
        int root = values.Max(); 
        int rootIndex = Array.IndexOf(values, root); 
        // 2. Dividir arrays e ordenar decrescente 
        var left = values.Take(rootIndex).OrderByDescending(x => x).ToArray(); 
        var right = values.Skip(rootIndex + 1).OrderByDescending(x => x).ToArray(); 
        // 3. Imprimir raiz 
        
        int leftPosition = left.Length + left.Length - 1;

        Console.WriteLine(new string(' ', leftPosition) + $" {root}");

        Console.WriteLine(new string(' ', leftPosition ) + "/ \\"); 
        // 4. Loop pelo maior tamanho 
        int maxLen = Math.Max(left.Length, right.Length); 
        for (int i = 0; i < maxLen; i++) { 
            string leftVal = i < left.Length ? new string(' ', leftPosition - (i+2)) + left[i].ToString() : " "; 
            string rightVal = i < right.Length ? new string(' ',  (i+2)) + right[i].ToString() : " "; 
            Console.WriteLine($" {leftVal} {rightVal}"); 
            // se ainda houver elementos, desenha os galhos 
            if (i < left.Length - 1 || i < right.Length - 1) 
               Console.WriteLine( new string(' ', left.Length - i) + "/" + new string(' ',  (i+4)) + "\\");
        }

  
    }
}
