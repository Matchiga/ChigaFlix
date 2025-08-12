using ChigaFlix.Models;
using Microsoft.EntityFrameworkCore;

namespace ChigaFlix.Shared.Data.Bank;

public class DAL<T> where T : class
{
    private readonly ChigaFlixContext context;
    public DAL(ChigaFlixContext context)
    {
        this.context = context;
    }
    public IEnumerable<T> List()
    {
        return context.Set<T>().ToList();
    }
    public void Add(T objeto)
    {
        context.Set<T>().Add(objeto);
        context.SaveChanges();
    }
    public void Update(T objeto)
    {
        context.Set<T>().Update(objeto);
        context.SaveChanges();
    }
    public void Remove(T objeto) 
    {
        context.Set<T>().Remove(objeto);
        context.SaveChanges();
    }
    public T? RecoverBy(Func <T, bool> condition)
    {
        return context.Set<T>().FirstOrDefault(condition);
    }

    public async Task<IEnumerable<Videos>?> RetrieveVideosByCategoryAsync(string categoryTitle)
    {
        return await context.Videos
            .Include(v => v.Categories)
            .Where(v => v.Categories != null && v.Categories.Title.ToUpper() == categoryTitle.ToUpper())
            .ToListAsync();
    }
}
