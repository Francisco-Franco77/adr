using AdR.DatabaseContext;
using AdR.Interfaces;
using AdR.Models.Enums;
using AdR.Models;
using AdR.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace AdR.Services
{
    public class CadastroService([FromServices] IEmpresaRepository empresaRepository, INotaRepository notaRepository)
    {
        public MensagemServiceResult CadastroEmpresa(Empresa empresa)
        {
            int result = empresaRepository.CreateEmpresa(empresa);
            if (result == 1)
                return new MensagemServiceResult(true, "Empresa de cnpj: " + empresa.Cnpj + " cadastrada com id: " + empresa.Id);
            else
                return new MensagemServiceResult(false, "Empresa não pode ser cadastrada");
        }
        public MensagemServiceResult CadastroNota(Nota nota)
        {
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
            if (nota.DataVencimento.DayNumber < dataAtual.DayNumber)
                return new MensagemServiceResult(false, "Data de vencimento da nota é anterior à data atual");
            int result = notaRepository.CreateNota(nota);
            if (result == 1)
                return new MensagemServiceResult(true, "Nota de número: " + nota.Numero + " cadastrada para empresa de id: " + nota.EmpresaId);
            else
                return new MensagemServiceResult(false, "Nota não pode ser cadastrada");
        }
    }
}
