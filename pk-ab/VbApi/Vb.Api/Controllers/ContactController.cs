using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly VbDbContext dbContext;

    public ContactController(VbDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Contact>> Get()
    {
        return await dbContext.Set<Contact>()
            .ToListAsync();
    }

    // [HttpGet("{id}")]
    // public async Task<Contact> Get(int customerId)
    // {
    //     var contact =  await dbContext.Set<Contact>()
    //         .Include(x=> x.Customer)
    //         .Where(x => x.CustomerId == customerId).FirstOrDefaultAsync();
       
    //     return contact;
    // }

    [HttpPost]
    public async Task Post([FromBody] Contact contact)
    {
        await dbContext.Set<Contact>().AddAsync(contact);
        await dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int customerId, [FromBody] Contact contact)
    {
        var fromdb = await dbContext.Set<Contact>().Where(x => x.Id == customerId).FirstOrDefaultAsync();
        fromdb.ContactType = contact.ContactType;
        fromdb.Information = contact.Information;

        await dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int customerId)
    {
        dbContext.Remove(await dbContext.Set<Contact>().Where(x => x.CustomerId == customerId).FirstOrDefaultAsync());
        await dbContext.SaveChangesAsync();
    }
}