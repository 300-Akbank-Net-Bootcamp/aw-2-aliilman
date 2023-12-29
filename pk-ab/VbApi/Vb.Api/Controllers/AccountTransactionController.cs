using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountTransactionController : ControllerBase
{
    private readonly VbDbContext dbContext;

    public AccountTransactionController(VbDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<AccountTransaction>> Get()
    {
        return await dbContext.Set<AccountTransaction>()
            .ToListAsync();
    }

    // [HttpGet("{id}")]
    // public async Task<AccountTransaction> Get(int accountId)
    // {
    //     var accountTransaction =  await dbContext.Set<AccountTransaction>()
    //         .Include(x=> x.Account)
    //         .Where(x => x.AccountId == accountId).FirstOrDefaultAsync();
       
    //     return accountTransaction;
    // }

    [HttpPost]
    public async Task Post([FromBody] AccountTransaction accountTransaction)
    {
        await dbContext.Set<AccountTransaction>().AddAsync(accountTransaction);
        await dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int accountId, [FromBody] AccountTransaction accountTransaction)
    {
        var fromdb = await dbContext.Set<AccountTransaction>().Where(x => x.AccountId == accountId).FirstOrDefaultAsync();
        fromdb.ReferenceNumber = accountTransaction.ReferenceNumber;


        await dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int accountId)
    {
        dbContext.Remove(await dbContext.Set<AccountTransaction>().Where(x => x.AccountId == accountId).FirstOrDefaultAsync());
        await dbContext.SaveChangesAsync();
    }
}