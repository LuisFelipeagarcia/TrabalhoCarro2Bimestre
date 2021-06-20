using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelo.Cadastro
{
    public class Carro
    {
        [Key]
        public long? CarroID { get; set; }

        [Required]
        public string Cor { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Fabricante { get; set; }
    }
}
