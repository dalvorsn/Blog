﻿using Blog.Models.Blog.Autor;
using System;
using System.Collections.Generic;

namespace Blog.ViewMoldels.Home
{
    public class HomeIndexViewModel
    {
        public ICollection<PostagemHomeIndex> Postagens { get; set; }

        public HomeIndexViewModel() {
            Postagens = new List<PostagemHomeIndex>();
        }
    }

    public class PostagemHomeIndex
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public AutorEntity Autor { get; set; }
        public string UrlCapa { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public string UltimaAtualizacaoFormatada {
            get
            {
                TimeSpan lastUpdate = DateTime.Now.Subtract(this.UltimaAtualizacao);
                if (lastUpdate.TotalDays > 0)
                {
                    return string.Format("Last updated on {0}", this.UltimaAtualizacao.ToString("MM/dd/YYYY"));
                }
                else
                {
                    return string.Format("{0:D2}:{0:D2}:{1:D2}", (int)lastUpdate.TotalHours, (int)lastUpdate.TotalMinutes, lastUpdate.Seconds);
                }
            }
        }

    }
}
