// using Minio;
// using Minio.DataModel;
// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Threading.Tasks;

// public class MinioFileRepository : IFileRepository
// {
//     private readonly MinioClient _minioClient;
//     private readonly string _bucketName;

//     public MinioFileRepository(string endpoint, string accessKey, string secretKey, string bucketName)
//     {
//         _minioClient = new MinioClient(endpoint, accessKey, secretKey);
//         _bucketName = bucketName;
//     }

//     public async Task Delete(string name)
//     {
//         await _minioClient.RemoveObjectAsync(_bucketName, name);
//     }

//     public async Task DeleteFilesWithoutReference()
//     {
//         // This method may require additional logic to determine which files should be deleted.
//         // Here's a simple example that deletes all files in the bucket.
//         var objectsList = await _minioClient.ListObjectsAsync(_bucketName, recursive: true);
//         foreach (var item in objectsList)
//         {
//             await _minioClient.RemoveObjectAsync(_bucketName, item.Key);
//         }
//     }

//     public async Task<ScrollFile?> Download(string fileName)
//     {
//         await _minioClient.GetObjectAsync(_bucketName, fileName, stream =>
//         {
//             using (var memoryStream = new MemoryStream())
//             {
//                 stream.CopyTo(memoryStream);
//                 return new ScrollFile
//                 {
//                     FileName = fileName,
//                     Content = memoryStream.ToArray()
//                 };
//             }
//         });
//         return null;  // Return null if the object does not exist
//     }

//     public async Task<bool> Exists(string name)
//     {
//         try
//         {
//             await _minioClient.StatObjectAsync(_bucketName, name);
//             return true;
//         }
//         catch (Exception)
//         {
//             return false;
//         }
//     }

//     public IQueryable<ScrollFileInfo> GetAll()
//     {
//         var objectsList = _minioClient.ListObjectsAsync(_bucketName, recursive: true).Result;
//         return objectsList.Select(item => new ScrollFileInfo
//         {
//             FileName = item.Key,
//             Size = item.Size
//         }).AsQueryable();
//     }

//     public async Task Upload(FileInfo fileInfo, string contentType)
//     {
//         await _minioClient.PutObjectAsync(_bucketName, fileInfo.Name, fileInfo.FullName, contentType);
//     }

//     public async Task Upload(Stream stream, string fileName, string contentType)
//     {
//         await _minioClient.PutObjectAsync(_bucketName, fileName, stream, stream.Length, contentType);
//     }

//     public async Task Upload(string filePath, string fileName, string contentType)
//     {
//         await _minioClient.PutObjectAsync(_bucketName, fileName, filePath, contentType);
//     }
// }