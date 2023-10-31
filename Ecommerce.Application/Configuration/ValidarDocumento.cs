namespace Ecommerce.Application.Configuration
{
    public static class ValidarDocumento
    {
        public static bool IsValid(string documento)
        {
            return (IsCpf(documento) || IsCnpj(documento));
        }

        public static bool IsCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf?.Trim()))
                return false;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;


            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private static readonly int[] cnpjMultiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] cnpjMultiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        public static bool IsCnpj(string cnpj)
        {
            if (cnpj is null)
                return false;

            var digitosIdenticos = true;
            var ultimoDigito = -1;
            var posicao = 0;
            var totalDigito1 = 0;
            var totalDigito2 = 0;

            foreach (var c in cnpj)
            {
                if (!char.IsDigit(c)) 
                    continue;
                
                var digito = c - '0';
                if (posicao != 0 && ultimoDigito != digito)
                {
                    digitosIdenticos = false;
                }

                ultimoDigito = digito;
                switch (posicao)
                {
                    case < 12:
                        totalDigito1 += digito * cnpjMultiplicador1[posicao];
                        totalDigito2 += digito * cnpjMultiplicador2[posicao];
                        break;
                    case 12:
                    {
                        var dv1 = (totalDigito1 % 11);
                        dv1 = dv1 < 2 
                            ? 0 
                            : 11 - dv1;

                        if (digito != dv1)
                            return false;
                        
                        totalDigito2 += dv1 * cnpjMultiplicador2[12];
                        break;
                    }
                    case 13:
                    {
                        var dv2 = (totalDigito2 % 11);

                        dv2 = dv2 < 2 
                            ? 0 
                            : 11 - dv2;

                        if (digito != dv2)
                            return false;
                        break;
                    }
                }

                posicao++;
            }

            return (posicao == 14) && !digitosIdenticos;
        }
    }
}
