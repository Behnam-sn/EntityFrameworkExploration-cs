using FluentAssertions;
using TPH.Entities;

namespace TPH.Tests;

public sealed class TPHTests : IClassFixture<ApplicationDbContextFixture>
{
    private readonly ApplicationDbContext _context;

    public TPHTests(ApplicationDbContextFixture domainDbContextFixture)
    {
        _context = domainDbContextFixture.Context;
    }

    [Fact]
    public async Task Test1()
    {
        // Arrange
        var customer = new Customer();

        // Act
        await _context.Set<Customer>().AddAsync(customer);
        await _context.SaveChangesAsync();

        // Assert
        var actual = _context.Set<Customer>().First(b => b.Id == customer.Id);
        actual.Should().NotBeNull();
    }
}