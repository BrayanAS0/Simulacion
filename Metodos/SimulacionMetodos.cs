using System;
using System.Collections.Generic;

namespace InventarioSimulador.Metodos
{
    public class SimulacionMetodos
    {
        private enum EstadoValidacion
        {
            Inicio,
            EsperandoLetra,
            EsperandoNumero,
            Exito, 
            Error
        }



        public bool ValidarCodigoRackConAutomata(string codigo)
        {
            if (string.IsNullOrEmpty(codigo) || codigo.Length != 5)
                return false;

            EstadoValidacion estado = EstadoValidacion.Inicio;
            int contadorNumeros = 0;

            foreach (char c in codigo)
            {
                if (estado == EstadoValidacion.Inicio)
                {
                    if (char.IsUpper(c))
                    {
                        estado = EstadoValidacion.EsperandoNumero;
                    }
                    else
                    {
                        estado = EstadoValidacion.Error;
                    }
                }
                else if (estado == EstadoValidacion.EsperandoNumero)
                {
                    if (char.IsDigit(c))
                    {
                        contadorNumeros++;
                    }
                    else
                    {
                        estado = EstadoValidacion.Error;
                    }
                }
                else if (estado == EstadoValidacion.Error)
                {
                    return false;
                }
            }

            return estado != EstadoValidacion.Error &&
                   contadorNumeros == 4;
        }

        public bool ValidarCadenaConPatron(string cadena, string patron)
        {
            //if (!ValidarCodigoRackConAutomata(cadena)) // si no tiene una letra mayuscual al inicio y 4 digitos regresa false 
            //    return false;

            int i = 0; //  cadena
            int p = 0; //  patron
            //ejemplo de un patron Ld{4} Y cadena F0206
            while (i < cadena.Length && p < patron.Length) /// si i no arrbaza el alrgo de la cadena y p no se ha salido del patron 
            {
                char token = patron[p];

                if (token == 'L' || token == 'd' || token == 'l' || token == 'a')
                {
                            if (p + 1 < patron.Length && patron[p + 1] == '{') ///patron = {
                            {
                                int cierre = patron.IndexOf('}', p + 2); 
                                if (cierre == -1) return false;

                                string contenido = patron.Substring(p + 2, cierre - p - 2); //F0206 Ld{4}
                                                                                            //  
                                var partes = contenido.Split(',');

                                if (!int.TryParse(partes[0], out int min)) return false;
                                int max = partes.Length > 1 && !string.IsNullOrEmpty(partes[1]) ?
                                          int.Parse(partes[1]) : min;

                                int repeticiones = 0;
                                while (repeticiones < max && i < cadena.Length) //d{1,4}
                                {
                                    if (!CoincideToken(cadena[i], token)) break;
                                    i++;
                                    repeticiones++;
                                }

                                if (repeticiones < min) return false;//si no se hicieron las minimas veces 
                                p = cierre + 1;
                            }
                            else //no teine llaves
                            {
                                // Validación simple de un carácter
                                if (i >= cadena.Length || !CoincideToken(cadena[i], token))
                                    return false;
                                i++;
                                p++;
                            }
                }
                else if (token == '|') //ya que no entro aqui                 if (token == 'L' || token == 'd' || token == 'l' || token == 'a')

                {
                            var alternativas = patron.Split('|');
                            foreach (var alt in alternativas)
                            {
                                if (ValidarCadenaConPatron(cadena, alt))
                                    return true;
                            }
                            return false;
                }
                else
                {
                            // Caracter literal com */-@#$%&?¡
                            if (i >= cadena.Length || cadena[i] != token)// si la ubucacion es mas grande o igual o carater es difentes al toke regresa false 
                                return false;
                            i++;
                            p++;
                }
            }

            return i == cadena.Length && p == patron.Length;
        }

        private bool CoincideToken(char c, char token)
        {
            if (token == 'L')
            {
                return char.IsUpper(c);
            }
            else if (token == 'l')
            {
                return char.IsLower(c);
            }
            else if (token == 'a')
            {
                return char.IsLetter(c);
            }
            else if (token == 'd')
            {
                return char.IsDigit(c);
            }
            else
            {
                return false;
            }
        }

    }
}