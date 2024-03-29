
using Whisper.net;

namespace MauiAppUnir.Services.Interfaces;

public interface IWhisperService
{
    Task DownloadGGMLAsync();
    WhisperFactory? whisperFactory { get; set; }
    WhisperProcessor? whisperProcessor { get; set; }

}
