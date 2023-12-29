using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly VbDbContext dbContext;

    public AddressController(VbDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Address>> Get()
    {
        return await dbContext.Set<Address>()
            .ToListAsync();
    }

    // [HttpGet("{id}")]
    // public async Task<Address> Get(int customerId)
    // {
    //     var address =  await dbContext.Set<Address>()
    //         .Include(x=> x.Customer)
    //         .Where(x => x.CustomerId == customerId).FirstOrDefaultAsync();
       
    //     return address;
    // }

    [HttpPost]
    public async Task Post([FromBody] Address address)
    {
        await dbContext.Set<Address>().AddAsync(address);
        await dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int customerId, [FromBody] Address address)
    {
        var fromdb = await dbContext.Set<Address>().Where(x => x.CustomerId == customerId).FirstOrDefaultAsync();
        fromdb.Address1 = address.Address1;
        fromdb.Address2 = address.Address2;

        await dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int customerId)
    {
        dbContext.Remove(await dbContext.Set<Address>().Where(x => x.CustomerId == customerId).FirstOrDefaultAsync());
        await dbContext.SaveChangesAsync();
    }
}