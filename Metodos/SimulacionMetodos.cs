namespace InventarioSimulador.Metodos;
    public class SimulacionMetodos
    {
    public bool ValidarCadenaConPatron(string cadena, string patron)
    {
        int i = 0; // Índice para la cadena de entrada
        for (int p = 0; p < patron.Length;)
        {
            char token = patron[p];

            if (token == 'L' || token == 'd' || token == 'l')
            {
                p++;
                if (p < patron.Length && patron[p] == '{') /// es una llave y ahi mas aparte de la llave
                {
                    int cierre = patron.IndexOf('}', p);
                    if (cierre == -1) return false;

                    string contenido = patron.Substring(p + 1, cierre - p - 1); // es lo que ahi de {lo qu esta aqui adentro }
                    int min = 0, max = int.MaxValue;

                    if (contenido.Contains(","))
                    {
                        var partes = contenido.Split(','); /// ejemplo va tener {1,}
                        if (!int.TryParse(partes[0], out min)) return false; // si no puede convertitlo no se puede ese patron asiq eu regresa false
                        if (partes.Length > 1 && !string.IsNullOrEmpty(partes[1])) ///siginca que ahi {3,4}
                        {
                            if (!int.TryParse(partes[1], out max)) return false;/// ahria inteta aparsea el {3,4}  el 4 si es una letra o cosa que no sea un simbolo regresa false
                        }
                    }
                    else /// entonces fue {3,}
                    {
                        if (!int.TryParse(contenido, out min)) return false;/// solo parsea el 3 
                        max = min; // exactamente min repeticiones entonces seria 3 min y max
                    }

                    int repeticiones = 0;
                    while (repeticiones < max && i < cadena.Length)
                    {
                        if (token == 'L' && !char.IsUpper(cadena[i])) break;
                        if (token == 'd' && !char.IsDigit(cadena[i])) break;
                        if (token == 'l' && !char.IsLetter(cadena[i])) break;

                        i++;
                        repeticiones++;
                    }

                    if (repeticiones < min) return false;

                    p = cierre + 1;
                }
                else
                {

///solo espera ya sea slo una letra o digito  ay que el siguiente no es un parentesis 
                    if (i >= cadena.Length) return false;

                    if (token == 'L' && !char.IsUpper(cadena[i])) return false;
                    if (token == 'd' && !char.IsDigit(cadena[i])) return false;
                    if (token == 'l' && !char.IsLetter(cadena[i])) return false;

                    i++;
                }
            }
            else ///enatre si es un caracter especial como - | @ entre otros 
            {
                // Carácter literal esperado
                if (i >= cadena.Length || cadena[i] != token) return false;
                i++;
                p++;
            }
        }

        return i == cadena.Length;
    }


}

