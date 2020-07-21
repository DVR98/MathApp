using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MathApp_DVR
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

    //For all user accounts

        public static UsersModel Model { get; set; }

        async public static Task SaveUserModelAsync()
        {
            DataContractSerializer DCS = new DataContractSerializer(Model.GetType());

            //Writes Objects stored on Model to Users.xml
            var File = await ApplicationData.Current.LocalFolder.CreateFileAsync("Users.xml", CreationCollisionOption.OpenIfExists);
            var transaction = await File.OpenTransactedWriteAsync();
            var RAS = transaction.Stream;
            var OS = RAS.GetOutputStreamAt(0);
            var Stream = OS.AsStreamForWrite();
            DCS.WriteObject(Stream, Model);
            Stream.Flush();
            Stream.Dispose();

            await transaction.CommitAsync();
            transaction.Dispose();
        }

//Link to xml files(For me): C:\Users\duran\AppData\Local\Packages\1420707b-80e0-4ff6-bc7e-de5df55bdbe7_8xgv4p6bky1vt\LocalState

        async public static Task<UsersModel> LoadUserModelAsync()
        {
            try
            {
                var File = await ApplicationData.Current.LocalFolder.GetFileAsync("Users.xml");
                var RAS = await File
                    .OpenAsync(Windows.Storage.FileAccessMode.Read);
                var IS = RAS.GetInputStreamAt(0);
                var reader = new DataReader(IS);
                var size = (await File
                    .GetBasicPropertiesAsync()).Size;
                await reader.LoadAsync((uint)size);
                var model_text = reader.ReadString((uint)size);

                //before attempting deserialize, ensure the string is valid
                if (string.IsNullOrWhiteSpace(model_text))
                    return null;

                //deserializes the text
                var MS = new MemoryStream(Encoding
                    .UTF8.GetBytes(model_text));
                var dsc =
                    new DataContractSerializer(typeof(UsersModel));
                var ret_val = dsc.ReadObject(MS) as UsersModel;

                return ret_val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    //For all User Results
        public static ResultsModel RModel { get; set; }

        async public static Task SaveResultModelAsync()
        {
            DataContractSerializer dcs = new DataContractSerializer(RModel.GetType());

            //Can read, but can't write Test.xml, "Access denied"
            //var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Test.xml"));

            //Writes Objects stored on Model to Users.xml
            var File = await ApplicationData.Current.LocalFolder.CreateFileAsync("UserResults.xml", CreationCollisionOption.OpenIfExists);
            var SST = await File.OpenTransactedWriteAsync();
            var RAS = SST.Stream;
            var OS = RAS.GetOutputStreamAt(0);
            var stream = OS.AsStreamForWrite();
            dcs.WriteObject(stream, RModel);
            stream.Flush();
            stream.Dispose();
            await SST.CommitAsync();
            SST.Dispose();
        }

        async public static Task<ResultsModel> LoadResultModelAsync()
        {
            try
            {
                //Can read, but can't write Test.xml, "Access denied"
                //var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Test.xml"));

                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("UserResults.xml");
                var opn = await file
                    .OpenAsync(Windows.Storage.FileAccessMode.Read);
                var stream = opn.GetInputStreamAt(0);
                var reader = new DataReader(stream);
                var size = (await file
                    .GetBasicPropertiesAsync()).Size;
                await reader.LoadAsync((uint)size);
                var model_text = reader.ReadString((uint)size);

                //Ensure's the string is valid
                if (string.IsNullOrWhiteSpace(model_text))
                    return null;

                //deserialize the text
                var ms = new MemoryStream(Encoding
                    .UTF8.GetBytes(model_text));
                var dsc =
                    new DataContractSerializer(typeof(ResultsModel));
                var res_val = dsc.ReadObject(ms) as ResultsModel;
                return res_val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        async protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Model = await LoadUserModelAsync();
            if (Model == null)
            {
                Model = new UsersModel();

                //add default Users if Users.xml is empty
                Model.Users.Add(new Users
                {
                    Username = "DVR",
                    Password = "1234"
                });

                Model.Users.Add(new Users
                {
                    Username = "Mario",
                    Password = "EkIsVet"
                });
            }

            RModel = await LoadResultModelAsync();
            if (RModel == null)
            {
                RModel = new ResultsModel();

                //add default UserResults if UserResults.xml is empty
                RModel.Results.Add(new Results
                {
                    User = "User: DVR",
                    Difficulty = "Difficulty: Easy",
                    Grade = "Grade: 4",
                    Score = "Score: 90%"
                });

                RModel.Results.Add(new Results
                {
                    User = "User: Mario",
                    Difficulty = "Difficulty: Hard",
                    Grade = "Grade: 4",
                    Score = "Score: 75%"
                });

                RModel.Results.Add(new Results
                {
                    User = "User: Joe",
                    Difficulty = "Difficulty: Hard",
                    Grade = "Grade: 3",
                    Score = "Score: 100%"
                });
            }


            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
