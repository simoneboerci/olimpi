using System;
using System.IO;
using System.Threading.Tasks;

namespace Olimpi.Core.LogSystem.Infrastructure{
    public class FileSink : ILogSink
    {
        private readonly string _filePath;
        private readonly int _maxFileSizeBytes;
        private readonly int _maxBackupFiles;

        public FileSink(string filePath, int maxFileSizeBytes = 10 * 1024 * 1024, int maxBackupFiles = 5){
            _filePath = filePath;
            _maxFileSizeBytes = maxFileSizeBytes;
            _maxBackupFiles = maxBackupFiles;
        }

        public void Write(string formattedLog)
        {
            RotateLogFileIfNeeded();
            File.AppendAllText(_filePath, formattedLog + Environment.NewLine);
        }

        public async Task WriteAsync(string formattedLog)
        {
            await RotateLogFileIfNeededAsync();
            await File.AppendAllTextAsync(_filePath, formattedLog + Environment.NewLine);
        }

        private void RotateLogFileIfNeeded(){
            if(File.Exists(_filePath) && new FileInfo(_filePath).Length > _maxFileSizeBytes){
                for(int i = _maxBackupFiles - 1; i >= 0; i--){
                    string sourceFile = i == 0 ? _filePath : $"{_filePath}.{i}";
                    string destFile = $"{_filePath}.{i + 1}";

                    if(File.Exists(sourceFile)){
                        if (i == _maxBackupFiles - 1) File.Delete(sourceFile);
                        else File.Move(sourceFile, destFile);
                    }
                }

                File.Move(_filePath, $"{_filePath}.1");
            }
        }

        private async Task RotateLogFileIfNeededAsync(){
            if(File.Exists(_filePath) && (await Task.Run(() => new FileInfo(_filePath).Length)) > _maxFileSizeBytes){
                for(int i = _maxBackupFiles - 1; i >= 0; i--){
                    string sourceFile = i == 0 ? _filePath : $"{_filePath}.{i}";
                    string destFile = $"{_filePath}.{i + 1}";

                    if(File.Exists(sourceFile)){
                        if (i == _maxBackupFiles - 1) await Task.Run(() => File.Delete(sourceFile));
                        else await Task.Run(() => File.Move(sourceFile, destFile));
                    }
                }

                await Task.Run(() => File.Move(_filePath, $"{_filePath}.1"));
            }
        }
    }
}