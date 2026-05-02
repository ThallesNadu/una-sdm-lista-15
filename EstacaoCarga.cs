using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDriveApi.Models
{
    public class EstacaoCarga
    {
        public int Id { get; set; }
        public string Localizacao { get; set; }
        public string TipoCarga { get; set; }
         public string Cidade { get; set; } = string.Empty;
        public double CargaDisponivelKW { get; set; }
        
    }
}