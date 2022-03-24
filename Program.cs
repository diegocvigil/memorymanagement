using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MemoryManagement
{
   class Program
   {
      static int loopQty = 100000;
      static List<Pessoa> listPerson = new List<Pessoa>();
      static int executionsStep4 = 0;  // contador para o step4
      static int executionsStep5 = 0;  // contador para o step5
      static int executionsStep6 = 0;  // contador para o step6

      static void Main(string[] args)
      {
         string answer = "";

         var stopWatch = new Stopwatch();

         do
         {
            Console.WriteLine("\nDigite o número do Passo a ser executado");
            // Aguarda tecla digitada no console para identificar o Passo a ser executado
            answer = Console.ReadLine().ToUpper();

            // Recupera contador de coletas do GC
            var gcZero = GC.CollectionCount(0);
            var gcOne = GC.CollectionCount(1);
            var gcTwo = GC.CollectionCount(2);

            stopWatch.Reset();
            stopWatch.Start();

            switch (answer)
            {
               case "1":
                  Step1();
                  break;
               case "2":
                  Step2();
                  break;
               case "3":
                  Step3();
                  break;
               case "4":
                  Step4();
                  break;
               case "5":
                  Step5();
                  break;
               case "6":
                  Step6();
                  break;
               case "9":
                  Step9();
                  break;
               case "C":
                  // Executa manualmente uma coleta de gen2 (full)
                  GC.Collect(2);
                  break;
               default:
                  break;
            }

            stopWatch.Stop();

            // Exibe um resumo da execução do Step
            Console.WriteLine($"############################");
            Console.WriteLine($"# Tempo: {stopWatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"# Memória: {Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024} MB");
            Console.WriteLine($"# GC - Gen 0: {GC.CollectionCount(0) - gcZero}");
            Console.WriteLine($"# GC - Gen 1: {GC.CollectionCount(1) - gcOne}");
            Console.WriteLine($"# GC - Gen 2: {GC.CollectionCount(2) - gcTwo}");
            Console.WriteLine($"############################");
         } while (answer != "X");
      }

      static void Step1()
      {
         Console.WriteLine("Comparando 10.000.000 de \"sim\"");

         string sim = "sim";

         for (int i = 0; i < 10000000; i++)
         {
            if (sim == "sim")
            {
            }
         }
      }

      static void Step2()
      {
         Console.WriteLine("Comparando 10.000.000 de \"SIM\" (toupper)");

         string sim = "sim";

         for (int i = 0; i < 10000000; i++)
         {
            if (sim.ToUpper() == "SIM")
            {
            }
         }
      }

      static void Step3()
      {
         Console.WriteLine("Comparando 10.000.000 de \"SIM\" ou \"S\" (toupper e substring)");

         string sim = "sim";

         for (int i = 0; i < 10000000; i++)
         {
            if (sim.ToUpper() == "SIM" && sim.Substring(1).ToUpper() == "S")
            {
            }
         }
      }

      static void Step4()
      {
         Console.WriteLine($"Inicializando {loopQty} instâncias de Produto (sem Using)");
         List<Produto> products = new List<Produto>(loopQty);

         executionsStep4++;

         for (int i = 0; i < loopQty; i++)
         {
            products.Add(new Produto(i, "Step4 / Produto " + i, 'S', executionsStep4));
         }
      }

      static async void Step5()
      {
         Console.WriteLine($"Inicializando {loopQty} instâncias de Area (com Using)");
         List<Area> products = new List<Area>(loopQty);

         executionsStep5++;

         for (int i = 0; i < loopQty; i++)
         {
            using (var area = new Area(i, "Step5 / Area " + i, 'S', executionsStep5))
            {
               products.Add(area);
            }
         }
      }

      static void Step6()
      {
         Console.WriteLine($"Inicializando {loopQty} instâncias de Pessoa");

         executionsStep6++;

         for (int i = 0; i < loopQty; i++)
         {
            listPerson.Add(new Pessoa(i, "Pessoa " + i, "Lastname " + executionsStep6, i, executionsStep6));
         }
      }

      static void Step9()
      {
         Console.WriteLine("Inicializando char[90000]");

         List<char> lista = new List<char>(86000);

         for (int i = 0; i < 86000; i++)
         {
            lista.Add('A');
         }
      }
   }
}
