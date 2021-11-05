using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Veiculo.Dominio.ViewModel
{
    public class AgendadorViewModel
    {
        [Display(Name = "Minuto")]        
        [MaxLength(2, ErrorMessage = "Máximo 2 caracteres permitidos")]
        [Range(0, 59, ErrorMessage = "Os valores devem estar entre 0 e 59")]
        public string Minute { get; set; }
        
        [Display(Name = "Hora")]
        [MaxLength(2, ErrorMessage = "Máximo 2 caracteres permitidos")]
        [Range(0, 23, ErrorMessage = "Os valores devem estar entre 0 e 23")]
        public string Hour { get; set; }
        
        [Display(Name = "Dia do Mês")]
        [MaxLength(2, ErrorMessage = "Máximo 2 caracteres permitidos")]
        [Range(1, 31, ErrorMessage = "Os valores devem estar entre 1 e 31")]
        public string DayMonth { get; set; }
        
        [Display(Name = "Mês")]
        [MaxLength(2, ErrorMessage = "Máximo 2 caracteres permitidos")]
        [Range(1, 13, ErrorMessage = "Os valores devem estar entre 1 e 13")]
        public string Month { get; set; }
        
        [Display(Name = "Dia da semana")]
        [MaxLength(1, ErrorMessage = "Máximo 1 caracter permitido")]
        [Range(0, 7, ErrorMessage = "Os valor deve estar entre 0 e 7")]
        public string DayWeek { get; set; }

    }
}
