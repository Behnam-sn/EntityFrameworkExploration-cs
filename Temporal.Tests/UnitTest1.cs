using Domain;
using Microsoft.EntityFrameworkCore;

namespace Temporal.Tests
{
    public class UnitTest1 : IClassFixture<DomainDbContextFixture>
    {
        private const string SOME_TITLE = "Title1";
        private const string SOME_OTHER_TITLE = "Title2";
        private const string SOME_CODE = "Code1";
        private const string SOME_OTHER_CODE = "Code2";
        private const int SOME_VALUE = 1;
        private const int SOME_OTHER_VALUE = 2;

        private readonly DomainDbContext domainDbContext;

        public UnitTest1(DomainDbContextFixture domainDbContextFixture)
        {
            domainDbContext = domainDbContextFixture.DomainDbContext;
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange
            var parameterValue = new ParameterValue()
            {
                Code = SOME_CODE,
                Value = SOME_VALUE
            };
            var document = new Document()
            {
                Id = Guid.NewGuid(),
                Title = SOME_TITLE,
                ParameterValues = new List<ParameterValue>
                {
                    parameterValue
                }
            };
            // Act
            await domainDbContext.AddAsync(document);
            await domainDbContext.SaveChangesAsync();
            // Assert
            var actual = domainDbContext.Documents.First(d => d.Id == document.Id);
            actual.Should().BeEquivalentTo(new
            {
                Title = SOME_TITLE,
                ParameterValues = new[]
                {
                    new
                    {
                        Code = SOME_CODE,
                        Value = SOME_VALUE
                    }
                }
            });
        }

        [Fact]
        public async Task Test2()
        {
            // Arrange
            var parameterValue = new ParameterValue()
            {
                Code = SOME_CODE,
                Value = SOME_VALUE
            };
            var document = new Document()
            {
                Id = Guid.NewGuid(),
                Title = SOME_TITLE,
                ParameterValues = new List<ParameterValue>
                {
                    parameterValue
                }
            };
            await domainDbContext.AddAsync(document);
            await domainDbContext.SaveChangesAsync();
            // Act
            document.Title = SOME_OTHER_TITLE;
            parameterValue.Code = SOME_OTHER_CODE;
            parameterValue.Value = SOME_OTHER_VALUE;
            await domainDbContext.SaveChangesAsync();
            // Assert
            var actual = domainDbContext.Documents.First(d => d.Id == document.Id);
            actual.Should().BeEquivalentTo(new
            {
                Title = SOME_OTHER_TITLE,
                ParameterValues = new[]
                {
                    new
                    {
                        Code = SOME_OTHER_CODE,
                        Value = SOME_OTHER_VALUE
                    }
                }
            });
        }

        [Fact]
        public async Task Test3()
        {
            // Arrange
            var parameterValue = new ParameterValue()
            {
                Code = SOME_CODE,
                Value = SOME_VALUE
            };
            var document = new Document()
            {
                Id = Guid.NewGuid(),
                Title = SOME_TITLE,
                ParameterValues = new List<ParameterValue>
                {
                    parameterValue
                }
            };
            await domainDbContext.AddAsync(document);
            await domainDbContext.SaveChangesAsync();
            // Act
            document.Title = SOME_OTHER_TITLE;
            await domainDbContext.SaveChangesAsync();
            // Assert
            var actual = domainDbContext.Documents.First(d => d.Id == document.Id);
            actual.Should().BeEquivalentTo(new
            {
                Title = SOME_OTHER_TITLE,
                ParameterValues = new[]
                {
                    new
                    {
                        Code = SOME_CODE,
                        Value = SOME_VALUE
                    }
                }
            });
        }

        [Fact]
        public async Task Test4()
        {
            // Arrange
            var parameterValue = new ParameterValue()
            {
                Code = SOME_CODE,
                Value = SOME_VALUE
            };
            var document = new Document()
            {
                Id = Guid.NewGuid(),
                Title = SOME_TITLE,
                ParameterValues = new List<ParameterValue>
                {
                    parameterValue
                }
            };
            await domainDbContext.AddAsync(document);
            await domainDbContext.SaveChangesAsync();
            // Act
            document.Title = SOME_OTHER_TITLE;
            parameterValue.Code = SOME_OTHER_CODE;
            parameterValue.Value = SOME_OTHER_VALUE;
            await domainDbContext.SaveChangesAsync();
            // Assert
            var actual = domainDbContext.Documents.TemporalAll().Where(d => d.Id == document.Id).ToList();
            actual.Should().BeEquivalentTo(new[]
            {
                new
                {
                    Title = SOME_TITLE,
                    ParameterValues = new[]
                    {
                        new
                        {
                            Code = SOME_CODE,
                            Value = SOME_VALUE
                        }
                    }
                },
                new
                {
                    Title = SOME_OTHER_TITLE,
                    ParameterValues = new[]
                    {
                        new
                        {
                            Code = SOME_OTHER_CODE,
                            Value = SOME_OTHER_VALUE
                        }
                    }
                }
            });
        }
    }
}