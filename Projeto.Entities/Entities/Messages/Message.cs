﻿using Projeto.Entities.Entities.Authentication;
using Projeto.Entities.Entities.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Entities.Entities.Messages
{
    [Table(name: "TB_MESSAGE")]
    public class Message : Notifiers
    {
        [Column("MSN_ID")]
        public int Id { get; set; }

        [Column("MSN_TITULO")]
        [MaxLength(255)]
        public string Titulo { get; set; }

        [Column("MSN_ATIVO")]
        public bool Ativo { get; set; }

        [Column("MSN_DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; }

        [Column("MSN_DATA_ALTERACAO")]
        public DateTime DataAlteracao { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
