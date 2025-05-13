namespace InventarioSimulador.Metodos;
    public class SimulacionMetodos
    {
    public SimulacionMetodos() { }
        public bool ValidarCadenaConPatron(string cadena, string patron) //"L{3}-L{3}-d{4}"
    {
            int i = 0; // Índice para la cadena de entrada
            for (int p = 0; p < patron.Length;)
            {
                char token = patron[p];///la parte del patron 

                if (token == 'L' || token == 'd' || token=='l')
                {
                    p++;
                    if (p < patron.Length && patron[p] == '{') //verifica que ahi mas carartes despues del token que y que sea { 
                    {
                        int cierre = patron.IndexOf('}', p);
                        if (cierre == -1) return false;

                        string numRepeticiones = patron.Substring(p + 1, cierre - p - 1);
                        if (!int.TryParse(numRepeticiones, out int repeticiones)) return false;

                        for (int r = 0; r < repeticiones; r++)
                        {
                            if (i >= cadena.Length) return false;

                            if (token == 'L' && !char.IsUpper(cadena[i])) return false;
                            if (token == 'd' && !char.IsDigit(cadena[i])) return false;
                            if (token == 'l' && !char.IsLetter(cadena[i])) return false;

                            i++;
                        }
                        p = cierre + 1; // Mover el puntero después del }
                    }
                    else
                    {
                        // Si no hay {n}, esperamos solo un carácter
                        if (i >= cadena.Length) return false;

                        if (token == 'L' && !char.IsUpper(cadena[i])) return false;
                        if (token == 'd' && !char.IsDigit(cadena[i])) return false;
                        i++;
                    }
                }
                else//// SI NO ES LETA O DIGITO
                {
                    // Caracter literal esperado (ejemplo: '-')
                    if (i >= cadena.Length || cadena[i] != token) return false;
                    i++;
                    p++;
                }
            }

            return i == cadena.Length;
        }

    }

