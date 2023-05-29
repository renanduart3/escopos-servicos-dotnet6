// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

void Scoped(ServiceProvider service)
{
	using (var scope = service.CreateScope())
	{
		// Obter instâncias de Foo e imprimir seus IDs
		var fooScoped1 = scope.ServiceProvider.GetService<Foo>();
		var fooScoped2 = scope.ServiceProvider.GetService<Foo>();

		Console.WriteLine("Serviços Scoped:");
		fooScoped1.PrintId();
		fooScoped2.PrintId();
        Console.WriteLine("Os hashs e Ids tem que ser iguais toda vez que vc selecionar uma opção no menu.");
        Console.WriteLine(fooScoped1.GetHashCode() + " - " + fooScoped2.GetHashCode());

		Console.WriteLine();
	}
}
void Singleton(ServiceProvider service)
{
	using (var scope = service.CreateScope())
	{
		// Obter instâncias de Foo e imprimir seus IDs
		var fooSingleton1 = service.GetService<Foo>();
		var fooSingleton2 = service.GetService<Foo>();

		Console.WriteLine("Serviços Singleton:");
		fooSingleton1.PrintId();
		fooSingleton2.PrintId();
		Console.WriteLine("Os hashs e Ids serão sempre o mesmo, enquanto a aplicação estiver rodando.");
		Console.WriteLine(fooSingleton1.GetHashCode() + " - " + fooSingleton2.GetHashCode());
		Console.WriteLine();
	}
}
void Transient(ServiceProvider service)
{
	using (var scope = service.CreateScope())
	{
		// Obter instâncias de Foo e imprimir seus IDs
		var fooTransient1 = scope.ServiceProvider.GetService<Foo>();
		var fooTransient2 = scope.ServiceProvider.GetService<Foo>();

		Console.WriteLine("Serviços Transient:");
		fooTransient1.PrintId();
		fooTransient2.PrintId();
		Console.WriteLine("Os hashs e Ids serão sempre o diferentes.");
		Console.WriteLine(fooTransient1.GetHashCode() + " - " + fooTransient2.GetHashCode());
	}

}

// Criar o container de injeção de dependência
var serviceTransient = new ServiceCollection()
	.AddTransient<Foo>() // Transient: Nova instância a cada vez que é injetado
	.BuildServiceProvider();

var serviceScoped = new ServiceCollection()
	.AddScoped<Foo>() // Transient: Nova instância a cada vez que é injetado
	.BuildServiceProvider();

var serviceSingleton = new ServiceCollection()
	.AddSingleton<Foo>() // Transient: Nova instância a cada vez que é injetado
	.BuildServiceProvider();

Console.WriteLine("Iniciar programa de escopos de serviços:");
Console.ReadKey();

int opcao;

// Laço para simular fluxo de execução, cada vez que seleciona uma opção, é criado uma injeção de dependencia
// de acordo com o ciclo de vida de cada implementação.
do
{
	Console.Clear();
	Console.WriteLine("Seleciona a opcao para executar");
	Console.WriteLine("1 - Executar servico Singleton");
	Console.WriteLine("2 - Executar servico Scoped");
	Console.WriteLine("3 - Executar servico Transient");
	Console.WriteLine("0 - Sair");

	Console.WriteLine("informe a opção desejada: ");
	opcao = int.Parse(Console.ReadLine());

	switch (opcao)
	{
		case 1:
			Console.WriteLine("Execução Singleton");
			Singleton(serviceSingleton);
			break;
		case 2:
			Console.WriteLine("Execução Scoped");
			Scoped(serviceScoped);
			break;
		case 3:
			Console.WriteLine("Execução Transient");
			Transient(serviceTransient);
			break;
		default:
			break;
	}
	Console.ReadKey();
} while (opcao != 0);


public class Foo
{
	private readonly Guid _id;

	public Foo()
	{
		_id = Guid.NewGuid();
	}

	public void PrintId()
	{
		Console.WriteLine($"Foo ID: {_id}");
	}
}
