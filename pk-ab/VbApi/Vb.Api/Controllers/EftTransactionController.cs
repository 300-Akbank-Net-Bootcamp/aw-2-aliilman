using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EftTransactionController : ControllerBase
{
    private readonly VbDbContext dbContext;


    public EftTransactionController(VbDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<EftTransaction>> Get()
    {
        return await dbContext.Set<EftTransaction>()
            .ToListAsync();
    }

    // [HttpGet("{id}")]
    // public async Task<Customer> Get(int id)
    // {
    //     var customer =  await dbContext.Set<Customer>()
    //         .Include(x=> x.Accounts)
    //         .Include(x=> x.Addresses)
    //         .Include(x=> x.Contacts)
    //         .Where(x => x.Id == id).FirstOrDefaultAsync();
    //     return customer;
    // }

    [HttpPost]
    public async Task Post([FromBody] EftTransaction eftTransaction)
    {
        await dbContext.Set<EftTransaction>().AddAsync(eftTransaction);
        await dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int accountId, [FromBody] EftTransaction eftTransaction)
    {
        var fromdb = await dbContext.Set<EftTransaction>().Where(x => x.AccountId == accountId).FirstOrDefaultAsync();
        fromdb.SenderIban = eftTransaction.SenderIban;
        fromdb.SenderName = eftTransaction.SenderName;

        await dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int accountId)
    {
        dbContext.Remove(await dbContext.Set<EftTransaction>().Where(x => x.AccountId == accountId).FirstOrDefaultAsync());
        await dbContext.SaveChangesAsync();
    }
}