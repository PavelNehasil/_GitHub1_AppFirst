using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace AppFirst.Classes;

static class ImageBlobConverter
{
    public static byte[] ImageFilePathToBytes(string filePath)
    {
        byte[] bytes = null;
        if (File.Exists(filePath))
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    bytes = binaryReader.ReadBytes((int)fileStream.Length);
                }
            }
        }
        //var fileUri = new Uri(filePath);
        //var bitmapImage = new BitmapImage(fileUri);
        //StorageFile storageFile = await StorageFile.GetFileFromPathAsync(file.Path);
        //RandomAccessStreamReference stream = RandomAccessStreamReference.CreateFromFile(storageFile);
        //var streamContent = await stream.OpenReadAsync();
        //byte[] buffer = new byte[streamContent.Size];
        //await streamContent.ReadAsync(buffer.AsBuffer(), (uint)streamContent.Size, InputStreamOptions.None);
        return bytes;
    }

    public static async Task<byte[]> WriteableBitmapToBytesAsync(WriteableBitmap bitmapImage)
    {
        byte[] bytes = null;

        if (bitmapImage != null)
        {
            using (var ms = new InMemoryRandomAccessStream())
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, ms); //JpegEncoderId
                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Straight,
                    (uint)bitmapImage.PixelWidth,
                    (uint)bitmapImage.PixelHeight,
                    96, 96,
                    bitmapImage.PixelBuffer.ToArray()
                );

                await encoder.FlushAsync();
                bytes = new byte[ms.Size];
                await ms.ReadAsync(bytes.AsBuffer(), (uint)ms.Size, InputStreamOptions.None);
            }
        }

        return bytes;
    }

    public static async Task<BitmapImage> BytesToBitmapImage(byte[] bytes)
    {
        BitmapImage image = new BitmapImage();
        using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
        {
            await stream.WriteAsync(bytes.AsBuffer());
            stream.Seek(0);
            await image.SetSourceAsync(stream);
        }
        return image;
    }
    static public async Task<WriteableBitmap> ByteToWriteableBitmapAsync(byte[] value)
    {
        if (value == null || !(value is byte[]))
            return null;
        using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
        {
            using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
            {
                writer.WriteBytes((byte[])value);
                writer.StoreAsync().GetResults();
            }
            var image = new WriteableBitmap(1,1);
            image.SetSource(ms);
            return image;
        }
    }

    public static async Task<BitmapImage> GetBitmapAsync(byte[] data)
    {
        var bitmapImage = new BitmapImage();

        using (var stream = new InMemoryRandomAccessStream())
        {
            using (var writer = new DataWriter(stream))
            {
                writer.WriteBytes(data);
                await writer.StoreAsync();
                await writer.FlushAsync();
                writer.DetachStream();
            }

            stream.Seek(0);
            await bitmapImage.SetSourceAsync(stream);
        }

        return bitmapImage;
    }

    public static BitmapImage LoadBitmapImage(string filename)
    {
        var bitmapImage = new BitmapImage();

        bitmapImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;

        bitmapImage.UriSource = new Uri(filename);

        return bitmapImage;
    }

    //public static Byte[] SaveImage(string filename)
    //{
    //    Image image = Image.FromFile(filename);
    //    MemoryStream ms = new MemoryStream();
    //    image.Save(ms, image.RawFormat);
    //    Byte[] buffer = new Byte[ms.Length - 1];
    //    ms.Position = 0;
    //    ms.Read(buffer, 0, buffer.Length);
    //    ms.Close();
    //    return buffer;
    //}

    //public static Byte[] ImageToByte(BitmapImage imageSource)
    //{
    //    Stream stream = imageSource.StreamSource;
    //    Byte[] buffer = null;
    //    if (stream != null && stream.Length > 0)
    //    {
    //        using (BinaryReader br = new BinaryReader(stream))
    //        {
    //            buffer = br.ReadBytes((Int32)stream.Length);
    //        }
    //    }

    //    return buffer;
    //}

    public static async void SaveSoftwareBitmapToFile(SoftwareBitmap softwareBitmap, StorageFile outputFile)
    {
        IRandomAccessStream stream = await outputFile.OpenAsync(FileAccessMode.ReadWrite);

        // Create an encoder with the desired format
        Guid bitmapEncoderGuid = BitmapEncoder.PngEncoderId;
        switch (outputFile.FileType.ToLower())
        {
            case ".jpg" or ".jpeg":
                bitmapEncoderGuid = BitmapEncoder.JpegEncoderId;
                break;
            case ".png":
                bitmapEncoderGuid = BitmapEncoder.PngEncoderId;
                break;
            case ".bmp":
                bitmapEncoderGuid = BitmapEncoder.BmpEncoderId;
                break;
        }
        BitmapEncoder encoder = await BitmapEncoder.CreateAsync(bitmapEncoderGuid, stream);

        // Set the software bitmap
        encoder.SetSoftwareBitmap(softwareBitmap);

        // Set additional encoding parameters, if needed
        //encoder.BitmapTransform.ScaledWidth = 320;
        //encoder.BitmapTransform.ScaledHeight = 240;
        //encoder.BitmapTransform.Rotation = Windows.Graphics.Imaging.BitmapRotation.Clockwise90Degrees;
        encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Fant;
        encoder.IsThumbnailGenerated = false;

        await encoder.FlushAsync();

        //try
        //{
        //    await encoder.FlushAsync();
        //}
        //catch (Exception err)
        //{
        //    const int WINCODEC_ERR_UNSUPPORTEDOPERATION = unchecked((int)0x88982F81);
        //    switch (err.HResult)
        //    {
        //        case WINCODEC_ERR_UNSUPPORTEDOPERATION:
        //            // If the encoder does not support writing a thumbnail, then try again
        //            // but disable thumbnail generation.
        //            encoder.IsThumbnailGenerated = false;
        //            break;
        //        default:
        //            throw;
        //    }
        //}

        //if (encoder.IsThumbnailGenerated == false)
        //{
        //    await encoder.FlushAsync();
        //}
    }


}
