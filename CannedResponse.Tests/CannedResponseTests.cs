using FluentAssertions;
using NUnit.Framework;
using XmppBot.Common;
using XmppBot_CannedResponse;

namespace CannedResponse.Tests
{
    [TestFixture]
    public class CannedResponseTests
    {
        [Test]
        public void should_not_respond_to_partial_trigger()
        {
            var plugin = new CannedResponsePlugin();

            var pl = new ParsedLine("This is the exact trigger purple monkey dishwahser", "bob");

            plugin.Evaluate(pl).Should().BeNull("This wasn't the exact trigger");
        }

        [Test]
        public void should_respond_to_exact_trigger()
        {
            // Default app.config; one response in file, 100% response chance
            var plugin = new CannedResponsePlugin();

            var pl = new ParsedLine("This is the exact trigger", "bob");

            plugin.Evaluate(pl)
                  .Should()
                  .Be("This is the exact response");
        }

        [Test]
        public void should_respond_to_partial_trigger()
        {
            var plugin = new CannedResponsePlugin();

            var pl = new ParsedLine("This line contains the partial trigger fnord, which should be enough", "bob");

            plugin.Evaluate(pl).Should().Be("You said fnord!");
        }

        [Test]
        public void a_trap()
        {
            var plugin = new CannedResponsePlugin();

            var pl = new ParsedLine("I think it's a tRap", "bob");

            plugin.Evaluate(pl).Should().Be("https://skydrive.live.com/redir?resid=3F2CFE9060480107%21285");
        }
    }
}