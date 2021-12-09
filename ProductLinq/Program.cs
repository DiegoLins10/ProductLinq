using System;
using System.Collections.Generic;
using ProductLinq.Entities;
using System.IO;
using System.Globalization;
using System.Linq;

namespace ProductLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            //string path = Console.ReadLine();
            string path = @"C:\temp\in.txt";

            List<Product> list = new List<Product>();

            // Criando um streareader para chamar o file.open text que pega o Path do csv
            using (StreamReader sr = File.OpenText(path))
            {
                // enquanto não chegar no fim da stream
                while (!sr.EndOfStream)
                {
                    //separa os arquivos a partir da virgula
                    string[] fields = sr.ReadLine().Split(',');
                    string name = fields[0];
                    double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                    list.Add(new Product(name, price));
                }   
            }
            // fazendo um array com a media
            // var avg = list.Average(p => p.Price);
            // o jeito de cima funciona, porém o metodo abaixo é mais seguro
            // para tratar excecoes caso a colecao esteja vazia
            var avg = list.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Average price: " + avg.ToString("f2", CultureInfo.InvariantCulture));

            // mostra os nomes, em ordem decrescente, dos produtos que
            // possuem preço inferior ao preço médio
            var desc = list.Where(p => p.Price < avg).OrderByDescending(p => p.Nome).Select(p => p.Nome);
            foreach(var i in desc)
            {
                Console.WriteLine(i);
            }
        }
    }
}
