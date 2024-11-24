namespace Shared;

public interface ILogger
{
	public void Log(string message);

	public void Error(string message);
}
