using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Data.Dtos
{
    public class FilmeDTO
    {
        [Required(ErrorMessage = "Titulo é obrigatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Diretor é obrigatorio")]
        public string Diretor { get; set; }

        [Required(ErrorMessage = "Genero é obrigatorio")]
        public string Genero { get; set; }

        [Range(1, 200, ErrorMessage = "A duração deve ter no maximo 200 minutos")]
        public int Duracao { get; set; }
    }
}