﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UploadArquivos.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace UploadArquivos.Controllers
{
    public class RelatorioController : Controller
    {
        IHostingEnvironment Enviroment { get; set; }

        public RelatorioController(IHostingEnvironment enviroment)
        {
            Enviroment = enviroment;
        }

        [Route("Relatorio/Exibir/{nome}")]
        public IActionResult Exibir(string nome)
        {
            RelatorioMunicipio relatorio = new RelatorioMunicipio(Enviroment.WebRootPath);
            relatorio.Municipio = new Municipio() { Nome = nome };
            return View(relatorio);
        }

        [HttpPost]
        public IActionResult GerarRelatorio(Municipio municipio)
        {
            if (municipio.Arquivo != null)
            {
                // salvando arquivo em disco
                var reader = new BinaryReader(municipio.Arquivo.OpenReadStream());
                System.IO.File.WriteAllBytes(System.IO.Path.Combine(Enviroment.WebRootPath, $"{municipio.Nome}." + municipio.Arquivo.ContentType.Split('/').Last()), reader.ReadBytes((int)municipio.Arquivo.Length));
            }
            return RedirectToAction("Exibir", "Relatorio", municipio.Nome);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
