using Xunit;
using File_Duplicator_App;
namespace FileDuplicatorApp.Tests
{
    public class FileDuplicatorServiceTests
    {
        [Fact]
        public void DuplicateFiles_ShouldCreateFilesWithUpdatedNameAndContent()
        {
            // arrange
            var testDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            Directory.CreateDirectory(testDir);

            var subDir = Path.Combine(testDir, "Test_abc");

            Directory.CreateDirectory(subDir);

            var originalFile = Path.Combine(subDir, "page_abc.html");
            File.WriteAllText(originalFile, "This is abc content.");

            var service = new FileDuplicatorService(testDir);

            // act
            service.DuplicateFiles(testDir, "abc", "xyz");

            // assert
            var expectedFile = Path.Combine(subDir, "page_xyz.html");

            Assert.True(File.Exists(expectedFile));

            var newContent = File.ReadAllText(expectedFile);
            Assert.Contains("xyz", newContent, StringComparison.OrdinalIgnoreCase);

            // cleanup
            Directory.Delete(testDir, true);
        }
    }
}