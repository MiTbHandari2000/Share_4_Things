using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Share_4_Things
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button _ShareTextButton;
        private Button _ShareLinkButton;
        private Button _ShareAttachmentButton;
        private Button _ShareMultiAttachmentButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            _ShareTextButton = FindViewById<Button>(Resource.Id.ButtonShareText);
            _ShareLinkButton = FindViewById<Button>(Resource.Id.ButtonSharelink);
            _ShareAttachmentButton = FindViewById<Button>(Resource.Id.ButtonShareAttachment);
            _ShareMultiAttachmentButton = FindViewById<Button>(Resource.Id.ButtonShareMultiplleAttachment);
            _ShareTextButton.Click += _ShareTextButton_Click;
            _ShareLinkButton.Click += _ShareLinkButton_Click;
            _ShareAttachmentButton.Click += _ShareAttachmentButton_Click;
            _ShareMultiAttachmentButton.Click += _ShareMultiAttachmentButton_Click;
        }

        private void _ShareMultiAttachmentButton_Click(object sender, System.EventArgs e)
        {
            _ = ShareMultipleFiles();
        }

        private void _ShareAttachmentButton_Click(object sender, System.EventArgs e)
        {
            _ = ShareFiles();
        }

        private async Task ShareFiles()
        {
            var fn = "Attachment.txt";
            var file = Path.Combine(FileSystem.CacheDirectory, fn);
            File.WriteAllText(file, "hello world");
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = Title,
                File = new ShareFile(file)
            });
        }

        private void _ShareLinkButton_Click(object sender, System.EventArgs e)
        {
            _ = ShareUri("https://www.google.co.in/");
        }

        private async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share Web Link "
            });
        }

        private void _ShareTextButton_Click(object sender, System.EventArgs e)
        {
            _ = ShareText("hello world");
        }

        private async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share text"
            }


                );
        }

        public async Task ShareMultipleFiles()
        {

            var file1 = Path.Combine(FileSystem.CacheDirectory, "Attachment1.txt");
            File.WriteAllText(file1, "Content 1");
            var file2 = Path.Combine(FileSystem.CacheDirectory, "Attachment2.txt");
            File.WriteAllText(file2, "content2");

            await Share.RequestAsync(new ShareMultipleFilesRequest
            {
                Title = "Title",
                Files = new List<ShareFile> { new ShareFile(file1), new ShareFile(file2) }
            });
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}