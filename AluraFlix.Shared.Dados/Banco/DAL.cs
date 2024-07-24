using AluraFlix.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AluraFlix.Shared.Dados.Banco;

public class DAL<T> where T : class
{
    private readonly AluraFlixContext context;
    public DAL(AluraFlixContext context)
    {
        this.context = context;
    }
    public IEnumerable<T> Listar()
    {
        return context.Set<T>().ToList();
    }
    public void Adicionar(T objeto)
    {
        context.Set<T>().Add(objeto);
        context.SaveChanges();
    }
    public void Atualizar(T objeto)
    {
        context.Set<T>().Update(objeto);
        context.SaveChanges();
    }
    public void Deletar(T objeto) 
    {
        context.Set<T>().Remove(objeto);
        context.SaveChanges();
    }
    public T? RecuperarPor(Func <T, bool> condicao)
    {
        return context.Set<T>().FirstOrDefault(condicao);
    }

    public async Task<IEnumerable<Videos>?> RecuperarVideosPorCategoriaAsync(string categoriaTitulo)
    {
        return await context.Videos
            .Include(v => v.Categorias) // Carrega a categoria relacionada
            .Where(v => v.Categorias != null && v.Categorias.Titulo.ToUpper() == categoriaTitulo.ToUpper())
            .ToListAsync();
    }
}
