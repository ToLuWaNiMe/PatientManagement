using Microsoft.EntityFrameworkCore;
using PatientManagement.Application.Models;
using PatientManagement.Infrastructure.Data;
using PatientManagement.Infrastructure.Services;

namespace PatientManagement.Tests.Services
{
    public class PatientRecordServiceTests : IDisposable
    {
        private readonly PatientManagementContext _dbContext;
        private readonly PatientRecordService _patientRecordService;

        public PatientRecordServiceTests()
        {
            // Set up the in-memory database
            var options = new DbContextOptionsBuilder<PatientManagementContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _dbContext = new PatientManagementContext(options);

            // Initialize the service with the in-memory DbContext
            _patientRecordService = new PatientRecordService(_dbContext);
        }

        [Fact]
        public async Task GetPatientRecordByIdAsync_ReturnsPatientRecord_WhenIdIsValid()
        {
            // Arrange
            var patientRecord = new PatientRecord
            {
                PatientRecordId = 1,
                PatientId = 101,
                RecordDate = DateTime.Parse("2023-02-10"),
                Description = "Initial Consultation"
            };

            _dbContext.PatientRecords.Add(patientRecord);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _patientRecordService.GetPatientRecordByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(patientRecord.PatientRecordId, result.PatientRecordId);
        }

        [Fact]
        public async Task GetPatientRecordByIdAsync_ReturnsNull_WhenIdIsInvalid()
        {
            // Act
            var result = await _patientRecordService.GetPatientRecordByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllPatientRecordsByPatientIdAsync_ReturnsRecordsForSpecificPatient()
        {
            // Arrange
            _dbContext.PatientRecords.Add(new PatientRecord
            {
                PatientRecordId = 1,
                PatientId = 101,
                RecordDate = DateTime.Parse("2023-01-01"),
                Description = "Blood Test"
            });
            _dbContext.PatientRecords.Add(new PatientRecord
            {
                PatientRecordId = 2,
                PatientId = 102,
                RecordDate = DateTime.Parse("2023-01-15"),
                Description = "X-ray"
            });
            _dbContext.PatientRecords.Add(new PatientRecord
            {
                PatientRecordId = 3,
                PatientId = 101,
                RecordDate = DateTime.Parse("2023-02-01"),
                Description = "Follow-up Visit"
            });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _patientRecordService.GetAllPatientRecordsByPatientIdAsync(101);

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CreatePatientRecordAsync_AddsPatientRecordToDatabase()
        {
            // Arrange
            var patientRecord = new PatientRecord
            {
                PatientId = 103,
                RecordDate = DateTime.Parse("2023-03-01"),
                Description = "MRI Scan"
            };

            // Act
            var createdRecord = await _patientRecordService.CreatePatientRecordAsync(patientRecord);

            // Assert
            var retrievedRecord = await _dbContext.PatientRecords.FindAsync(createdRecord.PatientRecordId);
            Assert.NotNull(retrievedRecord);
            Assert.Equal(patientRecord.Description, retrievedRecord.Description);
        }

        [Fact]
        public async Task UpdatePatientRecordAsync_UpdatesExistingPatientRecord()
        {
            // Arrange
            var patientRecord = new PatientRecord
            {
                PatientRecordId = 1,
                PatientId = 104,
                RecordDate = DateTime.Parse("2023-04-01"),
                Description = "Initial Diagnosis"
            };
            _dbContext.PatientRecords.Add(patientRecord);
            await _dbContext.SaveChangesAsync();

            // Modify details
            patientRecord.Description = "Updated Diagnosis";

            // Act
            var updatedRecord = await _patientRecordService.UpdatePatientRecordAsync(patientRecord);

            // Assert
            var retrievedRecord = await _dbContext.PatientRecords.FindAsync(patientRecord.PatientRecordId);
            Assert.Equal("Updated Diagnosis", retrievedRecord.Description);
        }

        [Fact]
        public async Task DeletePatientRecordAsync_RemovesPatientRecordFromDatabase()
        {
            // Arrange
            var patientRecord = new PatientRecord
            {
                PatientRecordId = 1,
                PatientId = 105,
                RecordDate = DateTime.Parse("2023-05-01"),
                Description = "General Checkup"
            };
            _dbContext.PatientRecords.Add(patientRecord);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _patientRecordService.DeletePatientRecordAsync(1);

            // Assert
            Assert.True(result);
            var retrievedRecord = await _dbContext.PatientRecords.FindAsync(1);
            Assert.Null(retrievedRecord);
        }

        public void Dispose()
        {
            // Clean up the database between test runs
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
