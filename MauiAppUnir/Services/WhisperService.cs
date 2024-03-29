using MauiAppUnir.Services.Interfaces;
using Whisper.net.Ggml;
using Whisper.net;

namespace MauiAppUnir.Services;

internal class WhisperService : IWhisperService
{

   public WhisperFactory? whisperFactory { get; set; }

    public WhisperProcessor? whisperProcessor { get; set; }

    public async Task DownloadGGMLAsync()
    {
        // Definir la ruta del archivo local.
        string localFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "LargeV3.ggml");

        // Verificar si el archivo ya existe.
        if (!File.Exists(localFilePath))
        {
            // Si el archivo no existe, descargarlo y guardarlo.
            await DownloadAndSaveGgmlModelAsync(GgmlType.LargeV3, localFilePath);
        }

        // Independientemente de si el archivo fue descargado o ya existía, cargarlo en memoria.
        if (whisperFactory == null)
        {
            whisperFactory = WhisperFactory.FromPath(localFilePath);
        }

        if (whisperProcessor == null)
        {
            whisperProcessor = whisperFactory.CreateBuilder()
                                   .WithLanguage("es")
                                   .Build();
        }
    }

    // Método para descargar y guardar el modelo GGML localmente. Deberás implementarlo.
    private async Task DownloadAndSaveGgmlModelAsync(GgmlType type, string savePath)
    {
        var model = await WhisperGgmlDownloader.GetGgmlModelAsync(type);
        using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
        {
            await model.CopyToAsync(fileStream);
        }
    }
}
