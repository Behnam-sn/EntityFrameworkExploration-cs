using Domain;
using Microsoft.EntityFrameworkCore;

namespace Temporal.Tests
{
    public class TemporalTests : IClassFixture<DomainDbContextFixture>
    {
        private readonly DomainDbContext domainDbContext;

        public TemporalTests(DomainDbContextFixture domainDbContextFixture)
        {
            domainDbContext = domainDbContextFixture.DomainDbContext;
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange
            var parameterValue = new ParameterValue()
            {
                Code = TemporalTestConstants.SOME_CODE,
                Value = TemporalTestConstants.SOME_VALUE
            };
            var document = new Document()
            {
                Id = Guid.NewGuid(),
                Title = TemporalTestConstants.SOME_TITLE,
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
                Title = TemporalTestConstants.SOME_TITLE,
                ParameterValues = new[]
                {
                    new
                    {
                        Code = TemporalTestConstants.SOME_CODE,
                        Value = TemporalTestConstants.SOME_VALUE
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
                Code = TemporalTestConstants.SOME_CODE,
                Value = TemporalTestConstants.SOME_VALUE
            };
            var document = new Document()
            {
                Id = Guid.NewGuid(),
                Title = TemporalTestConstants.SOME_TITLE,
                ParameterValues = new List<ParameterValue>
                {
                    parameterValue
                }
            };
            await domainDbContext.AddAsync(document);
            await domainDbContext.SaveChangesAsync();
            // Act
            document.Title = TemporalTestConstants.SOME_OTHER_TITLE;
            parameterValue.Code = TemporalTestConstants.SOME_OTHER_CODE;
            parameterValue.Value = TemporalTestConstants.SOME_OTHER_VALUE;
            await domainDbContext.SaveChangesAsync();
            // Assert
            var actual = domainDbContext.Documents.First(d => d.Id == document.Id);
            actual.Should().BeEquivalentTo(new
            {
                Title = TemporalTestConstants.SOME_OTHER_TITLE,
                ParameterValues = new[]
                {
                    new
                    {
                        Code = TemporalTestConstants.SOME_OTHER_CODE,
                        Value = TemporalTestConstants.SOME_OTHER_VALUE
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
                Code = TemporalTestConstants.SOME_CODE,
                Value = TemporalTestConstants.SOME_VALUE
            };
            var document = new Document()
            {
                Id = Guid.NewGuid(),
                Title = TemporalTestConstants.SOME_TITLE,
                ParameterValues = new List<ParameterValue>
                {
                    parameterValue
                }
            };
            await domainDbContext.AddAsync(document);
            await domainDbContext.SaveChangesAsync();
            // Act
            document.Title = TemporalTestConstants.SOME_OTHER_TITLE;
            await domainDbContext.SaveChangesAsync();
            // Assert
            var actual = domainDbContext.Documents.First(d => d.Id == document.Id);
            actual.Should().BeEquivalentTo(new
            {
                Title = TemporalTestConstants.SOME_OTHER_TITLE,
                ParameterValues = new[]
                {
                    new
                    {
                        Code = TemporalTestConstants.SOME_CODE,
                        Value = TemporalTestConstants.SOME_VALUE
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
                Code = TemporalTestConstants.SOME_CODE,
                Value = TemporalTestConstants.SOME_VALUE
            };
            var document = new Document()
            {
                Id = Guid.NewGuid(),
                Title = TemporalTestConstants.SOME_TITLE,
                ParameterValues = new List<ParameterValue>
                {
                    parameterValue
                }
            };
            await domainDbContext.AddAsync(document);
            await domainDbContext.SaveChangesAsync();
            // Act
            document.Title = TemporalTestConstants.SOME_OTHER_TITLE;
            parameterValue.Code = TemporalTestConstants.SOME_OTHER_CODE;
            parameterValue.Value = TemporalTestConstants.SOME_OTHER_VALUE;
            await domainDbContext.SaveChangesAsync();
            // Assert
            //var actual = await domainDbContext
            //    .Documents
            //    .TemporalAll()
            //    .Where(d => d.Id == document.Id)
            //    .Select(d => new DocumentVM
            //    {
            //        Id = d.Id,
            //        Title = d.Title,
            //        ParameterValues = domainDbContext.ParameterValues
            //        .TemporalFromTo(EF.Property<DateTime>(d, "ValidFrom"), EF.Property<DateTime>(d, "ValidTo"))
            //        .Where(pv => pv.DocumentId == d.Id)
            //        .ToList(),
            //        ValidFrom = EF.Property<DateTime>(d, "ValidFrom"),
            //        ValidTo = EF.Property<DateTime>(d, "ValidTo")
            //    })
            //    .ToListAsync();

            var actual = await domainDbContext
                .Documents
                .TemporalAll()
                .Where(d => d.Id == document.Id)
                .Select(d => new DocumentVM
                {
                    Id = d.Id,
                    Title = d.Title,
                    ValidFrom = EF.Property<DateTime>(d, "ValidFrom"),
                    ValidTo = EF.Property<DateTime>(d, "ValidTo")
                })
                .ToListAsync();

            foreach (var documentItem in actual)
            {
                var parameterValues = domainDbContext
                    .ParameterValues
                    .TemporalFromTo(documentItem.ValidFrom, documentItem.ValidTo)
                    .Where(pv => pv.DocumentId == documentItem.Id)
                    .ToList();

                documentItem.ParameterValues = parameterValues;
            }

            actual.Should().BeEquivalentTo(new[]
            {
                new
                {
                    Title = TemporalTestConstants.SOME_TITLE,
                    ParameterValues = new[]
                    {
                        new
                        {
                            Code = TemporalTestConstants.SOME_CODE,
                            Value = TemporalTestConstants.SOME_VALUE
                        }
                    }
                },
                new
                {
                    Title = TemporalTestConstants.SOME_OTHER_TITLE,
                    ParameterValues = new[]
                    {
                        new
                        {
                            Code = TemporalTestConstants.SOME_OTHER_CODE,
                            Value = TemporalTestConstants.SOME_OTHER_VALUE
                        }
                    }
                }
            });
        }
    }
}