using Domain;
using Microsoft.EntityFrameworkCore;

namespace Temporal.Tests
{
    public class TemporalTests : IClassFixture<ApplicationDbContextFixture>
    {
        private readonly ApplicationDbContext context;

        public TemporalTests(ApplicationDbContextFixture domainDbContextFixture)
        {
            context = domainDbContextFixture.Context;
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
            await context.AddAsync(document);
            await context.SaveChangesAsync();
            // Assert
            var actual = context.Documents.First(d => d.Id == document.Id);
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
            await context.AddAsync(document);
            await context.SaveChangesAsync();
            // Act
            document.Title = TemporalTestConstants.SOME_OTHER_TITLE;
            parameterValue.Code = TemporalTestConstants.SOME_OTHER_CODE;
            parameterValue.Value = TemporalTestConstants.SOME_OTHER_VALUE;
            await context.SaveChangesAsync();
            // Assert
            var actual = context.Documents.First(d => d.Id == document.Id);
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
            await context.AddAsync(document);
            await context.SaveChangesAsync();
            // Act
            document.Title = TemporalTestConstants.SOME_OTHER_TITLE;
            await context.SaveChangesAsync();
            // Assert
            var actual = context.Documents.First(d => d.Id == document.Id);
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
            await context.AddAsync(document);
            await context.SaveChangesAsync();
            // Act
            document.Title = TemporalTestConstants.SOME_OTHER_TITLE;
            parameterValue.Code = TemporalTestConstants.SOME_OTHER_CODE;
            parameterValue.Value = TemporalTestConstants.SOME_OTHER_VALUE;
            await context.SaveChangesAsync();
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

            var actual = await context
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
                var parameterValues = context
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