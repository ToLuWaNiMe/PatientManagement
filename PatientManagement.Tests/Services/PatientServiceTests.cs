using Microsoft.EntityFrameworkCore;
using PatientManagement.Application.Models;
using PatientManagement.Infrastructure.Data;
using PatientManagement.Infrastructure.Services;

namespace PatientManagement.Tests.Services
{
    public class PatientServiceTests : IDisposable
    {
        private readonly PatientManagementContext _dbContext;
        private readonly PatientService _patientService;

        public PatientServiceTests()
        {
            // Set up the in-memory database
            var options = new DbContextOptionsBuilder<PatientManagementContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _dbContext = new PatientManagementContext(options);

            // Initialize the service with the in-memory DbContext
            _patientService = new PatientService(_dbContext);
        }

        [Fact]
        public async Task GetPatientByIdAsync_ReturnsPatient_WhenIdIsValid()
        {
            // Arrange
            var patient = new Patient
            {
                PatientId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Gender = "Male",
                Phone = "1234567890",
                DateOfBirth = DateTime.Parse("1985-01-01")
            };

            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _patientService.GetPatientByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(patient.PatientId, result.PatientId);
        }

        [Fact]
        public async Task GetPatientByIdAsync_ReturnsNull_WhenIdIsInvalid()
        {
            // Act
            var result = await _patientService.GetPatientByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllPatientsAsync_ReturnsAllPatients()
        {
            // Arrange
            _dbContext.Patients.Add(new Patient
            {
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice.smith@example.com",
                Gender = "Female",
                Phone = "5551234567"
            });
            _dbContext.Patients.Add(new Patient
            {
                FirstName = "Bob",
                LastName = "Jones",
                Email = "bob.jones@example.com",
                Gender = "Male",
                Phone = "5559876543"
            });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _patientService.GetAllPatientsAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CreatePatientAsync_AddsPatientToDatabase()
        {
            // Arrange
            var patient = new Patient
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                Gender = "Female",
                Phone = "9876543210",
                DateOfBirth = DateTime.Parse("1990-05-15")
            };

            // Act
            var createdPatient = await _patientService.CreatePatientAsync(patient);

            // Assert
            var retrievedPatient = await _dbContext.Patients.FindAsync(createdPatient.PatientId);
            Assert.NotNull(retrievedPatient);
            Assert.Equal(patient.Email, retrievedPatient.Email);
        }

        [Fact]
        public async Task UpdatePatientAsync_UpdatesExistingPatient()
        {
            // Arrange
            var patient = new Patient
            {
                PatientId = 1,
                FirstName = "Jack",
                LastName = "Brown",
                Email = "jack.brown@example.com",
                Gender = "Male",
                Phone = "5556789101"
            };
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();

            // Modify details
            patient.LastName = "Black";
            patient.Phone = "1112223333";

            // Act
            var updatedPatient = await _patientService.UpdatePatientAsync(patient);

            // Assert
            var retrievedPatient = await _dbContext.Patients.FindAsync(patient.PatientId);
            Assert.Equal("Black", retrievedPatient.LastName);
            Assert.Equal("1112223333", retrievedPatient.Phone);
        }

        [Fact]
        public async Task DeletePatientAsync_SoftDeletesPatient()
        {
            // Arrange
            var patient = new Patient
            {
                PatientId = 1,
                FirstName = "Emily",
                LastName = "Davis",
                Email = "emily.davis@example.com",
                Gender = "Female",
                Phone = "4445556667"
            };
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _patientService.DeletePatientAsync(1);

            // Assert
            var retrievedPatient = await _dbContext.Patients.FindAsync(1);
            Assert.True(result);
            Assert.True(retrievedPatient.IsDeleted);
        }

        public void Dispose()
        {
            // Clean up the database between test runs
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
