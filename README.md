# TDC Connections 2022

## Trilha Arquitetura .NET
### **Gerenciando memória no .NET do jeito certo**
#
### Objetivo: Mostrar como a falta de conhecimento ou desatenção na implementação de simples códigos, podem gerar grandes impactos no código caso ele faça parte do caminho crítico de sua aplicação.
#

O intuito deste código (Console Application) é mostrar 2 pontos distintos:
- Nos Steps 1, 2 e 3 mostramos como simples objetos do tipo string podem gerar pressão no GC que afetará diretamente a performance da aplicação;
- Nos Steps 4, 5 e 6 o intuito é mostrar o resultado das alocações e como identificá-los na Heap. Também é possível ver se possuem referência, se estão na finalyzer queue ou se estão apenas aguardando próxima coleta para serem desalocados.

### Passos para executar o código após cloná-lo

Abra 3 terminais

1. No primeiro terminal execute o console application;
```
   dotnet run
```
2. No segundo terminal você pode utilizar o dotnet-counters para ver como as alterações realizadas no console refletem na Heap (substitua pid pelo código do processo MemoryManagement em execução exibido após executar o comando ==dotnet-counters ps==);
```
   dotnet-counters ps
   dotnet-counters monitor --refresh-interval 1 -p pid
```
3. No terceiro terminal utilize o dotnet-dump para coletar o dump da Heap e poder analizar os objetos alocados (substitua pid pelo código do processo MemoryManagement em execução exibido após executar o comando ==dotnet-counters ps== / Em seguida substitua dump_filename pelo nome do arquivo gerado após executar o comando ==dotnet-dump collect -p pid==);
```
   dotnet-dump collect -p pid
   dotnet-dump analyze dump_filename
```

Após a execução do comando ==dotnet-dump analyze dump_filename== você poderá executar comandos para análise da Heap. Abaixo os mais comuns:
```
   dumpheap -stat    # Lista todos os objetos da heap em ordem de tamanho
   dumpheap -type MemoryManagement.Produto      # Lista todos os objetos do tipo MemoryManagement.Produto
   dumpobj endereco_memoria   # Escolha um objeto listado e substitua o endereco_memoria pelo endereço copiado e veja a estrutura do objeto
   gcroot endereco_memoria    # Lista a cadeia de raízes do objeto
   exit     # volta ao terminal
```
