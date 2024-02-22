

using NAudio.Wave;
using NAudio.Wave.SampleProviders;


namespace MauiAppUnir.Converters;


public class AudioConverter
{
    public static MemoryStream ConvertMp3ToWav(string mp3File)
    {

        using var fileStream = File.OpenRead(mp3File);
        using var wavStream = new MemoryStream();

        using var reader = new WaveFileReader(fileStream);
        var resampler = new WdlResamplingSampleProvider(reader.ToSampleProvider(), 16000);
        WaveFileWriter.WriteWavFileToStream(wavStream, resampler.ToWaveProvider16());

        // This section sets the wavStream to the beginning of the stream. (This is required because the wavStream was written to in the previous section)
        wavStream.Seek(0, SeekOrigin.Begin);
        return wavStream;
    }
}