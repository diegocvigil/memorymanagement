using System;
using System.Data.SqlClient;

namespace MemoryManagement
{
   // Classe implementa IDisposable apenas para desalocar o objeto SqlConnection 
   // que possuio Dispose por se tratar de um código de acesso a banco de dados
   // e consequentemente possuir código não gerenciado
   public class Produto: IDisposable
   {
      private string name;
      private int id;
      private char status;
      private int geracao;
      private bool disposedValue;

      SqlConnection conn;

      public Produto(int id, string name, char status, int geracao)
      {
         this.id = id;
         this.name = name;
         this.status = status;
         this.geracao = geracao;
         conn = new SqlConnection();
      }

      protected virtual void Dispose(bool disposing)
      {
         if (!disposedValue)
         {
            if (disposing)
            {
               // Desacola objetos gerenciados
               Console.WriteLine($"[Produto] Executando Dispose ({this.name} / Geração: {this.geracao})");
               conn.Dispose();
               conn = null;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
         }
      }

      // Não é uma boa prática utilizar Finalyzer. Deixei descomentado apenas para exibir a mensagem
      // no console da execução e vermos o GC em ação
      // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
      ~Produto()
      {
         Console.WriteLine($"[Produto] Executando Finalyzer ({this.name} / Geração: {this.geracao})");
          // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
          Dispose(disposing: false);
      }

      public void Dispose()
      {
         // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
         Dispose(disposing: true);
         GC.SuppressFinalize(this);
      }
   }
}