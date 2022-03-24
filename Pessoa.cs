using System;
using System.Data.SqlClient;

namespace MemoryManagement
{
   // Classe implementa IDisposable apenas para desalocar o objeto SqlConnection 
   // que possuio Dispose por se tratar de um código de acesso a banco de dados
   // e consequentemente possuir código não gerenciado
   public class Pessoa : IDisposable
   {
      Int64 id;
      string nome;
      string sobrenome;
      int idade;
      int geracao;
      string endereco;
      string cpf;
      string rg;
      DateTime dataNascimento;
      private bool disposedValue;

      SqlConnection conn;

      public Pessoa(int id, string nome, string sobrenome, int idade, int geracao)
      {
         this.id = id;
         this.nome = nome;
         this.sobrenome = sobrenome;
         this.idade = idade;
         this.geracao = geracao;
         conn = new SqlConnection();
      }

      public void Salvar()
      {
         // Salva no banco
      }

      protected virtual void Dispose(bool disposing)
      {
         Console.WriteLine($"[Pessoa] Executando Dispose de recursos gerenciados ({this.nome} / {this.sobrenome})");
         if (!disposedValue)
         {
            if (disposing)
            {
               // TODO: dispose managed state (managed objects)
               conn.Dispose();
               conn = null;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
         }
      }

      // Não é uma boa prática utilizar Finalyzer. Deixei descomentado apenas para exibir a mensagem
      // no console da execução e vermos o GC em ação. Neste caso, como o objeto Pessoa faz parte de
      // uma lista estática, o objeto nunca perde a referência e nunca é desalocado, logo a mensagem
      // abaixo não é exibida. Comparar o resultado deste finalyzer com o de produtos
      // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
      ~Pessoa()
      {
         Console.WriteLine($"[Pessoa] Executando Finalyzer ({this.nome} / Geração: {this.sobrenome})");
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
