using Xamarin.Forms;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Linq;

namespace XamStoreApp
{
	public partial class XamStoreAppPage : ContentPage
	{
		readonly Label _title = new Label { HorizontalTextAlignment = TextAlignment.Center };
		readonly Image _image = new Image();
		readonly ActivityIndicator _activityIndicator = new ActivityIndicator();

		public XamStoreAppPage()
		{
			Content = new StackLayout
			{
				Spacing = 15,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					_title,
					_image,
					_activityIndicator
				}
			};
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			_activityIndicator.IsRunning = true;

			var blobList = await BlobStorageService.GetBlobsAsync<CloudBlockBlob>("photos");

			var firstBlob = blobList?.FirstOrDefault();
			var photo = new PhotoModel { Title = firstBlob?.Name, Uri = firstBlob?.Uri };

			_title.Text = photo?.Title;
			_image.Source = ImageSource.FromUri(photo?.Uri);

			_activityIndicator.IsRunning = false;
			_activityIndicator.IsVisible = false;
		}
	}
}
