﻿using Domain.Common;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class VacanteResponse : BaseResponse
    {
        public int IdVacante { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [Required]
        public string DescripcionCargo { get; set; } = string.Empty;

        [Required]
        public string Profesion { get; set; } = string.Empty;

        [Required]
        public int TiempoExperiencia { get; set; }

        [Required]
        public int IdContrato { get; set; }

        public string Horario { get; set; } = string.Empty;

        [Required]
        public int IdModalidadTrabajo { get; set; }

        public bool PruebaTecnica { get; set; }
        public string DescripcionFunciones { get; set; } = string.Empty;

        [Required]
        public int IdEstadoVacante { get; set; }

        public string Comentarios { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Prioridad 0:Baja 1:Alta
        /// </summary>
        public bool Prioridad { get; set; }

        public decimal Salario100Prestacion { get; set; }

        public decimal SalarioPorcentual { get; set; }

        public decimal SalarioPrestacionServicios { get; set; }

        public string TiempoContrato { get; set; }

        public List<IdiomaVacanteResponse> ListaIdiomas { get; set; }
    }

    public class VacanteDetailResponse
    {
        public int IdVacante { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [Required]
        public string DescripcionCargo { get; set; } = string.Empty;

        [Required]
        public string Profesion { get; set; } = string.Empty;

        [Required]
        public int TiempoExperiencia { get; set; }

        [Required]
        public int IdContrato { get; set; }

        public string? DescripcionContrato { get; set; }

        [Required]
        public int IdSalario { get; set; }

        public string? DescripcionSalario { get; set; }

        public string Horario { get; set; } = string.Empty;

        [Required]
        public int IdModalidadTrabajo { get; set; }

        public string? DescripcionModalidadTrabajo { get; set; }

        public string Idioma { get; set; } = string.Empty;
        public string PorcentajeIdioma { get; set; } = string.Empty;
        public bool PruebaTecnica { get; set; }
        public string? DescripcionFunciones { get; set; } = string.Empty;

        [Required]
        public int IdEstadoVacante { get; set; }

        public string? DescripcionEstadoVacante { get; set; }
        public string Comentarios { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Prioridad 0:Baja 1:Alta
        /// </summary>
        public bool Prioridad { get; set; }

        public decimal Salario100Prestacion { get; set; }

        public decimal SalarioPorcentual { get; set; }

        public decimal SalarioPrestacionServicios { get; set; }

        public TiempoContrato TiempoContrato { get; set; }
    }

    public class SkillVacanteResponse
    {
        [Required]
        public int IdCategoria { get; set; }

        public string Descripcion { get; set; } = string.Empty;
    }

    public class VacantesEmpresaResponse
    {
        public int IdVacante { get; set; }
        public string? DescripcionCargo { get; set; }
        public int IsEstadoVacante { get; set; }
        public string? EstadoVacante { get; set; }
    }
}