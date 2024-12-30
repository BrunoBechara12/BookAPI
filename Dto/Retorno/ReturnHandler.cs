using Azure;
using BookAPI.Models;

namespace BookAPI.Dto.Retorno;

public class ReturnHandler<T>
{
    public static dynamic Return(string Mensagem, bool Status, dynamic Dados = null)
    {
        return AbstractedReturn(Mensagem, Status, Dados);
    }

    public static dynamic AbstractedReturn(string Mensagem, bool Status, dynamic Dados)
    {
        ResponseModel<T> response = new ResponseModel<T>
        {
            Mensagem = Mensagem,
            Status = Status,
            Dados = Dados
        };

        return response;
    }
}
