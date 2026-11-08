var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();

List<Jogadores> ListaJogadores = new List<Jogadores>
{
    new Jogadores { Nome = "Joao", Pontos = 150 },
    new Jogadores { Nome = "Maria", Pontos = 200 },
    new Jogadores { Nome = "Pedro", Pontos = 250 }
};

app.MapGet("/{bonus}/{pontos}", (int bonus, int pontos) =>
{


    int totalPontos = pontos + bonus;
    string resultado = $"Total de pontos: {totalPontos}";

    if (totalPontos >= 200 && bonus > 50)
    {
        int sobra = totalPontos - 200;
        return $"Parabéns! você passou! sobraram {sobra} pontos!";
    }
    else
     {
        int sobra = 200 - totalPontos;
        return $"Você não passou, Faltou {sobra} pontos para passar!";
     }
});

   app.MapGet("/ranking", () =>
   {
    
    List<int> pontuacoes = new List<int> {120, 150, 200,250,300};

    return pontuacoes.OrderBy(p =>p).ToList();

});
 


app.MapGet("/jogadores", () =>
{

    return ListaJogadores.OrderBy(j => j.Pontos).ToList();
});

app.MapGet("/jogadores/{nome}", (string nome) =>
{
    var jogador = ListaJogadores.FirstOrDefault(j => j.Nome.ToLower() == nome.ToLower());
     
     if (jogador !=null)
    {
        return Results.Ok(jogador);

    }

    else
    {
        return Results.NotFound($"Jogador {nome} nao encontrado.");
    }
    
});
app.MapGet("/jogadores/Aprovados", () =>
{

    return ListaJogadores.Where(j => j.Pontos >= 200).OrderBy(j => j.Pontos).ToList();
});

app.MapPost("/jogadores", (Jogadores novojogador) =>
{
    ListaJogadores.Add(novojogador);
return$" jogador {novojogador.Nome} adicionado com sucesso!";
});





app.Run();
class Jogadores
{
    public string Nome { get; set;}
    public int Pontos {get; set;}
}

