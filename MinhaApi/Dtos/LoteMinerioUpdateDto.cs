using System.ComponentModel.DataAnnotations;

namespace MinhaApi.Dtos
{
    // Usando 'record' igual ao seu Response, mas com validações
    public record UpdateLoteMinerioDto(
        [Required(ErrorMessage = "O Código do Lote é obrigatório")]
        string CodigoLote,

        [Required(ErrorMessage = "A Mina de Origem é obrigatória")]
        string MinaOrigem,

        [Range(0, 100, ErrorMessage = "O teor de Fe deve ser entre 0 e 100")]
        decimal TeorFe,

        [Range(0, 100, ErrorMessage = "A umidade deve ser entre 0 e 100")]
        decimal Umidade,

        decimal? SiO2,

        decimal? P,

        [Range(0.01, double.MaxValue, ErrorMessage = "Toneladas deve ser maior que zero")]
        decimal Toneladas,

        DateTime? DataProducao,

        // Aqui recebemos int para validar o range fácil (0 a 2), 
        // mas poderia ser o Enum direto se preferir.
        [Range(0, 2, ErrorMessage = "Status inválido (use 0, 1 ou 2)")]
        int Status,

        [Required(ErrorMessage = "A localização atual é obrigatória")]
        string LocalizacaoAtual
    );
}
