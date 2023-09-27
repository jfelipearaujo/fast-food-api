namespace Domain.Entities.ClientAggregate.Validators;

public static class CpfValidator
{
    private const int CPF_LENGTH = 11;

    public static bool Check(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
        {
            return false;
        }

        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf;
        string digito;

        int soma;
        int resto;

        cpf = cpf.Trim()
            .Replace(".", string.Empty, StringComparison.InvariantCultureIgnoreCase)
            .Replace("-", string.Empty, StringComparison.InvariantCultureIgnoreCase);

        if (cpf.Length != CPF_LENGTH)
        {
            return false;
        }

        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        }

        resto = soma % CPF_LENGTH;

        if (resto < 2)
        {
            resto = 0;
        }
        else
        {
            resto = CPF_LENGTH - resto;
        }

        digito = resto.ToString();
        tempCpf += digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        }

        resto = soma % 11;

        if (resto < 2)
        {
            resto = 0;
        }
        else
        {
            resto = CPF_LENGTH - resto;
        }

        digito += resto.ToString();

        return cpf.EndsWith(digito, StringComparison.InvariantCultureIgnoreCase);
    }
}
