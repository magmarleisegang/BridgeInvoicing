using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using System;
using Java.Lang;
using System.Collections.Generic;

namespace BridgeUI.Driod.Helpers
{
    [ContentProvider(new string[] { BridgeUI.Driod.Helpers.CachedFileProvider.AUTHORITY })]
    public class CachedFileProvider : ContentProvider
    {
        Context context = Android.App.Application.Context;


        public const string AUTHORITY = "com.bridgeinvoice.attachprovider";
        static string BASE_PATH = "files";
        public static readonly Android.Net.Uri CONTENT_URI = Android.Net.Uri.Parse("content://" + AUTHORITY + "/" + BASE_PATH);
        private UriMatcher _uriMatcher;

        public override int Delete(Android.Net.Uri uri, string selection, string[] selectionArgs)
        {
            throw new NotImplementedException();
        }

        public override string GetType(Android.Net.Uri uri)
        {
            return "application/octet-stream";
        }

        public override Android.Net.Uri Insert(Android.Net.Uri uri, ContentValues values)
        {
            throw new NotImplementedException();
        }

        public override bool OnCreate()
        {
            _uriMatcher = new UriMatcher(UriMatcher.NoMatch);
            _uriMatcher.AddURI(AUTHORITY, "*", 1);
            return true;
        }

        public override ICursor Query(Android.Net.Uri uri, string[] projection, string selection, string[] selectionArgs, string sortOrder)
        {
            switch (_uriMatcher.Match(uri))
            {

                case 1:
                    MatrixCursor cursor = null;

                    var file = new Java.IO.File(System.IO.Path.Combine(context.CacheDir.Path, uri.LastPathSegment));
                    if (file.Exists())
                    {
                        cursor = new MatrixCursor(new string[] { OpenableColumns.DisplayName, OpenableColumns.Size});
                        var list = new Java.Util.ArrayList(2);
                        list.Add(uri.LastPathSegment);
                        list.Add(file.Length());
                        cursor.AddRow(list);
                    }

                    return cursor;
                default:
                    return null;
            }
        }
        

        public override int Update(Android.Net.Uri uri, ContentValues values, string selection, string[] selectionArgs)
        {
            throw new NotImplementedException();
        }

        public override ParcelFileDescriptor OpenFile(Android.Net.Uri uri, string mode)
        {
            switch (_uriMatcher.Match(uri))
            {
                case 1:
                    var fileLocation = System.IO.Path.Combine(context.CacheDir.Path, uri.LastPathSegment);
                    ParcelFileDescriptor parcelFileDescriptor = ParcelFileDescriptor.Open(new Java.IO.File(fileLocation), ParcelFileMode.ReadOnly);
                    return parcelFileDescriptor;
                default:
                    throw new System.Exception("File not found");
            }
        }
    }
}