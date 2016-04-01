namespace Hanabi.IO
{
   public class IOService
    {
        private IInputReader _inputReader;
        private IOutputWriter _outputWriter;

        public IOService(IInputReader inputReader, IOutputWriter outputWriter)
        {
            _inputReader = inputReader;
            _outputWriter = outputWriter;
        }
        public string ReadLine()
        {
            return _inputReader.ReadLine();
        }
        public bool WriteLine(string line)
        {
            return _outputWriter.WriteLine(line);
        }

    }
}
