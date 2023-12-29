using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly VbDbContext dbContext;

    public AccountController(VbDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Account>> Get()
    {
        return await dbContext.Set<Account>()
            .ToListAsync();
    }


    [HttpPost]
    public async Task Post([FromBody] Account account)
    {
        await dbContext.Set<Account>().AddAsync(account);
        await dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] Account account)
    {
        var fromdb = await dbContext.Set<Account>().Where(x => x.Id == id).FirstOrDefaultAsync();
        fromdb.Name = account.Name;

        await dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int accountNumber)
    {
       // var fromdb = await dbContext.Set<Account>().Where(x => x.AccountNumber == accountNumber).FirstOrDefaultAsync();
        dbContext.Remove(await dbContext.Set<Account>().Where(x => x.AccountNumber == accountNumber).FirstOrDefaultAsync());
        await dbContext.SaveChangesAsync();
    }
}